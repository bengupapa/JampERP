define([
    'services/datacontext',
    'durandal/app',
    'services/logger',
    'durandal/plugins/router',
    'services/model',
    'services/paging'],
    function (datacontext, app, logger, router, model, paging, highcharts) {

        //declaring variable for the months total, number of customers, and total amount that was paid towards accounts
        var julycustomers = ko.observable(),
            julyamount = ko.observable(),
            augustcustomers = ko.observable(),
            augustamount = ko.observable(),
            septembercustomers = ko.observable(),
            septemberamount = ko.observable(),
            octobercustomers = ko.observable(),
            octoberamount = ko.observable(),
            Predicate = breeze.Predicate,
            //declaring the lists that contain the objects for the specific month
            julycustomerList = ko.observableArray(),
            junecustomersList = ko.observableArray(),
            augustcustomerList = ko.observableArray(),
            septembercustomerList = ko.observableArray(),
            octobercustomerList = ko.observableArray(),
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

            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            //getting the data from the local cached object and using a predicate to determine the date of the objects
            //datacontext.getEntityList(julycustomerList, datacontext.entityAddress.customer, false, null, Predicate.and([Julystart, Julyend]))
            datacontext.getEntityList(augustcustomerList, datacontext.entityAddress.customer, false, null, Predicate.and([Auguststart, Augustend]))
            datacontext.getEntityList(septembercustomerList, datacontext.entityAddress.customer, false, null, Predicate.and([Septemberstart, Septemberend]))
            datacontext.getEntityList(octobercustomerList, datacontext.entityAddress.customer, false, null, Predicate.and([Octoberstart, Octoberend]))
                .then(setup());

            //Set up is the function that is used to call the totals. gets given all the parameters of the objects to create a dynamic function
            function setup() {
                //console.log(julycustomerList().length)
                //totals(julycustomerList, julycustomers, julyamount)
                totals(augustcustomerList, augustcustomers, augustamount);
                totals(septembercustomerList, septembercustomers, septemberamount);
                totals(octobercustomerList, octobercustomers, octoberamount);
                
                
            };
            //function accepts in the list of objects, the total amount owed by the customers, and the total cost of the accounts in the system
            function totals(customerlist, observable, totalobservable) {
                //creating reference variable that will be used to count the totals
                var amountowedfigure = 0;
                var totalcustomers = 0;
                //looping through the list of objects to extract different variables from the object
                for (var i = 0; i <= customerlist().length - 1; i++) {
                    amountowedfigure += customerlist()[i].customerAccount().amountOwing();
                    totalobservable(amountowedfigure);
                    totalcustomers += totalpayments(customerlist()[i].customerAccount().customerPayments);
                    observable(totalcustomers);
                }
                return true;
            };

            //method takes in the object and from it all the payments that were made for that particular object
            function totalpayments(payments) {
                var totalPayments = 0;
                for (var a = 0; a <= payments().length - 1; a++) {
                    console.log(payments()[0].amountPaid());
                    totalPayments += payments()[a].amountPaid();
                }
                return totalPayments;
                //return the sum of the payments from the object
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

            bindEventToList(view, '.infoCustomersStats', infoCustomersStats);
            bindEventToList(view, '.infoCustomerNumbers', infoCustomerNumbers);


            // renders a graph showing all the payments against the amounts owed by the suppliers
            $('#totalCustomers', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Amount Owed' },

                xAxis: [{
                    categories: ['August', 'September', 'October'],
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
                    data: [ augustamount(), septemberamount(), octoberamount()],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Amount Owed',
                    type: 'line',
                    color: '#4CC417'
                }]

            });
            //graph shows the total number of customers in the system for the different months
            $('#NumberCustomers', view).highcharts({

                chart: { type: 'column' },
                title: { text: 'Total Number of Customers' },

                xAxis: [{
                    categories: ['August', 'September', 'October'],
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
                        text: 'People',
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
                    data: [julycustomerList().length + augustcustomerList().length, julycustomerList().length + augustcustomerList().length + septembercustomerList().length, julycustomerList().length + augustcustomerList().length + septembercustomerList().length+octobercustomerList().length],
                    yAxis: 0,
                    dashStyle: 'Solid',
                    name: 'Total Number of Customers',
                    type: 'column',
                    color: '#01A7E2'
                }]

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
            title: 'Customer',
            goBack: goBack,
            userSettings: userSettings


        };

        return vm;

        function infoCustomersStats() {
            var title = 'Customer Margins';
            var msg = 'Total amount owed by customers agains payments made';
            app.showMessage(msg, title, ['Ok']);
        }

        function infoCustomerNumbers() {
            var title = 'Customer Numbers';
            var msg = 'Total number of customers in the business';
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