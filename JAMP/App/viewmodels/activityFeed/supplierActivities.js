define([
    'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {

        var subscription = null,
            supplierList = ko.observableArray([]),
            paymentsList = ko.observableArray([]),
            archivedSupplier = ko.observableArray(),
            archivedOrders = ko.observableArray();

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            //Order by latest
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
            supplierList: supplierList,
            paymentsList: paymentsList,
            archivedSupplier:archivedSupplier,
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
                secondParm: true
            };
            return Q.all([
                datacontext.getEntityList(paymentsList, model.entityNames.supplierPayment, false, null, null, 10, null, null, 'supplierPaymentID desc'),
                datacontext.getEntityList(supplierList, datacontext.entityAddress.supplier, false, null, null, 10, null, null, 'supplierID desc'),
                datacontext.getEntityList(archivedSupplier, datacontext.entityAddress.supplier, false, null, where, null, null, null, 'supplierID desc')]);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().supplier()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion

    });