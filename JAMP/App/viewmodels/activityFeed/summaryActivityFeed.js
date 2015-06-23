define([
     'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {
           
        //#region Durandal Methods
        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            bindEventToList(view, '.sales', saleActivity);
            bindEventToList(view, '.customer', customerActivity);
            bindEventToList(view, '.inventory', inventoryActivity);
            bindEventToList(view, '.supplier', supplierActivity);
            bindEventToList(view, '.order', orderActivity);
        };
        //#endregion

        var vm = {
            // Durandal Methods
            viewAttached: viewAttached,
            // UI Methods
            title: 'Activity Feeds'
        };

        return vm;

        //#region Internal Helper Methods

        function saleActivity() {
            router.navigateTo('#/Sale_Activities');
        }

        function customerActivity() {
            router.navigateTo('#/Customer_Activities');
        }

        function inventoryActivity() {
            router.navigateTo('#/Inventory_Activities');
        }

        function supplierActivity() {
            router.navigateTo('#/Supplier_Activities');
        }

        function orderActivity() {
            router.navigateTo('#/Order_Activities');
        }

        // Bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        //#endregion
    });