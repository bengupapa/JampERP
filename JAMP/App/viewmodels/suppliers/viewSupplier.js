define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/system',
    'durandal/app',
    'services/logger',
    'services/model'],
    function (datacontext, router, paging, system, app, logger, model) {
        var supplier = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);

        //#region Durandal Methods
        var activate = function (routeData) {
            var id = parseInt(routeData.id, 10);
            return datacontext.getEntityDetails(id, model.entityNames.supplier, supplier);
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
        };

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var title = 'Cancel changes made to "' +
                   supplier().supplierName() + '" ?';
                var msg = 'Navigate away?';

                return app.showMessage(title, msg, ['Yes', 'No'])
                    .then(checkAnswer);
            }
            return true;

            function checkAnswer(selectedOption) {
                if (selectedOption === 'Yes') {
                    cancel();
                }
                return selectedOption;
            }
        };
        //#endregion

        //#region Visible methods

        //Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        //Observable has changes
        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        // Edit Supplier details switch
        var editSupplier = function () {
            if (vm.supplierEditable() === true) {
                return vm.supplierEditable(false);
            }
            else {
                return vm.supplierEditable(true);
            }
        };

        //Cancel any changes made
        var cancel = function () {
            datacontext.cancelChanges();
        };

        //Check is there are changes
        //and object is not already saving
        var canSave = ko.computed(function () {
            return hasChanges() && !isSaving();
        });

        //Save changes
        var save = function (reason, msgName) {
            isSaving(true);
            if (reason != datacontext.saveReason.deletion) {
                reason = datacontext.saveReason.update;
                msgName = supplier().supplierName();
            }
            return datacontext.saveChanges(reason, msgName).fin(complete);

            function complete() {
                isSaving(false);
            }
        };

        //Delete supplier
        var deleteSupplier = function () {
            var msg = '';
            if (supplier().supplierAccount().amountOwed() > 0) {
                msg = 'Are you sure you want to Delete Supplier Account "' + supplier().supplierAccount().accountName() + '" Currently Owed R' + supplier().supplierAccount().amountOwed() + '?';
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
                    save(datacontext.saveReason.deletion, supplier().supplierName()).then(success).fail(failed).fin(finish);
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
            goBack: goBack,
            cancel: cancel,
            canSave: canSave,
            save: save,
            hasChanges: hasChanges,
            deleteSupplier: deleteSupplier,
            // Binding Observables
            supplier: supplier,
            editSupplier: editSupplier,
            supplierEditable:ko.observable(false),
            // UI Methods
            title: 'Supplier Details'
        };

        return vm;
    });