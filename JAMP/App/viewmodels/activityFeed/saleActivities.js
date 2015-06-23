define([
    'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {

        var subscription = null,
            saleList = ko.observableArray([]);

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            //Order by lastest
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
            // Visible Methods
            goBack: goBack,
            // Binding Observables
            saleList: saleList,
            // UI Methods
            title: 'Sales'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh the page data
        function refreshPage() {
            return datacontext.getEntityList(saleList, datacontext.entityAddress.sale, false, null, null, 10, null, null, 'createdDate desc');
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().sale()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion

    });