define([
    'services/datacontext',
    'services/logger',
    'durandal/plugins/router',
    'services/model',
    'durandal/app'],
    function (datacontext, logger, router, model, app) {

        var subscription = null,
            salelist = ko.observableArray([]),
            specials = ko.observableArray([]),
            userSettings = ko.observable(),
            sale = ko.observable();


        //#region Durandal Methods
        var activate = function () {
            // Get the current users settings
            userSettings(datacontext.currentUser().settings());
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get the page data
            return refreshPage();
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            bindEventToList(view, '.infoSales', infoSales);
            bindEventToList(view, '.infoSpecials', infoSpecials);
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods

        // View sales details
        var viewDetails = function (selectedSale) {
            app.showModal('viewmodels/sales/saleDetailsModal', selectedSale);
        };       
        
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            viewDetails: viewDetails,
            // Binding Observables
            salelist: salelist,
            specials: specials,
            userSettings: userSettings,
            // UI Methods
            title: 'Sales'
        };

        return vm;

        //#region Internal Helper Methods

        // Modal Specials
        function infoSpecials() {
            var title = 'Specials';
            var msg = 'Products Currently on Special';
            app.showMessage(msg, title, ['Ok']);
        }

        // Modal Sales Info
        function infoSales() {
            var title = 'Sale Details';
            var msg = 'Click on the sale in the table to view more details';
            app.showMessage(msg, title, ['Ok']);
        }

        // Bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        // Refresh data on the page
        function refreshPage() {
            var where = {
                firstParm: 'special',
                operater: '==',
                secondParm: 'Yes'
            };

            return Q.all([
                datacontext.getEntityList(specials, datacontext.entityAddress.product, false, null, where),
                datacontext.getEntityList(salelist, datacontext.entityAddress.sale, false, 'Employee', null, 10, null, null, 'createdDate desc')]);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().product()) {
                    refreshPage();
                    datacontext.updateList().product(false);
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion

    });