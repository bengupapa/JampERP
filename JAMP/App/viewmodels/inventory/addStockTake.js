define([
    'durandal/app',
    'services/paging',
    'durandal/plugins/router',
    'services/datacontext',
    'services/model'],
    function (app, paging, router, datacontext, model) {

        var categoryList = ko.observableArray([]),
            secondCheckList = ko.observableArray([]),
            removeList = ko.observableArray([]),
            productIncidentList = ko.observableArray([]),
            incident = ko.observable(),
            isSaving = ko.observable(false),
            category = ko.observable(null),
            canShowSummary = ko.observable(false);

        //#region Durandal Methods
        var activate = function () {
            var tempCategoryList = ko.observableArray([]);
            return datacontext.getEntityList(tempCategoryList, datacontext.entityAddress.category)
                .then(checkCategories);

            function checkCategories() {

                ko.utils.arrayForEach(tempCategoryList(), function (category) {
                    if (category.productList().length > 0) {
                        categoryList.push(category);
                    }
                });
                return true;
            }
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
            bindEventToList(view, '.item-info-con', gotoDetails);
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                var msg = 'Do you want to leave and cancel?';
                return app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            resetPage();
                            categoryList.removeAll();
                            datacontext.cancelChanges();
                        }
                        return selectedOption;
                    });
            }
            resetPage();
            categoryList.removeAll();
            return true;
        };
        //#endregion

        //#region Visible Methods
        var goBack = function () {
            console.log('Going back');
            if (category() === null) {
                router.navigateBack();
            }
            else {
                closeForm();
            }
        };

        // First check of stock 
        var checkQuantity = function () {

            ko.utils.arrayForEach(category().productList(), function (product) {
                if (product.stockCountQuantity() != product.quantity()) {
                    secondCheckList.push(product);
                }
                product.stockCountQuantity(0);
            });
            if (secondCheckList().length === 0) {
                createSummary();
            }
        };

        // Re-count the quantities
        var secondCheck = function () {

            ko.utils.arrayForEach(secondCheckList(), function (product) {
                if (product.stockCountQuantity() == product.quantity()) {
                    product.stockCountQuantity(0);
                    removeList.push(product);
                }
            });
            createSummary();
        };

        // Display the recount form
        var displayRecount = ko.computed(function () {
            return category() != null && !canShowSummary();
        });

        // Save the stock take incident
        var save = function () {
            isSaving(true);
            

            //Decrease quantity on the products before saving
            ko.utils.arrayForEach(productIncidentList(), function (productIncident) {
                var countQuantity = productIncident.product().stockCountQuantity();
                var recordedQuantity = productIncident.product().quantity();
                var reconAmount = Math.abs(recordedQuantity - countQuantity);

                productIncident.quantity(reconAmount);

                // Add if count large or less
                if (countQuantity > recordedQuantity) {
                    productIncident.product().quantity(recordedQuantity + reconAmount);
                    productIncident.removed(false);
                } else {
                    productIncident.product().quantity(recordedQuantity - reconAmount);
                    productIncident.removed(true);
                }
                productIncident.product().stockCountQuantity(0);
            });

            
            // Create note:
            var note = "Completed " + category().categoryName() +
                " stock take. Counted [" + category().productList().length + "] products of which [" +
                secondCheckList().length + "] where incorrectly recorded. \r\n  \r\n";
            var currentNote = incident().notes() != null ? incident().notes() : '';

            incident().notes(note + currentNote);

            datacontext.saveChanges(datacontext.saveReason.add, model.entityNames.incident)
                .fin(complete);

            function complete() {
                isSaving(false);
                router.navigateBack();
            }
        };

        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        var canSave = ko.computed(function () {
            return hasChanges() && !isSaving();
        });

        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            checkQuantity: checkQuantity,
            secondCheck: secondCheck,
            canShowSummary: canShowSummary,
            displayRecount: displayRecount,
            save: save,
            canSave: canSave,
            // Binding Observables
            categoryList: categoryList,
            category: category,
            secondCheckList: secondCheckList,
            incident: incident,
            productIncidentList: productIncidentList,
            // UI Methods
            title: 'Stock Take'
        };

        return vm;

        //#region Internal Helper Methods

        // Go to Category product list
        function gotoDetails(selectedCategory) {
            category(selectedCategory);
        }

        // Close the forms
        function closeForm() {
            var msg = 'Do you want cancel the stock take?';
            app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            resetPage();
                            datacontext.cancelChanges();
                            
                            return true
                        }
                        return false;
                    });
        }

        // Bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var p = ko.dataFor(this);
                callback(p);
                return false;
            });
        }

        // Create the stock take summary & incident
        function createSummary() {
            

            if (removeList().length > 0) {
                ko.utils.arrayForEach(removeList(), function (product) {
                    secondCheckList.remove(product);
                });
            }

            incident(datacontext.createObject(model.entityNames.incident));
            canShowSummary(true);

            incident().type('Stock Take');
            // Check if there are recon items
            if (secondCheckList().length > 0) {
                ko.utils.arrayForEach(secondCheckList(), function (product) {
                    var productIncident = datacontext.createObject(model.entityNames.productIncident);

                    // Link product
                    productIncident.product(product);

                    // Link incident
                    productIncident.incident(incident());

                    // Add product incident to list
                    productIncidentList.push(productIncident);
                });
            }
        }

        // Reset page
        function resetPage() {
            secondCheckList.removeAll();
            removeList.removeAll();
            canShowSummary(false);
            category(null);
            productIncidentList.removeAll();
        }

        //#endregion

    });