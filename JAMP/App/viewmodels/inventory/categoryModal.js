define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/app',
    'services/model'],
    function (datacontext, router, paging, app, model) {
        var category = ko.observable(),
            isSaving = ko.observable(false),
            isEditing = false,
            result,
            isDeleting = ko.observable(false);

        //#region Durandal Methods
        var activate = function (routeData) {
            if (routeData) {
                category(routeData());
                isEditing = true;
            }
            else {
                isEditing = false;
                category(datacontext.createObject(model.entityNames.category));
            }
            return true;
        };

        //#endregion

        //#region Visible Methods

        var cancel = function (complete) {
            datacontext.cancelChanges();
            this.modal.close(result = 'cancel');
        };

        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        var canSave = ko.computed(function () {
            return hasChanges() && !isSaving();
        });

        //Save changes
        var save = function (reason, msgName) {
            isSaving(true);
            msgName = category().categoryName();

            if (isEditing === true) {
                reason = datacontext.saveReason.update;
            }
            else {
                reason = datacontext.saveReason.add;
            }

            datacontext.saveChanges(reason, msgName)
                .then(complete);

            function complete() {
                isSaving(false);
               
            }
            this.modal.close(result = 'save');
        };

        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            // Visible Methods
            save: save,
            canSave: canSave,
            hasChanges: hasChanges,
            cancel: cancel,
            // Binding Observables
            category: category
        };

        return vm;

    });