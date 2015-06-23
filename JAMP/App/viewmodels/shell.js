define([
    'durandal/system',
    'services/model',
    'durandal/plugins/router',
    'services/logger',
    'config',
    'durandal/app',
    'services/datacontext'],
    function (system, model, router, logger, config, app, datacontext) {

        var subscription = null,
            user = ko.observable(),
            orders = ko.observableArray([]),
            opts = { lines: 10, length: 0, width: 3, radius: 6, corners: 1, rotate: 0, color: '#fff', speed: 1 },
            spinner,
            Predicate = breeze.Predicate;

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Start the boot process
            return datacontext.primeData()
                .then(boot)
                .then(getDates)
                .fail(failedInitialization);
        };

        var viewAttached = function (view) {

            //Setup main content area page width
            if ($(window).width() > 939) {
                $('.content-frame').css('width', '100%').css('width', '-=190px');

            } else {
                $('.content-frame').css('width', '100%');
            }
            setupMenu();
            bindEventToList(view, '.ordermessage', gotoDetails);
            // Update Spinner
            spinner = new Spinner(opts);

            // Connect to server once view Attached
            datacontext.connectToServer().then(datacontext.retrieveUpdates());
        };

        //#endregion

        //#region Visible Methods
        // Reconnect to server
        var reconnect = function () {
            datacontext.connectToServer();
        };

        // Check for if busy updating
        var busyUpdating = ko.computed(function () {
            
            if (datacontext.busyUpdating()) {
                var target = document.getElementById('spinnerContainer');
                spinner.spin();
                target.appendChild(spinner.el);
                return true;
            } else {
                if (spinner) {
                    spinner.stop();
                }
                return false;
            }
        });

        // Notify UI if there is an update
        var updateAvailable = ko.computed(function () {
            return datacontext.updateAvailable();
        });

        // Notify UI if there is an update
        var getUpdates = function () {
            return datacontext.retrieveUpdates();
        };
        //#endregion

        var shell = {
            // Durandal Methods
            activate: activate,
            router: router,
            viewAttached: viewAttached,
            // Visible Methods
            reconnect: reconnect,
            getUpdates: getUpdates,
            busyUpdating: busyUpdating,
            updateAvailable: updateAvailable,
            gotoDetails: gotoDetails,
            // Binding Observables
            user: user,
            orders: orders
        };

        return shell;

        //#region Internal Helper Methods

        //Go to product Details
        function gotoDetails(selectedOrder) {
            if (selectedOrder && selectedOrder.orderID()) {
                var url = '#/Supplier_Order_Details/' + selectedOrder.orderID();
                router.navigateTo(url);
            }
        }

        // Setup sliding menu for mobile
        function setupMenu() {

            //setup sliding side menu
            window.sliding();
            var app = window.App;
            app.init();

            //Setup about reveal for sliding menu
            $('#slide-nav-logo').click(function () {
                var slide_menu = document.getElementById('nav-open-btn'),
                    event = document.createEvent('HTMLEvents');
                event.initEvent('click', true, true);
                slide_menu.dispatchEvent(event);
                $('#aboutModal').foundation('reveal', 'open');
            });
        }

        // Start JAMP by Dashboard route
        function boot() {
            // Get the current user
            user(datacontext.currentUser());

            // Get array of routes from config
            var routesVar = config.userRoutes(user().roleName());

            router.map(routesVar);

            // Assign starting route
            if (user().roleName() === 'Cashier') {
                start = 'Quick_Links';
            }
            else {
                start = 'Dashboard';
            }
            return router.activate(start);
        }

        // Get important dates
        function getDates() {
            var todayDate = moment().format("YYYY-MM-DD");
            var where = {
                firstParm: 'dateDue',
                operater: 'startswith',
                secondParm: todayDate
            };

            return datacontext.getEntityList(orders, datacontext.entityAddress.order, false, null, where);
        }

        // Failed to start JAMP message
        function failedInitialization(error) {
            var msg = 'App initialization failed: ' + error.message;
            logger.logError(msg, error, system.getModuleId(shell), true);
        }

        // Log messages to toastr
        function log(msg, data, showToast, toastType, toastTitle) {
            logger.log(msg, data, system.getModuleId(shell), showToast, toastType, toastTitle);
        }

        // Binding for div click events
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().order()) {
                    getDates();
                }
            }
        }
        //#endregion

    });