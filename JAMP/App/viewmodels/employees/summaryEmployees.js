define([
    'services/logger',
    'services/datacontext',
    'services/model',
    'durandal/plugins/router', ],
    function (logger, datacontext, model, router) {

        var subscription = null,
            employees = ko.observableArray([]),
            filter = ko.observable("");

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            bindEventToList(view, '.item-info-con', gotoDetails);
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods

        // Filter Employees
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return employees();
            } else {
                return ko.utils.arrayFilter(employees(), function (item) {
                    return ko.utils.stringStartsWith(item.fullName().toLowerCase(), search);
                });
            }
        });

        // Check filter is entered and empty
        var emptyList = ko.computed(function () {
            return (filteredItems() == 0) && (filter() != "");
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            emptyList: emptyList,
            // Binding Observables
            filter: filter,
            filteredItems: filteredItems,
            // UI Methods
            title: 'Employees'
        };

        return vm;

        //#region Internal Helper Methods

        // Bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        // Go to employee Details
        function gotoDetails(selectedEmployee) {
            if (selectedEmployee && selectedEmployee.employeeID()) {
                var url = '#/Employee_Details/' + selectedEmployee.employeeID();
                router.navigateTo(url);
            }
        }

        // Refresh Page
        function refreshPage() {
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: false
            };
            filter('');
            return datacontext.getEntityList(employees, datacontext.entityAddress.employee, false, null, where, null, null, null, 'employeeID');
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().employee()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion
    });