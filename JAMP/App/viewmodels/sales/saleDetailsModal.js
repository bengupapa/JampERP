define(['services/datacontext',
    'durandal/plugins/router',
    'durandal/app',
    'services/model'],
    function (datacontext, router, app, model) {
        var sale = ko.observable();

        //#region Durandal Methods
        var activate = function (saleData) {

            return sale(saleData);
        };
        //#endregion

        //#region Visible Methods
        var cancel = function () {
            this.modal.close();
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            // Visible Methods
            cancel: cancel,
            // Binding Observables
            sale: sale
        };

        return vm;



    })