define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/system',
    'durandal/app',
    'services/logger'],
    function (datacontext, router, paging, system, app, logger) {

        var id = 0,
            subscription = null,
            customer = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);
            

        //#region Durandal Methods
        var activate = function (routeData) {
            id = parseInt(routeData.id, 10);
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Customers');
        };

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var title = 'Cancel changes made to "' +
                   customer().customerAccount().accountName() + '" ?';
                var msg = 'Navigate away?';

                return app.showMessage(title, msg, ['Yes', 'No'])
                    .then(checkAnswer);
            }
            subscription.dispose();
            vm.accountEditable(false);
            return true;

            function checkAnswer(selectedOption) {
                if (selectedOption === 'Yes') {
                    cancel();
                    subscription.dispose();
                    vm.accountEditable(false);
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
            if (reason != datacontext.saveReason.deletion) {
                reason = datacontext.saveReason.update;
                msgName = customer().customerName();
            }
            return datacontext.saveChanges(reason, msgName).fin(complete);

            function complete() {
                isSaving(false);
            }
        };

        // Delete the current product
        var deleteCustomerAccount = function () {
            var msg = 'Are you sure you want to Delete Customer Account "' + customer().customerAccount().accountName() + '" Currently Owing R'+customer().customerAccount().amountOwing()+' ?';
            var title = 'Confirm Delete';
            isDeleting(true);


            return app.showMessage(msg, title, ['Yes', 'No'])
                .then(confirmDelete);

            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {
                    // Archive customer and their account
                    customer().customerAccount().archived(true);
                    customer().archived(true);
                    customer().archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                    save(datacontext.saveReason.deletion, customer().customerAccount().accountName()).then(success).fail(failed).fin(finish);
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

        // Edit customer details switch
        var editAccount = function () {
            if (vm.accountEditable() === true) {
                return vm.accountEditable(false);
            }
            else {
                return vm.accountEditable(true);
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
            deleteCustomerAccount: deleteCustomerAccount,
            goBack: goBack,
            editAccount: editAccount,
            // Binding Observables
            customer: customer,
            accountEditable: ko.observable(false),
            // UI Methods
            title: 'Customer Account Details '
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh data on the page
        function refreshPage() {
            var where = {
                firstParm: 'customerID',
                operater: '==',
                secondParm: id
            };
            return datacontext.getEntitySingle(customer, datacontext.entityAddress.customer, false, null, where);
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