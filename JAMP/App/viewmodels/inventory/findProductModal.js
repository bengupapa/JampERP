define([
    'services/datacontext',
    'durandal/plugins/router',
    'durandal/app',
    'services/model'],
    function (datacontext, router, app, model) {
        var result,
            filter = ko.observable(""),
            searchBy = ko.observable(),
            productList = ko.observableArray([]);

        //#region Durandal Methods
        var activate = function (list) {
            productList = list;
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

        //Send product back to incident
        var addProduct = function (item) {           
            vm.modal.close(item);
        };

        //Filter Products
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return;
            } else {
                return ko.utils.arrayFilter(productList(), function (item) {
                    if (searchBy() == 'Brand And Product Name') {
                        return ko.utils.stringStartsWith(item.fullName().toLowerCase(), search);
                    } else if (searchBy() == 'Brand') {
                        return ko.utils.stringStartsWith(item.brandName().toLowerCase(), search);
                    }
                    else {
                        return ko.utils.stringStartsWith(item.productName().toLowerCase(), search);
                    }
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
            addProduct: addProduct,
            // Binding Observables
            filteredItems: filteredItems,
            filter: filter,
            searchBy: searchBy
        };

        return vm;

    });