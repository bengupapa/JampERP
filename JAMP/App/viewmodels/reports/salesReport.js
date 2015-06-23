define([
    'services/datacontext',
    'durandal/app',
    'services/logger',
    'durandal/plugins/router',
    'services/model',
    'services/paging'],
    function (datacontext, app, logger, router, model, paging, highcharts) {
        //declaration of the variables that are to be used to extract information for the different months 
        var julysales = ko.observable(),
            julyaverage = ko.observable(),
            julycosts = ko.observable(),
            julycredit = ko.observable(),
            julymonthSales = ko.observableArray(),
            augustsales = ko.observable(),
            augustcost = ko.observable(),
            augustaverage = ko.observable(),
            augustcredit = ko.observable(),
            augustmonthsales = ko.observable(),
            septembersales = ko.observable(),
            septembercost = ko.observable(),
            septemberaverage = ko.observable(),
            septembercredit = ko.observable(),
            septembermonthsales = ko.observable(),
            octobersales = ko.observable(),
            octoberosts = ko.observable(),
            octoberaverage = ko.observable(),
            octoberaverage = ko.observable(),
            octobercredit = ko.observable(),
            //predicate that is used to filter objects
            Predicate = breeze.Predicate,
            //lists that are to contian the objects that have been filtered for the month
            julysalesList = ko.observableArray(),
            junesaleList = ko.observableArray(),
            augustsaleList = ko.observableArray(),
            septembersaleList = ko.observableArray(),
            octobersaleList = ko.observableArray(),
            userSettings = ko.observable();

        //predicaters of and that are used filter the the objects in the system
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
            //getting objects from local cache using the predicate
            datacontext.getEntityList(julysalesList, datacontext.entityAddress.sale, false, null, Predicate.and([Julystart, Julyend]))
            datacontext.getEntityList(augustsaleList, datacontext.entityAddress.sale, false, null, Predicate.and([Auguststart, Augustend]))
            datacontext.getEntityList(septembersaleList, datacontext.entityAddress.sale, false, null, Predicate.and([Septemberstart, Septemberend]))
            datacontext.getEntityList(octobersaleList, datacontext.entityAddress.sale, false, null, Predicate.and([Octoberstart, Octoberend]))
                .then(setup());


            function setup() {
                console.log("i am herer")
                //Dynamic methods of calling all the objects and there variables to be populated
                //totals(julysalesList, julysales, julycosts, julyaverage, julycredit);
                totals(augustsaleList, augustsales, augustcost, augustaverage, augustcredit);
                totals(septembersaleList, septembersales, septembercost, septemberaverage, septembercredit);
                totals(octobersaleList, octobersales, octoberosts, octoberaverage, octobercredit);
                console.log(octobersaleList().length / 30)
                console.log(octoberaverage())
                console.log(septembersaleList().length / 30)
                console.log(septemberaverage())
                console.log(augustsaleList().length / 30)
                console.log(augustaverage())
            };

            function totals(saleslist, observable, costobservable, averageobservable, creditSale, cashsale) {
                console.log("asd")
                //methods that takes in all the variables of the month and populated these for the grpah]
                var salesfigure = 0;
                var totalcost = 0;
                var creditsales = 0;
                var decimalFix = 0;
                for (var i = 0; i <= saleslist().length - 1; i++) {
                    //looping through the objects and extracting the informations
                    salesfigure += saleslist()[i].amountCharged();
                    decimalFix = Math.round(salesfigure);
                    observable(decimalFix);
                    totalcost += cost(saleslist()[i].salesItems);
                    decimalFix = Math.round(totalcost);
                    costobservable(decimalFix);
                    //check if the sale was on credit or cash
                    if (saleslist()[i].credit() == true) {
                        creditsales += saleslist()[i].amountCharged();

                    };
                }
                creditSale(creditsales);
                var average = salesfigure / saleslist().length;
                Math.round(average);
                averageobservable(average);


                return true;
            };


            function cost(saleitems) {
                //getting the cost price of the product to get total sales cost
                var totalSalesCost = 0;
                for (var a = 0; a <= saleitems().length - 1; a++) {
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
            paging.setActive(true, 'Reports');


            bindEventToList(view, '.infoSalesStats', infoSalesStats);
            bindEventToList(view, '.infoCreditSales', infoCreditSales);
            bindEventToList(view, '.infoAverageSales', infoAverageSales);


            //graph showing the markup on the sale made
            $('#totalSales', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Revenue vs Total Costs vs MarkUp' },

                xAxis: [{
                    categories: ['August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: ,
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
                    type: 'areaspline',
                    color: '#88C32F'
                },
                {
                    data: [ augustcost(), septembercost(), octoberosts()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Cost Received',
                    type: 'areaspline',
                    color: '#003F60'
                },
                {
                    data: [ augustsales()- augustcost(), septembersales()- septembercost(), octobersales() - octoberosts()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Mark Up',
                    type: 'areaspline',
                    color: '#01A7E2'
                }]

            });

            //graph showing the sales made on cash against the ones made on credit
            $('#TotalsalesVScredit', view).highcharts({

                chart: { type: 'bubble' },
                title: { text: 'Total Credit vs Total Cash' },

                xAxis: [{
                    categories: [ 'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: 10000,
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
                    data: [ augustsales()-augustcredit(), septembersales() - septembercredit(), octobersales()-octobercredit()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Cash Sales Amount',
                    type: 'line',
                    color: '#4CC417'
                },
                {
                    data: [julycredit(), augustcredit(), septembercredit()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Credit Sales',
                    type: 'line',
                    color: '#F70D1A'
                }]

            });


            //shows thge avergae number of sales for the month agaisnt the average sales figure for the month
            $('#AverageSales', view).highcharts({

                chart: { type: 'bubble' },
                title: { text: 'Average Sales Made vs Average Sales Amount' },

                xAxis: [{
                    categories: [  'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: 30,
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
                    data: [Math.round(augustaverage()), Math.round(septemberaverage()), Math.round(octoberaverage())],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Average Sales Amount per Day',
                    type: 'line',
                    color: '#01A7E2'
                }, {
                    data: [Math.round(augustsaleList().length / 30), Math.round(septembersaleList().length / 30), Math.round(octobersaleList().length / 30)],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Average Number of sales per Day',
                    type: 'line',
                    color: '#F70D1A'
                }]


            });
        }


        var goBack = function () {
            router.navigateBack();
        };

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            title: 'Sales',
            goBack: goBack,
            userSettings: userSettings

        };

        return vm;


        function infoSalesStats() {
            var title = 'Sales Stats';
            var msg = 'Total amount received against the total cost of the sales';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoCreditSales() {
            var title = 'Credit vs Cash stats';
            var msg = 'These are the number of credit sales against the cash sales for the month';
            app.showMessage(msg, title, ['Ok']);
        }
        
        function infoAverageSales() {
            var title = 'Average Sales';
            var msg = 'Average number of sales made per day as well as the average amount of each sale for the month';
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