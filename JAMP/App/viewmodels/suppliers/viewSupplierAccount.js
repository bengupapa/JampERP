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
            supplier = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false),
            Predicate = breeze.Predicate;

        //#region Durandal Methods
        var activate = function (routeData) {
            id = parseInt(routeData.id, 10);
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get page data
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
        };

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var title = 'Cancel changes made to "' +
                   supplier().supplierAccount().accountName() + '" ?';
                var msg = 'Navigate away?';

                return app.showMessage(title, msg, ['Yes', 'No'])
                    .then(checkAnswer);
            }
            subscription.dispose();
            return true;

            function checkAnswer(selectedOption) {
                if (selectedOption === 'Yes') {
                    subscription.dispose();
                    cancel();
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
                msgName = supplier().supplierAccount().accountName();
            }
            return datacontext.saveChanges(reason, msgName).fin(complete);

            function complete() {
                isSaving(false);
            }
        };

        // Delete the current product
        var deleteSupplierAccount = function () {
            var msg = '';
            if (supplier().supplierAccount().amountOwed() > 0) {
                msg = 'Are you sure you want to Delete Supplier Account "' + supplier().supplierAccount().accountName() + '" Currently Owed R' + supplier().supplierAccount().amountOwed() + ' and with ' + outstandingorders() + ' number of orders outstanding?';
            } else {
                msg = 'Are you sure you want to delete ' + supplier().supplierName() + '?';
            }
            var title = 'Confirm Delete';
            isDeleting(true);
            return app.showMessage(msg, title, ['Yes', 'No'])
                .then(confirmDelete);

            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {

                    supplier().archived(true);
                    supplier().supplierAccount().archived(true);
                    supplier().archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                    save(datacontext.saveReason.deletion, supplier().supplierAccount().accountName()).then(success).fail(failed).fin(finish);
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

        // Navigate to add a payment
        var transaction = function () {
            var url = '#/Supplier_Account_Payment/' + supplier().supplierID();
            router.navigateTo(url);
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
            deleteSupplierAccount: deleteSupplierAccount,
            goBack: goBack,
            accountEditable: ko.observable(false),
            editAccount: editAccount,
            transaction: transaction,
            // Binding Observables
            supplier: supplier,
            // UI Methods
            title: 'Supplier Account Details ',

        };

        return vm;

        //#region Internal Helper Methods
        // Calculate
        function outstandingorders() {
            var count = 0;
            ko.utils.arrayForEach(supplier().orders(), function (order) {
                count += order.completed() != 'Completed' ? 1 : 0;
            });
            return count;
        }

        // Refresh the page data
        function refreshPage() {
            var where = Predicate.create('supplierID', '==', id);

            return datacontext.getEntitySingle(supplier, datacontext.entityAddress.supplier, false, null, Predicate.and([where]));
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().supplier()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion
    });


