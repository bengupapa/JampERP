define([
    'services/datacontext',
    'durandal/plugins/router',
    'durandal/app',
    'services/model'],
    function (datacontext, router, app, model) {

        var sale = ko.observable(),
            result,
            isSaving = ko.observable(false);

        //#region Durandal Methods
        var activate = function (saleData) {
            return sale(saleData());
        };

        //#endregion

        //#region Visible Methods
        var cancel = function () {
            this.modal.close();
        };

        //Save changes
        var save = function () {
            isSaving(true);

            //Decrease quantity on the products before saving
            ko.utils.arrayForEach(sale().salesItems(), function (salesItem) {
                salesItem.product().quantity(salesItem.product().quantity() - salesItem.quantity());
            });

            // Update customer balance
            if (sale().customer())
            {
                sale().customer().customerAccount().amountOwing(sale().customer().customerAccount().amountOwing() + sale().amountCharged());
            }

            datacontext.saveChanges(datacontext.saveReason.add, model.entityNames.sale)
                .then(complete);

            function complete() {
                isSaving(false);
                vm.modal.close(result = 'save');
            }
            
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            // Visible Methods
            cancel: cancel,
            save: save,
            // Binding Observables
            sale: sale,
        };

        return vm;
    })