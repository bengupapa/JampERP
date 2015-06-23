define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/system',
    'durandal/app',
    'services/logger',
    'services/model'],
    function (datacontext, router, paging, system, app, logger, model) {
        var id = 0,
            subscription = null,
            order = ko.observable(),
            delivery = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);

        //#region Durandal Methods
        var activate = function (routeData) {
            id = parseInt(routeData.id, 10);
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refresh();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
            bindEventToList(view, '.item-info-con', gotoDeliveryDetails);
            bindEventToList(view, '.closeForm', closeForm);
        };

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var title = 'Cancel changes made to "' +
                   product().fullName() + '" ?';
                var msg = 'Navigate away?';

                return app.showMessage(title, msg, ['Yes', 'No'])
                    .then(checkAnswer);
            }
            subscription.dispose();
            delivery(null);
            return true;

            function checkAnswer(selectedOption) {
                if (selectedOption === 'Yes') {
                    subscription.dispose();
                    delivery(null);
                    cancel();
                }
                return selectedOption;
            }
        };
        //#endregion

        //#region Visible methods

        // Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        // Observable has changes
        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        // Cancel any changes made
        var cancel = function () {
            datacontext.cancelChanges();
        };

        // Check is there are changes
        // and object is not already saving
        var canSave = ko.computed(function () {
            return hasChanges() && !isSaving();
        });

        // Save changes
        var save = function (reason, msgName) {
            isSaving(true);
            if (reason !== datacontext.saveReason.deletion) {
                reason = datacontext.saveReason.update;
                msgName = supplier().supplierName();
            }
            return datacontext.saveChanges(reason, msgName).fin(complete);

            function complete() {
                isSaving(false);
            }
        };

        // Delete Order
        var deleteOrder = function () {
            var msg = 'Delete order?';
            var title = 'Confirm Delete';
            isDeleting(true);
            return app.showMessage(msg, title, ['Yes', 'No'])
                .then(confirmDelete);

            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {
                   
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
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            cancel: cancel,
            canSave: canSave,
            save: save,
            hasChanges: hasChanges,
            deleteOrder: deleteOrder,
            // Binding Observables
            order: order,
            delivery: delivery,
            // UI Methods
            title: 'Order Details'
        };

        return vm;

        //#region Internal Helper Methods

        // Go to Delivery Details
        function gotoDeliveryDetails(selectedDelivery) {
            delivery(selectedDelivery);
        }

        // Close the forms
        function closeForm() {
            delivery(null);
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
        function refresh() {
            return datacontext.getEntityDetails(id, model.entityNames.order, order);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().delivery()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }

        //#endregion
    });