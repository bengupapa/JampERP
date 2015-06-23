define([
    'services/datacontext',
    'services/paging',
    'durandal/plugins/router',
    'services/model',
    'durandal/app'],
    function (datacontext, paging, router, model, app) {

        var subscription = null,
            orders = ko.observableArray([]),
            filter = ko.observable(""),
            Predicate = breeze.Predicate;

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get the data
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
            bindEventToList(view, '.item-info-con', gotoOrderDetails);
        };

        var canDeactivate = function () {
            filter("");
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods
        var goBack = function () {
            router.navigateBack();
        };

        // Filter orders
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return orders();
            } else {
                return ko.utils.arrayFilter(orders(), function (item) {
                    return ko.utils.stringStartsWith(item.supplier().supplierName().toLowerCase(), search);
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
            // Binding Observables
            filter: filter,
            filteredItems: filteredItems,
            // UI Methods
            title: 'Orders',
        };

        return vm;

        //#region Internal Helper Methods

        // Go to Order Details and delivery
        function gotoOrderDetails(selectedOrder) {
            if (selectedOrder && selectedOrder.orderID()) {
                var url = '#/Supplier_Order_Details/' + selectedOrder.orderID();
                router.navigateTo(url);
            }
        }

        // Binding click events
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var p = ko.dataFor(this);
                callback(p);
                return false;
            });
        }

        // Refresh page
        function refreshPage() {
            var nowDate = moment().format("YYYY-MM-DD"),
                unCompleted = Predicate.create("completed", '!=', 'Completed'),
                overdue = Predicate.create("dateDue", '<', nowDate),
                archived = Predicate.create("archived", '==', false);
            return datacontext.getEntityList(orders, datacontext.entityAddress.order, false, null, Predicate.and([unCompleted, overdue, archived]));
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().order()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion
    });