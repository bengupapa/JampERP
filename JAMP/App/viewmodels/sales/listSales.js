define([
    'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {

        var subscription = null,
            salelist = ko.observableArray([]),
            date = ko.observable("");

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            //Order by latest
            return datacontext.getEntityList(salelist, datacontext.entityAddress.sale, false, null, null, null, null, null, 'createdDate desc');
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Sales');
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods
        var goBack = function () {
            router.navigateBack();
            date("");
        };

        // Sorting of the sale by date
        var sortedSales = ko.computed(function () {
            if (date()) {
                var searchdate = date().toLowerCase();
                if (!searchdate) {
                    return null;
                }
                else {
                    return ko.utils.arrayFilter(salelist(), function (sale) {
                        return ko.utils.stringStartsWith(sale.createdDate().toLowerCase(), searchdate);
                    });
                }
            }
        });

        // Go to sale details
        var goToSale = function (selectedSale) {
            app.showModal('viewmodels/sales/saleDetailsModal', selectedSale);
        };

        // Calculate the total value
        var total = ko.computed(function () {
            var totalamount = 0;

            if (sortedSales() == null) {
                totalamount = 0;
            } else {
                for (i = 0; i < sortedSales().length; i++) {
                    totalamount += sortedSales()[i].amountCharged();
                }
                return totalamount.toFixed(2);
            }
            
        });
       
        // Count the number of sales
        var listnumber = ko.computed(function () {
            var number = 0;
            if (sortedSales() == null) {
                number = 0;
            } else {
                number = sortedSales().length;
            }
            return number;
        });

        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            goBack: goBack,
            goToSale: goToSale,
            // Binding Observables
            sortedSales: sortedSales,
           listnumber: listnumber,
            date: date,
            total: total,
            // UI Methods
            title: 'Sales'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh the page data
        function refreshPage() {
            return datacontext.getEntityList(salelist, datacontext.entityAddress.sale, false, null, null, null, null, null, 'saleID desc');
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().sale()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }
        //#endregion

    });