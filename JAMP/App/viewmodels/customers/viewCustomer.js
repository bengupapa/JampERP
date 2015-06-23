define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/system',
    'durandal/app',
    'services/logger'],
    function (datacontext, router, paging, system, app, logger) {

        var customer = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);

        //#region Durandal Methods
        var activate = function (routeData) {
            var id = parseInt(routeData.id);
            return datacontext.getEntityDetails(id, 'Customer', customer);
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Customers');
        }

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var title = 'Cancel changes made to "' +
                   customer().customerName() + '" ?';
                var msg = 'Navigate away?';

                return app.showMessage(title, msg, ['Yes', 'No'])
                    .then(checkAnswer);
            }
            vm.customerEditable(false);
            return true;

            function checkAnswer(selectedOption) {
                if (selectedOption === 'Yes') {
                    cancel();
                    vm.customerEditable(false);
                }
                return selectedOption;
            }
        };
        //#endregion

        //#region Visible Methods

        // Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        // Edit customer details switch
        var editCustomer = function () {
            if (vm.customerEditable() === true) {
                return vm.customerEditable(false);
            }
            else {
                return vm.customerEditable(true);
            }
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
            return hasChanges() && !isSaving() && vm.customerEditable();
        });

        // Save changes
        var save = function (reason, msgName) {
            isSaving(true);
            if (reason != datacontext.saveReason.deletion) {
                reason = datacontext.saveReason.update;
                msgName = customer().customerName();
            }

            return datacontext.saveChanges(reason, msgName).fin(complete);

            function complete() {
                isSaving(false);
                router.navigateBack();
            }
        };

        // Delete the current product
        var deleteCustomer = function () {
            var msg = 'Are you sure you want to Delete Customer Account "' + customer().customerAccount().accountName() + '" Currently Owing R' + customer().customerAccount().amountOwing() + ' ?';
            var title = 'Confirm Delete';
            isDeleting(true);
            return app.showMessage(msg, title, ['Yes', 'No'])
                .then(confirmDelete);

            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {

                    // Archive customer and thier account
                    customer().customerAccount().archived(true);
                    customer().archived(true);
                    customer().archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                    save(datacontext.saveReason.deletion, customer().customerName()).then(success).fail(failed).fin(finish);
                }
                isDeleting(false);

                function success() {
                    router.navigateBack();
                }

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
            cancel: cancel,
            canSave: canSave,
            save: save,
            hasChanges: hasChanges,
            deleteCustomer: deleteCustomer,
            goBack: goBack,
            // Binding Observables
            customer: customer,
            editCustomer: editCustomer,
            customerEditable: ko.observable(false),
            // UI Methods
            title: 'Customer Details ',

        };

        return vm;
    });