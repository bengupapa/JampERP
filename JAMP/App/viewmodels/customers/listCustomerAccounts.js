define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging'],
    function (datacontext, router, paging) {

        var subscription = null,
            accountList = ko.observableArray([]),
            filter = ko.observable("");

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get page data
            return refreshPage();
        };

        var viewAttached = function (view) {
            // Set nav to customers
            paging.setActive(true, 'Customers');
            // Div click bindings
            bindEventToList(view, '.transactionDetails', gotoDetails);
            bindEventToList(view, '.accountDetails', gotoAccountDetails);
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

        var addCustomerAccount = function () {
            return router.navigateTo('#/Customers_Add_Customer_Account');
        };

        // Filter the list of customers
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return accountList();
            } else {
                return ko.utils.arrayFilter(accountList(), function (item) {
                    return ko.utils.stringStartsWith(item.accountName().toLowerCase(), search);
                });
            }
        });

        // Check if empty list
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
            addCustomerAccount: addCustomerAccount,
            // Binding Observables
            accountList: accountList,
            filter: filter,
            filteredItems: filteredItems,
            emptyList: emptyList,
            // UI Methods
            title: 'Customers Accounts'
        };

        return vm;

        //#region Internal Helper Methods

        // Go to Customer details 
        function gotoDetails(selectedCustomer) {
            if (selectedCustomer && selectedCustomer.customerID()) {
                var url = '#/Customers_Account_Payment/' + selectedCustomer.customerID();
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
            return datacontext.getEntityList(accountList, datacontext.entityAddress.customer, false, null, where);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().customer()) {
                    refreshPage();
                    datacontext.updateList().customer(false);
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion

    });