define([
    'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {

        var subscription = null,
            incidentList = ko.observableArray([]),
            deletedProductList = ko.observableArray([]),
            addedProductList = ko.observableArray([]),
            archived = ko.observableArray(),
            selectedDate = ko.observable("");

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
            incidentList: incidentList,
            deletedProductList: deletedProductList,
            addedProductList: addedProductList,
            archived:archived,
            selectedDate: selectedDate,
            // UI Methods
            title: 'Activities'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh the page data
        function refreshPage() {
            var tempProductList = ko.observableArray([]);

            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: true
            };

            return Q.all([
            datacontext.getEntityList(incidentList, datacontext.entityAddress.incident, false, null, null, 10, null, null, 'incidentID desc'),      // Incident List
            datacontext.getEntityList(tempProductList, datacontext.entityAddress.product, false, null, null, null, null, null, 'productID desc'),
            datacontext.getEntityList(archived, datacontext.entityAddress.product, false, null, where, null, null, null, 'productID desc')])  // Product List
                .then(sortProducts);

            // Sort products into recent deleted and added
            function sortProducts() {
                var tempDeleteList = tempProductList.remove(function (product) {
                    return product.archived() == true;
                });

                deletedProductList(tempDeleteList.splice(0, 10));       // Take 10   [deleted]
                addedProductList(tempProductList.splice(0, 10));        // Take 10   [added]

                return true;
            }   
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().product() || datacontext.updateList().incident()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion
    });