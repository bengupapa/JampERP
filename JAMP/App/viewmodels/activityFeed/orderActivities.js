define([
    'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {

        var subscription = null,
            orderList = ko.observableArray([]),
            deliveryList = ko.observableArray([]),
            archivedOrders = ko.observableArray();

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            //Ordered by lastest
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Activity_Feed');
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods
        var goBack = function () {
            router.navigateBack();
        };

        
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            // Binding Observables
            orderList: orderList,
            deliveryList: deliveryList,
            archivedOrders:archivedOrders,
            // UI Methods
            title: 'Activities'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh the page data
        function refreshPage() {
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: false
            };

            return Q.all([
                datacontext.getEntityList(orderList, datacontext.entityAddress.order, false, null, where, 10, null, null, 'orderID desc'),
                datacontext.getEntityList(deliveryList, model.entityNames.delivery, false, null, null, 10, null, null, 'deliveryID desc'),
                datacontext.getEntityList(archivedOrders, datacontext.entityAddress.supplier, false, null, where, null, null, null, 'supplierID desc')]);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().order()||datacontext.updateList().delivery()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion
    });