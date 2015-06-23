define([
    'services/datacontext',
    'durandal/app',
    'services/logger',
    'durandal/plugins/router',
    'services/model'],
    function (datacontext, app, logger, router, model) {

        var subscription = null,                
            productlist = ko.observable(),
            lowproducts = ko.observable(),
            outproducts = ko.observable(),
            incidents = ko.observable(),
            userSettings = ko.observable(),
            nearReorder = ko.observableArray([]),
            slowMovingHiQuantity = ko.observableArray([]),
            highTurnOver = ko.observableArray([]),
            lowTurnOver = ko.observableArray([]),
            Predicate = breeze.Predicate;




        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get user settings
            userSettings(datacontext.currentUser().settings());
            return refreshPage();
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            bindEventToList(view, '.infoTurnover', infoTurnover);
            bindEventToList(view, '.infoNearReorder', infoNearReorder);
            bindEventToList(view, '.infoSlowMoving', infoSlowMoving);
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
            productlist: productlist,
            lowproducts: lowproducts,
            outproducts: outproducts,
            incidents: incidents,
            nearReorder: nearReorder,
            slowMovingHiQuantity: slowMovingHiQuantity,
            highTurnOver: highTurnOver,
            lowTurnOver: lowTurnOver,
            userSettings: userSettings,
            // UI Methods
            title: 'Inventory'
        };

        return vm;

        //#region Internal Helper Methods

        // Modal Turnover Info
        function infoTurnover() {
            var title = 'Turnover Rate';
            var msg = 'This measure refers to the average quantity sold per day over the last 30 days. ' +
                'This helps to show the number of days worth of inventory is on hand.';
            app.showMessage(msg, title, ['Ok']);
        }

        // Modal Near reorder Info
        function infoNearReorder() {
            var title = 'Near Reorder Level';
            var msg = 'This measure refers to the products that are nearing there reorder level. '
                + 'By showing how many days worth or inventory there are till it reaches the reorder level.';
            app.showMessage(msg, title, ['Ok']);
        }

        // Modal slow moving Info
        function infoSlowMoving() {
            var title = 'Slow Moving High Quantity';
            var msg = 'This measure refers to the products that have no sales/or few sales over the last 30 days.';
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

        // Refresh data on the page
        function refreshPage() {
            var tempNearReorder = ko.observableArray([]),
                tempTurnOverRate = ko.observableArray([]),
                tempMovingHiQuantity = ko.observableArray([]),
                out = Predicate.create("quantity", "==", 0),
                low = Predicate.create("quantity", "<=", 'reorderLevel'),
                nReorder = Predicate.create("quantity", '>', 'reorderLevel'),
                archived = Predicate.create("archived", '==', false);

            return Q.all([
                //Get counts
                datacontext.getEntityCount(productlist, datacontext.entityAddress.product, false, archived),                        // All product
                datacontext.getEntityCount(outproducts, datacontext.entityAddress.product, false, Predicate.and([out, archived])),  // Out of stock
                datacontext.getEntityCount(lowproducts, datacontext.entityAddress.product, false, Predicate.and([low, archived])),  // Below reorder lvl
                datacontext.getEntityCount(incidents, datacontext.entityAddress.incident),                                          // All incidents
                //Get product lists
                datacontext.getEntityList(tempNearReorder, datacontext.entityAddress.product, false, null, Predicate.and([nReorder, archived])),    // Near reorder
                datacontext.getEntityList(tempTurnOverRate, datacontext.entityAddress.product, false, null)                                         // Hi/Lo turnover rate
            ]).then(sortProducts);

            //Sort products based on how close they are to reorder lvl
            function sortProducts() {
                // OrderBy ascending  [reorder]
                tempNearReorder.remove(function (item) { return item.closeReorder() === 0; });
                tempNearReorder.sort(function (left, right) {
                    return left.closeReorder() === right.closeReorder() ? 0 : (left.closeReorder() < right.closeReorder() ? -1 : 1);
                });

                // Get not selling products
                var tempMovingHiQuantity = tempTurnOverRate.remove(function (product) {
                    return product.turnOverRate() === 0;
                });

                // OrderBy Desc [slow moving]
                tempMovingHiQuantity.sort(function (left, right) {
                    return left.quantity() === right.quantity() ? 0 : (left.quantity() > right.quantity() ? -1 : 1);
                });

                // OrderBy Desc [Turnover rate]
                tempTurnOverRate.sort(function (left, right) {
                    return left.turnOverRate() === right.turnOverRate() ? 0 : (left.turnOverRate() > right.turnOverRate() ? -1 : 1);
                });

                nearReorder(tempNearReorder.splice(0, 3));                  // Take closest 3   [reorder]
                slowMovingHiQuantity(tempMovingHiQuantity.splice(0, 3));    // Take worst 3     [slow moving]
                highTurnOver(tempTurnOverRate.splice(0, 3));                // Take highest 3   [Turnover rate]
                tempTurnOverRate.reverse();                                 // Reverse order
                lowTurnOver(tempTurnOverRate.splice(0, 3));                 // Take lowest 3    [Turnover rate]

                // Sort slow moving sales items for desc
                ko.utils.arrayFilter(slowMovingHiQuantity(), function (product) {
                    return product.salesItems.sort(function (left, right) {
                        return left.saleID() === right.saleID() ? 0 : (left.saleID() > right.saleID() ? -1 : 1);
                    });
                });

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