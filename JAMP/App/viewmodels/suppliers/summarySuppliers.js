define([
    'services/logger',
    'services/datacontext',
    'durandal/app',
    'services/model'],
    function (logger, datacontext, app, model) {

        var subscription = null,
            orderList = ko.observable(),
            completeOrders = ko.observable(),
            completedOrders = ko.observable(),
            overDueOrders = ko.observable(),
            outstandingOrders = ko.observable(),
            allSuppliers = ko.observableArray([]),
            supplierList = ko.observableArray([]),
            Predicate = breeze.Predicate,
            userSettings = ko.observable();

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get the page data
            userSettings(datacontext.currentUser().settings());
            return refreshPage();
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            bindEventToList(view, '.infoSummary', infoSummary);
            bindEventToList(view, '.infoDelivery', infoDelivery);
            bindEventToList(view, '.infoAccount', infoAccount);
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
            orderList: orderList,
            completeOrders: completeOrders,
            completedOrders:completedOrders,
            overDueOrders: overDueOrders,
            outstandingOrders: outstandingOrders,
            allSuppliers: allSuppliers,
            supplierList: supplierList,
            userSettings:userSettings,
            // UI Methods
            title: 'Suppliers'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh page
        function refreshPage() {
            var nowDate = moment().format("YYYY-MM-DD"),
                completed = Predicate.create("completed", '==', 'Completed'),
                unCompleted = Predicate.create("completed", '!=', 'Completed'),
                overdue = Predicate.create("dateDue", '<', nowDate),
                outstanding = Predicate.create("dateDue", '>=', nowDate),
                archived = Predicate.create("archived", '==', false);

            return Q.all([
                // Get counts
                datacontext.getEntityCount(orderList, datacontext.entityAddress.order, false, Predicate.and([archived])),                                                                     // All orders
                datacontext.getEntityCount(completeOrders, datacontext.entityAddress.order, false, Predicate.and([completed, archived])),                   // Completed Orders
                datacontext.getEntityCount(overDueOrders, datacontext.entityAddress.order, false, Predicate.and([overdue, unCompleted, archived])),         // Overdue orders
                datacontext.getEntityCount(outstandingOrders, datacontext.entityAddress.order, false, Predicate.and([outstanding, unCompleted, archived])), // AOutstanding Orders
                // Get lists
                datacontext.getEntityList(allSuppliers, datacontext.entityAddress.supplier, false, null, Predicate.and([archived]), 6),                               // All supplier
                datacontext.getEntityList(supplierList, datacontext.entityAddress.supplier, false, null, Predicate.and([archived]), 6),
                datacontext.getEntityList(completedOrders, datacontext.entityAddress.order, false, 'supplier', Predicate.and([completed, archived])),
            ]);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().supplier() || datacontext.updateList().order()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }

        // Modal Turnover Info
        function infoSummary() {
            var title = 'Summary';
            var msg = 'Summary of the latest orders made to a supplier. Shows the amount owed against the number of orders made';
            app.showMessage(msg, title, ['Ok']);
        }

        // Modal Near reorder Info
        function infoDelivery() {
            var title = 'Deliveries';
            var msg = 'Displays the total number of Deliveries made by suppliers to the business.';
            app.showMessage(msg, title, ['Ok']);
        }

        // Modal slow moving Info
        function infoAccount() {
            var title = 'Accounts';
            var msg = 'Summary of the supplier accounts and the amount that they owe.'+'Looks at the latest supplier accounts';
            app.showMessage(msg, title, ['Ok']);
        }

        // bind click event
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