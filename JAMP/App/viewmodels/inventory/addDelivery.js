define([
    'durandal/app',
    'services/datacontext',
    'services/paging',
    'services/model',
    'durandal/plugins/router'],
    function (app, datacontext, paging, model, router) {

        var subscription,                               // Subscribe to DB changes
            orders = ko.observableArray([]),
            productDeliveries = ko.observableArray([]),
            delivery = ko.observable(),
            order = ko.observable(null),
            filter = ko.observable(""),
            deliveryComplete = ko.observable('No'),     // Check switch         [default No]
            deliveryFulfilled = ko.observable('No'),    // Check if fulfilled
            isSaving = ko.observable(false),
            inputError = ko.observable(false),          // Check if errors on inputs
            Predicate = breeze.Predicate;


        //#region Durandal Methods
        var activate = function () {
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
            bindEventToList(view, '.item-info-con', gotoOrderDetails);
            bindEventToList(view, '.closeForm', closeForm);
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                var msg = 'Do you want to leave and cancel?';
                return app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            order(null);
                            filter("");
                            if (delivery() != null) {
                                datacontext.entityDetached(delivery());
                            }
                            datacontext.cancelChanges();
                            deliveryComplete('No');
                            deliveryFulfilled('No');
                            subscription.dispose();
                        }
                        return selectedOption;
                    });
            }
            filter("");
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods
        var goBack = function () {
            if (order() === null) {
                router.navigateBack();
            } else {
                closeForm();
            }
        };

        // Changes in the entity manager
        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        // Check if can save
        var canSave = ko.computed(function () {
            return !inputError() && !isSaving();
        });

        // All ordered products delivered
        var orderFullfilled = ko.computed(function () {
            if (order() !== null) {
                var listLength = order().productOrders().length;
                var completedLength = 0;
                var errorCount = 0;

                ko.utils.arrayForEach(order().productOrders(), function (item) {
                    if (item.itemOrderComplete() === "Complete") {
                        completedLength += 1;
                    } else if (item.itemOrderComplete() === "Over") {
                        errorCount += 1;
                    }
                });

                // Check if errors
                if (errorCount > 0) {
                    inputError(true);
                } else {
                    inputError(false);
                }

                // Check order list is completed
                if (completedLength === listLength) {
                    deliveryComplete('Yes');
                    deliveryFulfilled('Yes');
                    return true;
                }
            }
            deliveryComplete('No');
            deliveryFulfilled('No');
            return false;
        });

        // Save delivery
        var save = function () {

            if (deliveryFulfilled() === 'No') {
                var msg = 'Do you want to save partially completed order?';
                app.showMessage(msg, 'Confirmation', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            saveDelivery();
                        }
                    });
            } else {
                saveDelivery();
            }

            // Save Delivery
            function saveDelivery() {
                isSaving(true);

                // Set appropriate status to order
                if (deliveryFulfilled() === 'Yes' || deliveryComplete() === 'Yes') {
                    order().completed('Completed');
                    if (deliveryFulfilled() === 'No') {
                        updateSupplier();
                    }
                } else {
                    order().completed('Partial');
                }
                // Update Products with new quantities
                ko.utils.arrayForEach(productDeliveries(), function (item) {
                    var newValue = item.product().quantity() + item.quantityDelivered();
                    item.product().quantity(newValue);
                });

                datacontext.saveChanges(datacontext.saveReason.add, model.entityNames.delivery)
                    .fin(complete);
            }

            // Update supplier account with partial completed orders
            function updateSupplier() {
                var valueChange = 0;
                ko.utils.arrayForEach(order().productOrders(), function (item) {
                    var differance = item.quantityOrdered() - item.quantityDelivered();
                    if (differance > 0) {
                        valueChange += differance * item.costPrice();
                    }
                });

                // Update supplier Account
                order().supplier().supplierAccount().amountOwed(order().supplier().supplierAccount().amountOwed() - valueChange);
            }

            // Completed the save operation
            function complete() {
                isSaving(false);
                deliveryComplete('No');
                deliveryFulfilled('No');
                return refreshPage().then(order(null));
            }
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
            canSave: canSave,
            save: save,
            hasChanges: hasChanges,
            orderFullfilled: orderFullfilled,
            // Binding Observables
            filter: filter,
            filteredItems: filteredItems,
            order: order,
            delivery: delivery,
            productDeliveries: productDeliveries,
            deliveryComplete: deliveryComplete,
            // UI Methods
            title: 'Delivery'
        };

        return vm;

        //#region Internal Helper Methods

        // Go to Order Details and delivery
        function gotoOrderDetails(selectedOrder) {
            // Clean product Deliveries list
            productDeliveries([]);
            // Create delivery entity
            delivery(datacontext.createObject(model.entityNames.delivery));

            //Assign order to delivery
            delivery().order(selectedOrder);

            order(selectedOrder); // Show current order

            // Build product delivery list
            ko.utils.arrayForEach(order().productOrders(), function (item) {

                // Get only product item which aren't completed
                if (item.itemOrderComplete() !== "Complete") {
                    var productDelivery = datacontext.createObject(model.entityNames.productDelivery);

                    productDelivery.product(item.product());
                    productDelivery.delivery(delivery());

                    // Product order subscribe to delivery quantity change
                    productDelivery.quantityDelivered.subscribe(function (newValue) {

                        // Check if has previous deliviers
                        if (item.tempDelivery > 0) {
                            item.quantityDelivered(item.tempDelivery + newValue);
                        } else {
                            item.quantityDelivered(newValue);
                        }
                    });

                    // Calculate need units 
                    // make sure value is a number
                    var value = item.quantityDelivered() > 0 ? item.quantityDelivered() : 0;
                    productDelivery.quantityNeeded(item.quantityOrdered() - value);

                    // add product delivery to list
                    productDeliveries.push(productDelivery);
                }
            });
        }

        // Close the forms
        function closeForm() {
            var msg = 'Do you want go to orders?';
            app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                        .then(function (selectedOption) {
                            if (selectedOption === 'Yes') {
                                order(null);
                                datacontext.entityDetached(delivery());
                                datacontext.cancelChanges();
                                deliveryComplete('No');
                            }
                        });
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

        // Refresh page observables
        function refreshPage() {
            var unCompleted = Predicate.create("completed", '!=', 'Completed'),
                archived = Predicate.create("archived", '==', false);
            return datacontext.getEntityList(orders, datacontext.entityAddress.order, false, 'productOrders, employee, supplier.supplierAccount', Predicate.and([unCompleted, archived]));
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