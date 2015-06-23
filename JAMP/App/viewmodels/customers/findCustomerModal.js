define([
    'services/datacontext',
    'durandal/plugins/router',
    'durandal/app',
    'services/model'],
    function (datacontext, router, app, model) {
        var customerlist = ko.observableArray([]),
            filter = ko.observable(""),
            searchBy = ko.observable(),
            isSaving = ko.observable(false);

        //#region Durandal Methods
        var activate = function (list) {
            customerlist = list;
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

        //Send customer back to incident
        var addCustomer = function (person) {
            vm.modal.close(person);
        };

        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return;
            } else {
                return ko.utils.arrayFilter(customerlist(), function (person) {
                    if (searchBy() == 'First Name') {
                        return ko.utils.stringStartsWith(person.customerName().toLowerCase(), search);
                    } else if(searchBy() == 'Last Name') {
                        return ko.utils.stringStartsWith(person.customerSurname().toLowerCase(), search);
                    }else{
                        return ko.utils.stringStartsWith(person.fullName().toLowerCase(), search);
                    }

                });
            }
        });
       

        var vm = {
            // Durandal Methods
            activate: activate,
            canDeactivate: canDeactivate,
            addCustomer: addCustomer,
            // Visible Methods
            cancel: cancel,
            // Binding Observables
            filteredItems: filteredItems,
            filter: filter,
            searchBy: searchBy
        };

        return vm;
    })