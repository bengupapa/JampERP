define([
    'durandal/app',
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/plugins/router',
    'services/model'],
    function (app, datacontext, router, paging, router, model) {

        var account = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);
            payments = ko.observable();


        //#region Durandal Methods
        var activate = function (routeData) {
            var id = parseInt(routeData.id);
            datacontext.getEntityDetails(id, model.entityNames.supplier, account);      // Get supplier Account
            payments(datacontext.createObject(model.entityNames.supplierPayment));      // Create a payment entity
            return true;
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');

        }

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var title = 'Cancel changes made to "' +
                   account().supplierName() + '" ?';
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
                msgName = account().supplierName();
            }
            id = account().supplierID();
            payments().supplierID(id);
            total = account().supplierAccount().amountOwed() - payments().amountPaid();
            account().supplierAccount().amountOwed(total)
            return datacontext.saveChanges(reason, msgName).fin(complete);

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
            cancel: cancel,
            canSave: canSave,
            save: save,
            hasChanges: hasChanges,
            goBack: goBack,
            // Binding Observables
            account: account,
            payments: payments,
            // UI Methods
            title: 'Pay Supplier account'
        };

        return vm;
    });