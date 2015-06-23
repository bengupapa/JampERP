define([
    'services/logger',
    'services/model',
    'services/datacontext',
    'durandal/app' ],
    function (logger, model, datacontext, app) {

        var subscription = null,
            employees = ko.observableArray([]),
            salelist = ko.observableArray([]),
            userSettings = ko.observable();

        var seven = 0,
            eight = 0,
            nine = 0,
            ten = 0,
            elven = 0,
            twelf = 0,
            one = 0,
            two = 0,
            three = 0,
            four = 0,
            five = 0,
            six = 0;


        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);

            // Get user settings
            userSettings(datacontext.currentUser().settings());

            // Get page data
            return refreshPage();
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }

            bindEventToList(view, '.infoHourlySales', infoHourlySales);


            $('#totalsSalesPerHour', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Sales per Hour' },

                xAxis: [{
                    categories: ['7', '8', '9', '10', '11', '12', '1', '2', '3', '4', '5', '6'],
                    title: {
                        text: 'Hour of the day',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    labels: {
                        rotation: 'auto',
                    },
                    gridLineWidth: 2,
                    title: {
                        text: 'Total Rands',
                        y: 600
                    }
                }],

                plotOptions: {
                    line: {
                        dataLabels: { enabled: true },
                    }
                },
                series: [{
                    data: [seven, eight, nine, ten, elven, twelf, one, two, three, four, five, six],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Amount Sales',
                    type: 'column',
                    color: '#88C32F'
                }]

            });
            //init = true;
        };

        var canDeactivate = function () {
            subscription.dispose();

            seven = 0,
            eight = 0,
            nine = 0,
            ten = 0,
            elven = 0,
            twelf = 0,
            one = 0,
            two = 0,
            three = 0,
            four = 0,
            five = 0,
            six = 0;
            return true;
        };
        //#endregion

        //#region Visible Methods
        // Get the current user
        var user = ko.computed(function () {
            return datacontext.currentUser();
        });

        // Income earned
        var incomeSales = ko.computed(function () {
            var amount = 0;
            if (salelist) {
                ko.utils.arrayForEach(salelist(), function (sale) {
                    amount += sale.amountCharged();
                });
                return amount == 0 ? 0 : amount.toFixed(2);
            }
            return amount;
        });

        // Cost of the sales
        var costSales = ko.computed(function () {
            var amount = 0;
            if (salelist) {
                ko.utils.arrayForEach(salelist(), function (sale) {
                    ko.utils.arrayForEach(sale.salesItems(), function (saleItem) {
                        amount += saleItem.quantity() * saleItem.costPrice();
                    });
                });
                return amount == 0 ? 0 : amount.toFixed(2);
            }
            return amount;
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Binding Observables
            user: user,
            employees: employees,
            salelist: salelist,
            incomeSales: incomeSales,
            costSales: costSales,
            userSettings:userSettings,
            // UI Methods
            title: 'Dashboard'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh Page
        function refreshPage() {
            var today = moment().format("YYYY-MM-DD"),
             where = {
                 firstParm: 'archived',
                 operater: '==',
                 secondParm: false
             },
             att = {
                 firstParm: 'createdDate',
                 operater: 'startsWith',
                 secondParm: today
             };

            return Q.all([
                datacontext.getEntityList(employees, datacontext.entityAddress.employee, false, null, where, null, null, null, 'employeeID'),
                datacontext.getEntityList(salelist, datacontext.entityAddress.sale, false, null, att)]).then(calcualtions);

            // Calculate 
            function calcualtions() {
                return ko.utils.arrayForEach(salelist(), function (item) {
                    var hour = moment(item.createdDate(), "YYYY-MM-DD, h:mm a").hours();
                    switch (hour) {
                        case 7:
                            seven += item.amountCharged()
                            break;
                        case 8:
                            eight += item.amountCharged()
                            break;
                        case 9:
                            nine += item.amountCharged()
                            break;
                        case 10:
                            ten += item.amountCharged()
                            break;
                        case 11:
                            elven += item.amountCharged()
                            break;
                        case 12:
                            twelf += item.amountCharged()
                            break;
                        case 13:
                            one += item.amountCharged()
                            break;
                        case 14:
                            two += item.amountCharged()
                            break;
                        case 15:
                            three += item.amountCharged()
                            break;
                        case 16:
                            four += item.amountCharged()
                            break;
                        case 17:
                            five += item.amountCharged();
                            console.log(five)
                            break;
                        case 18:
                            six += item.amountCharged();
                            break;
                    }
                });
            }
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().sale() || datacontext.updateList().employee()) {
                    refreshPage().then(refreshGraph);
                }
                datacontext.completedRefresh();
            }
        }

        // Refresh the graph
        function refreshGraph() {
            var chart = $('#totalsSalesPerHour').highcharts();
            chart.series[0].setData([seven, eight, nine, ten, elven, twelf, one, two, three, four, five, six], true);
        }

        // Modal hourly Sales Info
        function infoHourlySales() {
            var title = 'Hourly Stats';
            var msg = 'This shows the Hourly sales made by the business. Reads as the starting hour to the subsequent hour';
            app.showMessage(msg, title, ['Ok']);
        }

        // bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        //#endregion
    });