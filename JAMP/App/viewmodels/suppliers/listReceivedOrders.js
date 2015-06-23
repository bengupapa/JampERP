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
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false),
            Predicate = breeze.Predicate;

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
            bindEventToList(view, '.item-info-con', gotoOrderDetails);
        };

        var canDeactivate = function () {
            subscription.dispose();
            filter("");
            return true;
        };

        //#region Visible Methods
        var goBack = function () {
            router.navigateBack();
        };
        //#endregion

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

        // Delete the click order
        var removeOrder = function (selectedOrder) {
            deleteOrder(selectedOrder);
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            removeOrder: removeOrder,
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

        // Delete Order
        function deleteOrder(selectedOrder) {
            var msg = 'Delete order?';
            var title = 'Confirm Delete';

            return app.showMessage(msg, title, ['Yes', 'No'])
                .then(confirmDelete);

            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {
                    selectedOrder.archived(true);
                    selectedOrder.archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                    save(datacontext.saveReason.deletion, model.entityNames.order)
                        .fail(failed)
                        .fin(finish);
                }
                isDeleting(false);

                function failed(error) {
                    cancel();
                    var errorMsg = 'Error: ' + error.message;
                    logger.logError(errorMsg, error, system.getModuleId(vm), true);
                }

                function finish() {
                    return selectedOption;
                }
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

        // Save changes
        function save(reason, msgName) {
            isSaving(true);

            return datacontext.saveChanges(reason, msgName).fin(complete);

            function complete() {
                isSaving(false);
                refreshPage();
            }
        }

        // Refresh page
        function refreshPage() {
            var where = Predicate.create("completed", '==', 'Completed'),
                archived = Predicate.create("archived", '==', false);
            return datacontext.getEntityList(orders, datacontext.entityAddress.order, false, null, Predicate.and([where, archived]));
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