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
            employee = ko.observable(),
            user = ko.observable(),
            categoryList = ko.observableArray([]),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(true),
            canDelete = ko.observable();

        //#region Durandal Methods
        var activate = function (routeData) {
            id = parseInt(routeData.id, 10);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Employees');
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                var msg = 'Cancel changes made to "' +
                   employee().fullName() + '" ?';
                var title = 'Navigate away?';

                return app.showMessage(msg, title, ['Yes', 'No'])
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
                msgName = employee().fullName();
            }
            return datacontext.saveChanges(reason, msgName)
                .fin(complete);

            function complete() {
                isSaving(false);
            }
        };

        // Delete the employee
        var deleteEmployee = function () {
            var msg = 'Delete employee "' + employee().fullName() + '" ?';
            var title = 'Confirm Delete';
            isDeleting(true);
            return app.showMessage(msg, title, ['Yes', 'No'])
                .then(confirmDelete);

            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {
                    employee().archived(true);
                    employee().archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                    save(datacontext.saveReason.deletion, employee().fullName())
                        .then(success)
                        .fail(failed)
                        .fin(finish);
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
            deleteEmployee: deleteEmployee,
            goBack: goBack,
            // Binding Observables
            employee: employee,
            // UI Methods
            title: 'Employee Details'
        };

        return vm;

        //#region Internal Helper Methods
        // Refresh the page
        function refreshPage() {
            return datacontext.getEntityDetails(id, model.entityNames.employee, employee);
        }
        //#endregion
    });