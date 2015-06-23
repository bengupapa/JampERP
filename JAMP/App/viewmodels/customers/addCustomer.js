define([
    'durandal/app',
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/plugins/router',
    'services/model'],
    function (app, datacontext, router, paging, router, model) {

        var isSaving = ko.observable(false),
            customer = ko.observable(),
            account = ko.observable();

        //#region Durandal Methods
        var activate = function () {
            customer(datacontext.createObject(model.entityNames.customer));                                     // Create Customer
            account(datacontext.createObject(model.entityNames.customerAccount, customer().customerID()));      // Create Customer Account
            return true;
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Customers');
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
            var count = customer().customerSurname().length;
         
            var accountname = customer().customerSurname().substring(0, 2).toUpperCase() + customer().customerName().substring(0, 2).toUpperCase() + customer().customerID() + customer().businessID() + count;
            account().accountName(accountname);
            datacontext.saveChanges(datacontext.saveReason.add, customer().customerName())
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
            customer: customer,
            account: account,
            // UI Methods
            title: 'Add a new Customer'
        };

        return vm;

    });