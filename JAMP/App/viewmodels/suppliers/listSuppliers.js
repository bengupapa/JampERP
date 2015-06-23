define([
    'services/datacontext',
    'services/paging',
    'durandal/plugins/router',
    'services/model',
    'durandal/app'],
    function (datacontext, paging, router, model, app) {

        var subscription = null,
            suppliers = ko.observableArray(),
            filter = ko.observable("");

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
            bindEventToList(view, '.supplierDetails', gotoDetails);
            bindEventToList(view, '.accountDetails', gotoAccountDetails);
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

        // Go to add order page
        var makrORder = function () {
            var url = '#/Supplier_Create_Order';
            router.navigateTo(url);
        };

        // Filter through the suppliers
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return suppliers();
            } else {
                return ko.utils.arrayFilter(suppliers(), function (item) {
                    return ko.utils.stringStartsWith(item.supplierName().toLowerCase(), search);
                });
            }
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            makrORder: makrORder,
            // Binding Observables
            filter: filter,
            filteredItems: filteredItems,
            // UI Methods
            title: 'Suppliers'
        };

        return vm;

        //#region Internal Helper Methods

        // Go to supplier details
        function gotoDetails(selectedSupplier) {
            if (selectedSupplier && selectedSupplier.supplierID()) {
                var url = '#/Supplier_Details/' + selectedSupplier.supplierID();
                router.navigateTo(url);
            }
        }

        // Go to Supplier account details
        function gotoAccountDetails(selectedSupplier) {
            
            if (selectedSupplier && selectedSupplier.supplierID()) {
                var url = '#/Supplier_Account_Details/' + selectedSupplier.supplierID();
                router.navigateTo(url);
            }
        };

        // Bind click event on item-con's
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        // Refresh Page
        function refreshPage() {
            var where = {
                 firstParm: 'archived',
                 operater: '==',
                 secondParm: false
             };

            return datacontext.getEntityList(suppliers, datacontext.entityAddress.supplier, false, null, where);
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