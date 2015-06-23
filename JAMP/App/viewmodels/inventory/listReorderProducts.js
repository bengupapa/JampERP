define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging'],
    function (datacontext, router, paging) {

        var subscription,                    // Subscribe to DB changes
            lowproducts = ko.observableArray(),
            Predicate = breeze.Predicate;

        //#region Durandal Methods
        var activate = function () {
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
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
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            // Visible methods
            goBack: goBack,
            goToOrder: goToOrder,
            // Binding Observables
            lowproducts: lowproducts,
            // UI Methods
            title: 'Reorder List'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh data on the page
        function refreshPage() {
            var low = Predicate.create("quantity", "<=", 'reorderLevel'),
                archived = Predicate.create("archived", '==', false);

            return datacontext.getEntityList(lowproducts, datacontext.entityAddress.product, false, null, Predicate.and([low, archived]));
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().product()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }

        //#endregion

    });