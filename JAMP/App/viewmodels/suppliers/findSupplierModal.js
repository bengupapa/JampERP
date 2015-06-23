define([
    'services/datacontext',
    'durandal/plugins/router',
    'durandal/app',
    'services/model'],
    function (datacontext, router, app, model) {
        var result,
            filter = ko.observable(""),
            searchBy = ko.observable(),
            supplierList = ko.observableArray([]);

        //#region Durandal Methods
        var activate = function (list) {
            supplierList = list;
            return true;
        };

        var canDeactivate = function () {
            filter("");
            return true;
        };
        //#endregion

        //#region Visible Methods

        var cancel = function () {
            this.modal.close(result = 'cancel');
        };

        //Send supplier back
        var addSupplier = function (item) {
            vm.modal.close(item);
        };

        //Filter Supplier
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return;
            } else {
                return ko.utils.arrayFilter(supplierList(), function (item) {
                    return ko.utils.stringStartsWith(item.supplierName().toLowerCase(), search);
                });
            }
        });

        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            canDeactivate: canDeactivate,
            // Visible Methods
            cancel: cancel,
            addSupplier: addSupplier,
            // Binding Observables
            filteredItems: filteredItems,
            filter: filter,
            searchBy: searchBy
        };

        return vm;

    });