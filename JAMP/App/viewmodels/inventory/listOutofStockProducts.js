define([
    'services/datacontext',
    'durandal/plugins/router',
    'durandal/app',
    'services/paging',
    'services/model'],
    function (datacontext, router, app, paging, model) {

        var subscription,                    // Subscribe to DB changes
            outproducts = ko.observableArray(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false),
            filter = ko.observable(""),
            Predicate = breeze.Predicate;

        //#region Durandal Methods
        var activate = function () {
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
            bindEventToList(view, '.productDelete', deleteProduct);
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods

        //Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        // Send product to create an order
        var goToOrder = function (selectedProduct) {
            if (selectedProduct && selectedProduct.productID()) {
                var url = '#/Supplier_Create_Order/' + selectedProduct.productID();
                router.navigateTo(url);
            }
        };

        // Filter Products
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return outproducts();
            } else {
                return ko.utils.arrayFilter(outproducts(), function (item) {
                    return ko.utils.stringStartsWith(item.fullName().toLowerCase(), search);
                });
            }
        });

        // Check if list is empty and search term entered
        var emptyList = ko.computed(function () {
            return (filteredItems() === 0) && (filter() != "");
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            // Visible methods
            goBack: goBack,
            goToOrder: goToOrder,
            filter: filter,
            emptyList: emptyList,
            // Binding Observables
            outproducts: outproducts,
            filteredItems: filteredItems,
            // UI Methods
            title: 'Out of Stock'
        };

        return vm;

        //#region Internal Helper Methods
        // Bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        // Delete the current product
        function deleteProduct(selectedProduct) {
            if (selectedProduct && selectedProduct.productID()) {

                var msg = 'Delete category "' + selectedProduct.fullName() + '" ?';
                var title = 'Confirm Delete';
                isDeleting(true);
                return app.showMessage(msg, title, ['Yes', 'No'])
                    .then(confirmDelete);

                function confirmDelete(selectedOption) {
                    if (selectedOption === 'Yes') {
                        selectedProduct.archived(true);
                        selectedProduct.archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                        save(datacontext.saveReason.deletion, selectedProduct.fullName())
                            .fail(failed)
                            .fin(finish);
                    }
                    isDeleting(false);

                    function failed(error) {
                        cancel();
                        var errorMsg = 'Error: ' + error.message;
                        logger.logError(errorMsg, error, system.getModuleId(vm), true);
                    }

                    function finish() {
                        return selectedOption;
                    }
                }
            }
        }

        // Save changes
        function save(reason, msgName) {
            isSaving(true);

            return datacontext.saveChanges(reason, msgName)
                .fin(complete);

            function complete() {
                isSaving(false);
                return refreshPage();
            }
        }

        // Refresh Page
        function refreshPage() {
            var out = Predicate.create("quantity", "==", 0),
                archived = Predicate.create("archived", '==', false);

            return datacontext.getEntityList(outproducts, datacontext.entityAddress.product, false, null, Predicate.and([out, archived]));
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().category()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion

    });