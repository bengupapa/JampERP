define([
    'services/datacontext',
    'durandal/app',
    'services/logger',
    'durandal/plugins/router',
    'services/model',
    'services/paging'],
    function (datacontext, app, logger, router, model, paging, highcharts) {
        //declaring the different variables that are used to extract information from the object based on the months
        var julysupplier = ko.observable(),
            julyamount = ko.observable(),
            julyorders = ko.observable(),
            julyOverdue = ko.observable(),
            julyPartial = ko.observable(),
            julyOrderTotal = ko.observable(),
            augustsupplier = ko.observable(),
            augustamount = ko.observable(),
            augustorders = ko.observable(),
            augustOverdue = ko.observable(),
            augustPartial = ko.observable(),
            augustOrderTotal = ko.observable(),
            septembersupplier = ko.observable(),
            septemberamount = ko.observable(),
            septemberorders = ko.observable(),
            septemberOverdue = ko.observable(),
            septemberPartial = ko.observable(),
            septemberOrderTotal = ko.observable(),
            octobersupplier = ko.observable(),
            octoberamount = ko.observable(),
            octoberorders = ko.observable(),
            octoberOverdue = ko.observable(),
            octoberPartial = ko.observable(),
            octoberOrderTotal = ko.observable(),

            //predicate is the nested where statement used to filter
            Predicate = breeze.Predicate,
            //list of objects that are filtered through
            suppliers = ko.observableArray();
            julysupplierList = ko.observableArray(),
            junesupplierList = ko.observableArray(),
            augustsupplierList = ko.observableArray(),
            septembersupplierList = ko.observableArray(),
            octobersupplierList = ko.observableArray(),
            userSettings = ko.observable();


        //the predicates thate are used to filter objects into different lists
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

            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            //getting the data from the local cache for the duifferent months
            //datacontext.getEntityList(julysupplierList, datacontext.entityAddress.supplier, false, null, Predicate.and([Julystart, Julyend]))
            datacontext.getEntityList(augustsupplierList, datacontext.entityAddress.supplier, false, null, Predicate.and([Auguststart, Augustend]))
            datacontext.getEntityList(septembersupplierList, datacontext.entityAddress.supplier, false, null, Predicate.and([Septemberstart, Septemberend]))
            datacontext.getEntityList(octobersupplierList, datacontext.entityAddress.supplier, false, null, Predicate.and([Octoberstart, Octoberend]))
                .then(setup());

            //Dynamic function that is used to to call totals which extracts all the information from the objets
            function setup() {
                //totals(julysupplierList, julysupplier, julyamount, julyorders, julyOrderTotal, julyOverdue, julyPartial )
                totals(augustsupplierList, augustsupplier, augustamount, augustorders, augustOrderTotal, augustOverdue, augustPartial);
                totals(septembersupplierList, septembersupplier, septemberamount, septemberorders, septemberOrderTotal, septemberOverdue, septemberPartial);
                totals(octobersupplierList, septembersupplier, octoberamount, octoberorders, octoberOrderTotal, octoberOverdue, octoberPartial);
                console.log(julysupplierList().length);
            };

            function totals(supplierlist, observable, totalobservable, ordersobservable, orderCosts, overdueOrders, partialOrders) {
                //declaring refernce variables that will be used in calculating the totals from the objects
                var amountowedfigure = 0;
                var totalsupplier = 0;
                var totalorders = 0;
                var totalOrderCost = 0;
                for (var i = 0; i <= supplierlist().length - 1; i++) {
                    amountowedfigure += supplierlist()[i].supplierAccount().amountOwed();
                    totalobservable(amountowedfigure);
                    totalsupplier += totalpayments(supplierlist()[i].supplierAccount().supplierPayments);
                    observable(totalsupplier);
                    totalorders += supplierlist()[i].orders().length;
                    ordersobservable(totalorders);
                    totalOrderCost += totalCost(supplierlist()[i].orders, overdueOrders, partialOrders);
                    orderCosts(totalOrderCost);
                }


                // Get user settings
                userSettings(datacontext.currentUser().settings());
                return true;


            };


            function totalpayments(payments) {
                // total payments that are made to the supplier
                var totalPayments = 0;
                for (var a = 0; a <= payments().length - 1; a++) {
                    totalPayments += payments()[a].amountPaid();
                }
                return totalPayments;
                //return all the total amount paid to the supplier
            };

            function totalCost(payments, ordersOverdue, partialCompleted) {
                //filters out to find which orders are completed or still need to be completed as well as the total amount of the order cost
                var overdue = 0;
                var partial = 0;
                var totalCosting = 0;
                var b = 0;
                console.log(b)
                for (var a = 0; a <= payments().length - 1; a++) {
                    totalCosting += payments()[a].totalCost();
                    if (payments()[a].completed() != "Completed") {
                        overdue += 1;
                        console.log("not completed")
                    };
                    if (payments()[a].completed() == "Completed") {
                        partial += 1;
                    };
                }
                ordersOverdue(overdue);
                partialCompleted(partial);
                return totalCosting;
                //returns the cost of the orders in the object
            };
            return true;
        }

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            paging.setActive(true, 'Reports');


            bindEventToList(view, '.infoSupplierMargin', infoSupplierMargin);
            bindEventToList(view, '.infoOrderCost', infoOrderCost);
            bindEventToList(view, '.infoOrderStats', infoOrderStats);
            bindEventToList(view, '.infoSupplierStats', infoSupplierStats);

            //grapgh showing the amount paid to suppliers and the amount owed to suppliers
            $('#totalsSupplier', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Amount Owed vs Total Amount Paid' },

                xAxis: [{
                    categories: [ 'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: 3000,
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
                    data: [augustamount(), augustamount() + septemberamount(), augustamount() + septemberamount()+octoberamount() ],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Amount Owed',
                    type: 'line',
                    color:'#88C32F'
                },
                {
                    data: [augustsupplier(), augustsupplier() + septembersupplier(), augustsupplier() + septembersupplier()+octobersupplier()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Amount Paid',
                    type: 'line',
                    color: '#9F000F'
                }]

            });
            //graph showing the amount the order costa fopr the month
            $('#NumberSuppliers', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Cost of Orders' },

                xAxis: [{
                    categories: [ 'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: 100,
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
                series: [
                  {
                    data: [ augustOrderTotal(), septemberOrderTotal(), octoberOrderTotal()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Order Cost',
                    type: 'line',
                    color: '#88C32F'
                }]

            });

            //graph that shows the number of orders that have been made against those recieved and those that are yet to be recieved. Also shows the total number of suppliers
            $('#NumberOrdersSuppliers', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Orders vs Total Delivered vs Total Overdue' },

                xAxis: [{
                    categories: [  'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: 8,
                    labels: {
                        rotation: 'auto',
                    },
                    gridLineWidth: 2,
                    title: {
                        // need to change the style.
                        text: 'Totals',
                        y: 600
                    }
                }],

                plotOptions: {
                    line: {
                        dataLabels: { enabled: true },
                    }
                },
                series: [
                {
                    data: [ augustorders(), septemberorders(), octoberorders()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Orders',
                    type: 'column'
                }, {
                    data: [augustOverdue(), septemberOverdue(), octoberOverdue()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'OverDue Orders',
                    type: 'column'
                }, {
                    data: [ augustPartial(), septemberPartial(), octoberPartial()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Completed Orders',
                    type: 'column'
                }]

            });

            $('#totalSupplier', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Amount of suppliers' },

                xAxis: [{
                    categories: [  'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: 3000,
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
                series: [
                {
                    data: [augustsupplierList().length, augustsupplierList().length + septembersupplierList().length, augustsupplierList().length + septembersupplierList().length + octobersupplierList().length],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Suppliers',
                    type: 'column',
                    color:'#01A7E2'
                }, ]

            });


        }


        var goBack = function () {
            router.navigateBack();
        };

        //#region Helper methods
        //#endregion

        var vm = {
            activate: activate,
            viewAttached: viewAttached,
            title: 'Supplier',
            goBack: goBack,
            userSettings: userSettings

        };

        return vm;

        function infoSupplierMargin() {
            var title = 'Supplier Margin';
            var msg = 'Shows the amounts paid to suppliers against the amount owed to suppliers';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoOrderCost() {
            var title = 'Order Costing';
            var msg = 'Shows the total cost amount of the orders placed in that month';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoOrderStats() {
            var title = 'Order Stats';
            var msg = 'Shows the total orders made against those received and not completed';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoSupplierStats() {
            var title = 'Total Supplier';
            var msg = 'The total amount of suppliers in the business';
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