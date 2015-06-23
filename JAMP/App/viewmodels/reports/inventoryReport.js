define([
    'services/datacontext',
    'durandal/app',
    'services/logger',
    'durandal/plugins/router',
    'services/model',
    'services/paging'],
    function (datacontext, app, logger, router, model, paging, highcharts) {
        //declaring all the variables that will hold information for the month
        var julycatagory = ko.observable(),
            julyamount = ko.observable(),
            augustcatagory = ko.observable(),
            augustamount = ko.observable(),
            septembercatagory = ko.observable(),
            septemberamount = ko.observable(),
            octobercatagory = ko.observable(),
            octoberamount = ko.observable(),
            Predicate = breeze.Predicate,
            //declaring the lists that contain the objects for the specific month
            julyproductList = ko.observableArray(),
            juneproductList = ko.observableArray(),
            augustproductList = ko.observableArray(),
            septemberproductList = ko.observableArray(),
            octoberproductList = ko.observableArray(),
            categoryList = ko.observableArray(),
            julycategoryList = ko.observableArray(),
            augustcategoryList = ko.observableArray(),
            septembercategoryList = ko.observableArray(),
            octobercategoryList = ko.observableArray(),
            julyIcidentList = ko.observableArray(),
            augustIncidentList = ko.observableArray(),
            septemberIcidentList = ko.observableArray(),
            octoberIncidentList = ko.observableArray(),
            userSettings = ko.observable();

        var categories = new Array();
        // creating predicates that define the date to sort the entities apart
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


            //getting the data from the local cached object and using a predicate to determine the date of the objects
            //datacontext.getEntityList(julyproductList, datacontext.entityAddress.product, false, null, Predicate.and([Julystart, Julyend]))
            datacontext.getEntityList(augustproductList, datacontext.entityAddress.product, false, null, Predicate.and([Auguststart, Augustend]))
            datacontext.getEntityList(septemberproductList, datacontext.entityAddress.product, false, null, Predicate.and([Septemberstart, Septemberend]))
            datacontext.getEntityList(octoberproductList, datacontext.entityAddress.product, false, null, Predicate.and([Octoberstart, Octoberend]))
            //datacontext.getEntityList(julycategoryList, datacontext.entityAddress.category, false, null, Predicate.and([Julystart, Julyend]))
            datacontext.getEntityList(augustcategoryList, datacontext.entityAddress.category, false, null, Predicate.and([Auguststart, Augustend]))
            datacontext.getEntityList(septembercategoryList, datacontext.entityAddress.category, false, null, Predicate.and([Septemberstart, Septemberend]))
            datacontext.getEntityList(octobercategoryList, datacontext.entityAddress.category, false, null, Predicate.and([Octoberstart, Octoberend]))
            //datacontext.getEntityList(julyIcidentList, datacontext.entityAddress.incident, false, null, Predicate.and([Julystart, Julyend]))
            datacontext.getEntityList(augustIncidentList, datacontext.entityAddress.incident, false, null, Predicate.and([Auguststart, Augustend]))
            datacontext.getEntityList(septemberIcidentList, datacontext.entityAddress.incident, false, null, Predicate.and([Septemberstart, Septemberend]))
            datacontext.getEntityList(octoberIncidentList, datacontext.entityAddress.incident, false, null, Predicate.and([Octoberstart, Octoberend]))
                .then(setup());

            //Set up is the function that is used to call the totals. gets given all the parameters of the objects to create a dynamic function
            function setup() {
                //totals(julyproductList, julycatagory, julyamount)
                totals(augustproductList, augustcatagory, augustamount);
                totals(septemberproductList, septembercatagory, septemberamount);
                totals(octoberproductList, octobercatagory, octoberamount);
     



            };

            //function accepts in the list of objects, the total amount owed by the customers, and the total cost of the accounts in the system
            function totals(productlist, observable, totalobservable) {
                var amountfigure = 0;
                var totalTypeofProducts = 0;
                for (var i = 0; i <= productlist().length - 1; i++) {
                    amountfigure += productlist()[i].quantity();
                    totalTypeofProducts += 1;
                    observable(totalTypeofProducts);
                }
                totalobservable(amountfigure);
                return true;
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

            // renders a graph showing the number of products in the business
            $('#totalProducts', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Number of Products' },

                xAxis: [{
                    categories: [ 'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
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
                        // need to change the style.
                        text: 'Total Units',
                        y: 600
                    }
                }],

                plotOptions: {
                    line: {
                        dataLabels: { enabled: true },
                    }
                },
                series: [{
                    data: [ augustamount(), augustamount() + septemberamount(), augustamount() + septemberamount()+octoberamount()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Quantity of Products',
                    type: 'column'
                
                }]

            });

            // renders a graph showing all product types
            $('#ProductsPerCategory', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Number of Product Types' },

                xAxis: [{
                    categories: [ 'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
                        x: 0
                    }
                }],
                yAxis: [{
                    min: 0,
                    //max: 10,
                    labels: {
                        rotation: 'auto',
                    },
                    gridLineWidth: 2,
                    title: {
                        // need to change the style.
                        text: 'Total Units',
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
                    data: [augustcatagory(), augustcatagory()+septembercatagory(), augustcatagory()+septembercatagory()+octobercatagory()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Number of Product Types',
                    type: 'bar'
                }, {
                    data: [  augustcategoryList().length, augustcategoryList().length + septembercategoryList().length, augustcategoryList().length + septembercategoryList().length+octobercategoryList().length],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Number of Categories',
                    type: 'bar'
                }
                ]

            });


            // renders a graph showing all the incidents in the business
            $('#totalIncidences', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Number of Incidents' },

                xAxis: [{
                    categories: [ 'August', 'September', 'October'],
                    title: {
                        text: 'Month Of Year',
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
                        // need to change the style.
                        text: 'Total Units',
                        y: 600
                    }
                }],

                plotOptions: {
                    line: {
                        dataLabels: { enabled: true },
                    }
                },
                series: [{
                    data: [ augustIncidentList().length, septemberIcidentList().length, octoberIncidentList().length],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Number of Incidences',
                    type: 'column'
                }]

            });




        }

        var goBack = function () {
            router.navigateBack();
        };

        //#region Helper methods
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            title: 'Inventory',
            goBack: goBack,
            // Binding Observables
            userSettings:userSettings


        };

        return vm;

    });