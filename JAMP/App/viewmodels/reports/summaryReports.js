define([
    'services/datacontext',
    'durandal/app',
    'services/logger',
    'durandal/plugins/router',
    'services/model',
    'services/paging'],
    function (datacontext, app, logger, router, model, paging, highcharts) {
        //declaring all the variables that will hold information for the month
        var julysales = ko.observable(),
            julycosts = ko.observable(),
            augustsales = ko.observable(),
            augustcost = ko.observable(),
            septembersales = ko.observable(),
            septembercost = ko.observable(),
            octobersales = ko.observable(),
            octoberosts = ko.observable(),
            Predicate = breeze.Predicate,
            //declaring the lists that contain the objects for the specific month
            julysalesList = ko.observableArray(),
            junesaleList = ko.observableArray(),
            augustsaleList = ko.observableArray(),
            septembersaleList = ko.observableArray(),
            octobersaleList = ko.observableArray(),
            userSettings = ko.observable();


        // creating predicates that define the date to sort the entities apart
        var Junestart = Predicate.create("createdDate", '>', '2013-06-01, 08:00 am');
        var Juneend = Predicate.create("createdDate", '<=', '2013-06-30, 11:59 pm');
        var Julystart = Predicate.create("createdDate", '>', '2013-07-01, 08:00 am');
        var Julyend = Predicate.create("createdDate", '<=', '2013-07-30, 11:59 pm');
        var Auguststart = Predicate.create("createdDate", '>', '2013-08-01, 08:00 am');
        var Augustend = Predicate.create("createdDate", '<=', '2013-08-30, 11:59 pm');
        var Septemberstart = Predicate.create("createdDate", '>', '2013-09-01, 08:00 am');
        var Septemberend = Predicate.create("createdDate", '<=', '2013-09-30, 11:59 pm');
        var Octoberstart = Predicate.create("createdDate", '>', '2013-10-01, 08:00 am');
        var Octoberend = Predicate.create("createdDate", '<=', '2013-10-30, 11:59 pm');



        //#region Durandal Methods
        var activate = function () {
            //getting the data from the local cached object and using a predicate to determine the date of the objects
            //datacontext.getEntityList(julysalesList, datacontext.entityAddress.sale, false, null, Predicate.and([Julystart, Julyend]))
            datacontext.getEntityList(augustsaleList, datacontext.entityAddress.sale, false, null, Predicate.and([Auguststart, Augustend]))
            datacontext.getEntityList(septembersaleList, datacontext.entityAddress.sale, false, null, Predicate.and([Septemberstart, Septemberend]))
            datacontext.getEntityList(octobersaleList, datacontext.entityAddress.sale, false, null, Predicate.and([Octoberstart, Octoberend]))
                .then(setup());

            //Set up is the function that is used to call the totals. gets given all the parameters of the objects to create a dynamic function
            function setup() {
                //totals(julysalesList, julysales, julycosts)
                totals(augustsaleList, augustsales, augustcost);
                totals(septembersaleList, septembersales, septembercost);
                totals(octobersaleList, octobersales, octoberosts);
            };


            //function accepts in the list of objects, the total amount owed by the customers, and the total cost of the accounts in the system
            function totals(saleslist, observable, costobservable) {
                var salesfigure = 0;
                var totalcost = 0;
                var decimalFix = 0;
                for (var i = 0; i <= saleslist().length - 1; i++) {
                    salesfigure += saleslist()[i].amountCharged();
                    decimalFix = Math.round(salesfigure);
                    observable(decimalFix);

                    totalcost += cost(saleslist()[i].salesItems);
                    decimalFix = Math.round(totalcost);
                    costobservable(decimalFix);

                }
                return true;
            };


            function cost(saleitems) {
                var totalSalesCost = 0;
                for (var a = 0; a <= saleitems().length-1; a++)
                {
                    totalSalesCost += saleitems()[a].costPrice();
                }
                return totalSalesCost;
            };

            // Get user settings
            userSettings(datacontext.currentUser().settings());

            return true;
        }

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }

            bindEventToList(view, '.infoSales', infoSales);
            bindEventToList(view, '.infoSupplier', infoSupplier);
            bindEventToList(view, '.infoInventory', infoInventory);
            bindEventToList(view, '.infoCustomers', infoCustomers);
            bindEventToList(view, '.infoProfitMargin', infoprofitMargin);


            // renders a graph showing total revenue against costs
            $('#totalSales', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Revenue vs Total Costs' },

                xAxis: [{
                    categories: [ 'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    max: 15000,
                    labels: {
                        rotation: 'auto',
                    },
                    gridLineWidth: 2,
                    title: {
                        // need to change the style.
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
                    data: [ augustsales(), septembersales(), octobersales()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Revenue Received',
                    type: 'line',
                    color: '#01A7E2'
                },
                {
                    data: [ augustcost(), septembercost(), octoberosts()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Cost Received',
                    type: 'line',
                    color: '#FF0000'
                }]

            });

        }

        //#region Helper methods
        //#endregion

        var vm = {
            activate: activate,
            viewAttached:viewAttached,
            title: 'Reports',
            julysalesList: julysalesList,
            julysales: julysales,
            augustsaleList: augustsaleList,
            septembersaleList: septembersaleList,
            userSettings: userSettings

        };

        return vm;

        function infoSales() {
            var title = 'Sales Reports';
            var msg = 'Reporting doe on the sales of the business. Allows to view performance based on months';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoInventory() {
            var title ='Inventory Reporting';
            var msg = 'Reporting on the inventory within the business. Performance as well as general stats';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoSupplier() {
            var title = 'Supplier Reporting';
            var msg = 'Reports on the supplier sub system. Shows some stats as well as performance of supplier';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoCustomers() {
            var title = 'Customer Reporting';
            var msg = 'Reports on the customers as well as their account. Some general stats for the business';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoprofitMargin() {
            var title = 'Profit Margin';
            var msg = 'This depics the Profit margin of the business over the months';
            app.showMessage(msg, title, ['Ok']);
        }

        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

    });