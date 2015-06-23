define([
    'durandal/app',
    'services/model',
    'services/logger',
    'durandal/plugins/router',
    'durandal/system',
    'services/datacontext'],
    function (app, model, logger, router, system, datacontext) {

        var user = datacontext.currentUser,
            isSaving = ko.observable(false),
            businessEditable = ko.observable(false);

        //#region Durandal Methods
        var activate = function () {
            return true;
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                var msg = 'Do you want to leave and cancel?';
                return app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            datacontext.cancelChanges();
                        }
                        return selectedOption;
                    });
            }
            return true;
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
        };
        //#endregion

        //#region Visible methods

        // Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        // Check if owner
        var canEditBusiness = ko.computed(function () {
            if (user().roleName() === 'Owner') {
                return true;
            }
            else { return false; }
        });

        // Edit business details switch
        var editBusiness = function () {
            if (businessEditable() === true) {
                return businessEditable(false);
            }
            else {
                return businessEditable(true);
            }
        };

        // Changes exist
        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        //Cancel any changes made
        var cancel = function () {
            datacontext.cancelChanges();
        };

        // Check changes to save business
        var canSaveBusiness = ko.computed(function () {
            return hasChanges() && !isSaving() && businessEditable();
        });

        // Save changes
        var save = function (reason, msgName) {
            isSaving(true);
            if (businessEditable()) {
                msgName = user().business().businessName();
            } else { msgName = user().firstName(); }

            reason = datacontext.saveReason.update;

            return datacontext.saveChanges(reason, msgName).fin(complete);

            function complete() {
                isSaving(false);
            }
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            canEditBusiness: canEditBusiness,
            canSaveBusiness: canSaveBusiness,
            editBusiness: editBusiness,
            goBack: goBack,
            save: save,
            cancel: cancel,
            // Binding Observables
            user: user,
            businessEditable: businessEditable,
            // UI Methods
            title: 'Profile'
        };

        return vm;
    });