define([
    'durandal/app',
    'services/logger',
    'durandal/plugins/router',
    'services/datacontext'],
    function (app, logger, router, datacontext) {

        var user = ko.observable(),
            isSaving = ko.observable(false);

        //#region Durandal Methods
        var activate = function () {
            return user(datacontext.currentUser());
        }

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
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
        //#endregion

        //#region Visible Methods

        // Changes in the entity manager
        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        // Force a refresh of the local data
        var forceDataRefresh = function () {
            window.loggingOff = true;
            location.reload();
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

        // Save changes in entity manager
        var save = function () {
            isSaving(true);
            datacontext.saveChanges(datacontext.saveReason.update, 'Settings')
                .fin(complete);

            function complete() {
                isSaving(false);
                router.navigateBack();
            }
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            forceDataRefresh: forceDataRefresh,
            hasChanges: hasChanges,
            cancel: cancel,
            save: save,
            canSave: canSave,
            // Binding Observables
            user: user,
            // UI Methods
            title: 'Settings'
        };

        return vm;

    });