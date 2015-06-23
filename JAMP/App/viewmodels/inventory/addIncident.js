define([
    'services/paging',
    'services/datacontext',
    'durandal/plugins/router',
    'durandal/app',
    'services/model'],
    function (paging, datacontext, router, app, model) {

        var subscription,                    // Subscribe to DB changes
            incident = ko.observable(),
            isSaving = ko.observable(false),
            productIncidentList = ko.observableArray([]),
            productList = ko.observableArray([]);

        //#region Durandal Methods
        var activate = function () {
            refreshPage();
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return incident(datacontext.createObject(model.entityNames.incident));
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                var msg = 'Do you want to leave and cancel?';
                return app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            datacontext.cancelChanges();
                        }
                        productIncidentList.removeAll();
                        subscription.dispose();
                        return selectedOption;
                    });
            }
            productIncidentList.removeAll();
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods
        var goBack = function () {
            router.navigateBack();
        };

        var cancel = function (complete) {
            router.navigateBack();
        };

        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        var canSave = ko.computed(function () {
            return hasChanges() && !isSaving();
        });

        var save = function () {
            isSaving(true);

            //Decrease quantity on the products before saving
            ko.utils.arrayForEach(productIncidentList(), function (productIncident) {
                productIncident.product().quantity(productIncident.product().quantity() - productIncident.quantity());
                productIncident.removed(true);
            });

            datacontext.saveChanges(datacontext.saveReason.add, model.entityNames.incident)
                .fin(complete);

            function complete() {
                isSaving(false);
                router.navigateBack();
            }
        };

        // Call product search modal
        var addProduct = function () {
            showAddProductModal();
        };

        // Add product to list
        var addToList = function (selectedProductIncident) {
            productIncidentList.push(selectedProductIncident);
        };

        // Remove product from list
        var removeFromList = function (selectedProductIncident) {

            // Add product back to product list
            productList.push(selectedProductIncident.product());
            // Remove Product Incident from list
            productIncidentList.remove(selectedProductIncident);
            // Detach entity from cache
            datacontext.entityDetached(selectedProductIncident);
        };

        // Increase the quantity of product
        var increaseQuantity = function (selectedProductIncident) {
            var currentVal = selectedProductIncident.quantity();
            if (currentVal < selectedProductIncident.product().quantity()) {
                selectedProductIncident.quantity(currentVal + 1);
            }
        };

        // Decrease the quantity of product
        var decreaseQuantity = function (selectedProductIncident) {
            var currentVal = selectedProductIncident.quantity();
            if (currentVal >= 2) {
                selectedProductIncident.quantity(currentVal - 1);
            }
        };

        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            hasChanges: hasChanges,
            cancel: cancel,
            canSave: canSave,
            save: save,                    
            addProduct: addProduct,
            removeFromList: removeFromList,
            increaseQuantity: increaseQuantity,
            decreaseQuantity: decreaseQuantity,
            // Binding Observables
            incident: incident,
            productIncidentList: productIncidentList,
            // UI Methods
            title: 'Report Incident'
        };

        return vm;

        //#region Internal Helper Methods

        // Activate the add product modal
        function showAddProductModal() {
            app.showModal('viewmodels/inventory/findProductModal', productList)
                .then(function (result) {

                    if (result && result != 'cancel') {

                        var productIncident = datacontext.createObject(model.entityNames.productIncident);

                        productList.remove(result);

                        productIncident.product(result);
                        productIncident.quantity(1);

                        productIncident.incident(incident());
                        addToList(productIncident);
                    }
                });
        }

        // Refresh the page
        function refreshPage() {
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: 'false'
            };
            return datacontext.getEntityList(productList, datacontext.entityAddress.product, false, null, where);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().product()) {
                    refreshPage().then(removedProducts);
                }
                datacontext.completedRefresh();
            }

            // Remove products already in incident
            function removedProducts() {
                return ko.utils.arrayForEach(productIncidentList(), function (productIncident) {
                    return productList.remove(productIncident.product());
                });
            }
        }

        //#endregion

    });