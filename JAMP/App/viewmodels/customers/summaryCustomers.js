define([
    'services/datacontext',
    'services/logger'],
    function (datacontext, logger) {

        var subscription = null,
            accountslist = ko.observableArray([]),
            newcustomers = ko.observableArray([]);

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get data from server
            return refreshPage();
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Binding Observables
            accountslist: accountslist,
            newcustomers: newcustomers,
            // UI Methods
            title: 'Customers',
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh data on the page
        function refreshPage() {
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: false
            };
            return Q.all([
                datacontext.getEntityList(accountslist, datacontext.entityAddress.customer, false, null, where, 3, null, 'customerAccount', 'customerAccount.amountOwing desc'),
                datacontext.getEntityList(newcustomers, datacontext.entityAddress.customer, false, null, where, 3, null, null, 'customerID desc')
            ]);
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