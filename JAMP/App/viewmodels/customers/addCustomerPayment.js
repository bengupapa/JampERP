define([
    'durandal/app',
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'services/model'],
    function (app, datacontext, router, paging, model) {

        var customer = ko.observable(),
            payments = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);

        //#region Durandal Methods
        var activate = function (routeData) {
            var id = parseInt(routeData.id, 10);
            datacontext.getEntityDetails(id, model.entityNames.customer, customer);      // Get customer
            payments(datacontext.createObject(model.entityNames.customerPayment));      // Create payment entity
            return true;
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Customers');
        };

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var title = 'Cancel changes made to "' +
                   customer().customerName() + '"?';
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

        //#region Visible Methods

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
        var save = function () {
            isSaving(true);
            // Saving toastr info
            reason = datacontext.saveReason.update;
            msgName = customer().customerAccount().accountName();

            // Update entities properties
            payments().customerID(customer().customerID());
            total = customer().customerAccount().amountOwing() - payments().amountPaid();
            customer().customerAccount().amountOwing(total);

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
            customer: customer,
            payments: payments,
            // UI Methods
            title: 'Customer Payment'
        };

        return vm;
    });