define([
    'durandal/app',
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/plugins/router',
    'services/model'],
    function (app, datacontext, router, paging, router, model) {

        var isSaving = ko.observable(false),
            supplier = ko.observable(),
            account = ko.observable();

        //#region Durandal Methods
        var activate = function () {
            supplier(datacontext.createObject(model.entityNames.supplier));                                 // Create Supplier
            account(datacontext.createObject(model.entityNames.supplierAccount, supplier().supplierID()));  // Create Supplier Account
            return true;
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
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
        var goBack = function () {
            router.navigateBack();
        };

        var cancel = function (complete) {
            router.navigateBack();
        };

        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        var canSave = ko.computed(function () {
            return hasChanges() && !isSaving();
        });

        var save = function () {
            isSaving(true);
            var count = supplier().supplierName().length;

            var accountname = supplier().supplierName().substring(0, 2).toUpperCase() + supplier().supplierID() + supplier().businessID() + count;
            account().accountName(accountname);
            datacontext.saveChanges(datacontext.saveReason.add, supplier().supplierName())
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
            canSave: canSave,
            cancel: cancel,
            hasChanges: hasChanges,
            save: save,
            goBack: goBack,
            // Binding Observables
            supplier: supplier,
            account: account,
            // UI Methods
            title: 'Add a new Supplier'
        };

        return vm;

    });