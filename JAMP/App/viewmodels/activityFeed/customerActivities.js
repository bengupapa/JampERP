define([
    'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {

        var subscription = null,
            customerList = ko.observableArray([]),
            paymentsList = ko.observableArray([]),
            selectedDate = ko.observable(""),
            achievedlist = ko.observableArray();

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
            customerList: customerList,
            paymentsList: paymentsList,
            selectedDate: selectedDate,
            achievedlist:achievedlist,
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
                datacontext.getEntityList(paymentsList, model.entityNames.customerPayment, false, null, null, 10, null, null, 'customerPaymentID desc'),
                datacontext.getEntityList(customerList, datacontext.entityAddress.customer, false, null, null, 10, null, null, 'customerID desc'),
                datacontext.getEntityList(achievedlist, datacontext.entityAddress.customer, false, null, where, null, null, null, 'customerID desc')]);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().customer()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion

    });