define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging'],
    function (datacontext, router, paging) {

        var subscription = null,
            customerList = ko.observableArray([]),
            filter = ko.observable("");

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get data for page
            return refreshPage();
        };

        var viewAttached = function (view) {
            // Set nav to customers
            paging.setActive(true, 'Customers');
            // Div click bindings
            bindEventToList(view, '.cutomerDetails', gotoDetails);
            bindEventToList(view, '.accountDetails', gotoAccountDetails);
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods

        // Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        // Add a customer
        var addCustomer = function () {
            return router.navigateTo('#/Customers_Create');
        };

        // Filter customers
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return customerList();
            } else {
                return ko.utils.arrayFilter(customerList(), function (item) {
                    return ko.utils.stringStartsWith(item.customerName().toLowerCase(), search);
                });
            }
        });

        // Check if list is empty
        var emptyList = ko.computed(function () {
            return (filteredItems() == 0) && (filter() != "");
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            addCustomer: addCustomer,
            // Binding Observables
            filter: filter,
            filteredItems: filteredItems,
            emptyList: emptyList,
            customerList: customerList,
            // UI Methods
            title: 'Customers'
        };

        return vm;

        //#region Internal Helper Methods

        // Go to Customer details 
        function gotoDetails(selectedCustomer) {
            if (selectedCustomer && selectedCustomer.customerID()) {
                var url = '#/Customers_Details/' + selectedCustomer.customerID();
                router.navigateTo(url);
            }
        }

        // Go to Cusomter account details
        function gotoAccountDetails(selectedCustomer) {
            if (selectedCustomer && selectedCustomer.customerID()) {
                var url = '#/Customers_Account_Details/' + selectedCustomer.customerID();
                router.navigateTo(url);
            }
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

        // Refresh data on the page
        function refreshPage() {
            var where = {
                firstParm: "archived",
                operater: '==',
                secondParm: false
            };
            return datacontext.getEntityList(customerList, datacontext.entityAddress.customer, false, null, where);
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