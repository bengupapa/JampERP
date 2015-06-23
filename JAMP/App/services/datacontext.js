define([
    'durandal/system',
    'services/model',
    'services/logger'],
    function (system, model, logger) {
        var EntityQuery = breeze.EntityQuery,
            Predicate = breeze.Predicate,
            manager = configureBreezeManager(),
            entityNames = model.entityNames,
            currentUser = ko.observable(),
            stashName = "Jamp_data_all",
            deviceName = "DeviceNumber",
            busyUpdating = ko.observable(false),        // Notify UI of update
            updateAvailable = ko.observable(false),     // Notify UI when autoUpdate is off
            canRefresh = ko.observable();               // Allow UI to refresh observable arrays

        // Addresses for API queries 
        var entityAddress = {
            user: 'UserInformation',
            business: 'BusinessInformation',
            // Backup API's
            employee: 'Employees',
            product: 'Products',
            category: 'Categories',
            order: 'Orders',
            supplier: 'Suppliers',
            customer: 'Customers',
            sale: 'Sales',
            incident: 'Incidents'
        };

        // Type of save change
        var saveReason = {
            deletion: 'deletion',
            add: 'add',
            update: 'update'
        };

        // Updated Entities available
        var updateList = ko.observable({
            product: ko.observable(false),
            supplier: ko.observable(false),
            customer: ko.observable(false),
            category: ko.observable(false),
            order: ko.observable(false),
            incident: ko.observable(false),
            employee: ko.observable(false),
            sale: ko.observable(false),
            delivery: ko.observable(false)
        });

        //#region Entity Retrieval
        //---------------------------------------------------------------------

        /* Generic Method: Get entity [single] (Observable)
        * ======================================================
        * @Observable:      [ko observable] to add an entity too
        * @APIurl:          [string] value of the api url address
        * @forceRemote:     [bool] to get data from local or server db
        * @Expand:          [string] value representing the object to expand on
        * @Where:           [string] object representing the where clause parameters
        * @Select:          [string] value representing object attributes required 
        * @OrderBy:         [string] value representing the order to return the object */
        var getEntitySingle = function (Observable, APIurl, forceRemote, Expand, Where, Select, OrderBy) {

            //Build query
            var query = queryBuilder(APIurl, Expand, Where, null, null, Select, OrderBy);

            //Get local if cache exist
            if (!forceRemote) {
                var s = manager.executeQueryLocally(query);
                if (s.length > 0) {
                    log('Retrieved [' + APIurl + '] from local data source', null, false);
                    Observable(s[0]);
                    return Q.resolve();
                }
            }

            //Get from the server
            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            //If there is an observable object 
            //add retrieved data to it
            function querySucceeded(data) {
                if (Observable) {
                    var s = data.results[0];
                    Observable(s);
                }
                log('Retrieved [' + APIurl + '] from remote data source', data, false);
            }
        };

        /* Generic Method: Get entity [list] (Observable Array)
        * ======================================================
        * @Observable:      [ko observable] to add entities too
        * @APIurl:          [string] value of the api url address
        * @forceRemote:     [bool] to get data from local or server db 
        * @Expand:          [string] value representing the object to expand on
        * @Where:           [string] object representing the where clause parameters
        * @Take:            [int] value represents the number of object to retrieve
        * @Skip:            [int] value represents the number of object to ignore
        * @Select:          [string] value representing object attributes required 
        * @OrderBy:         [string] value representing the order to return the object */
        var getEntityList = function (Observable, APIurl, forceRemote, Expand, Where, Take, Skip, Select, OrderBy) {

            //Build query
            var query = queryBuilder(APIurl, Expand, Where, Take, Skip, Select, OrderBy);

            //Get local if cache exist
            if (!forceRemote) {
                var p = manager.executeQueryLocally(query);
                if (p.length > 0) {
                    log('Retrieved [' + APIurl + '] from local data source [List]', null, false);
                    Observable(p);
                    return Q.resolve();
                }
            }

            //Get from the server
            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            //If there is an observable object 
            //add retrieved data to it
            function querySucceeded(data) {
                if (Observable) {
                    Observable(data.results);
                }
                log('Retrieved [' + APIurl + '] from remote data source [List]', data, false);
            }
        };

        /* Specific Method: Get entity details [single] (Observable)
        * ======================================================
        * @ID:               [int] ID of the entity
        * @TypeEntity:       [string] type name of entity
        * @Observable:       [ko observable] to add the entity too */
        var getEntityDetails = function (ID, TypeEntity, Observable) {

            //check locally first
            return manager.fetchEntityByKey(
                TypeEntity, ID, true)
                .then(fetchSucceeded)
                .fail(queryFailed);

            // send back the individual entity
            function fetchSucceeded(data) {
                var p = data.entity;
                return Observable(p);
            }
        };

        /* Specific Method: Get entity count (Observable)
        * ======================================================
        * @Observable:      [ko observable] to add the entity too 
        * @APIurl:          [string] value of the api url address
        * @forceRemote:     [bool] to get data from local or server db 
        * @Where:           [string] object representing the where clause parameters */
        var getEntityCount = function (Observable, APIurl, forceRemote, Where) {

            //Build query
            var query = queryBuilder(APIurl, null, Where);
            query = query.take(0).inlineCount(true);

            //Get from local cache
            if (!forceRemote) {
                var data = manager.executeQueryLocally(query);
                Observable(data.length);
                log('Retrieved count for [' + APIurl + '] from local data source', null, false);
                return Q.resolve();
            }

            //Get from the server
            return manager.executeQuery(query)
                .then(querySucceeded)
                .fail(queryFailed);

            //Get the count
            function querySucceeded(data) {
                log('Retrieved count for [' + APIurl + '] from remote data source', data, false);
                return Observable(data.inlineCount);
            }

        };

        //#endregion

        //#region Manage Entities
        //---------------------------------------------------------------------

        /* Generic Method: Create an empty entity object
        * ===================================================
        * @Type:        [stirng] name of the entity to be created  
        * @Identity:    [int] value of the foreign key as Primary (one ~ one) */
        var createObject = function (Type, Identity) {

            log('Created [' + Type + '] entity', null, false);

            switch (Type) {

                case entityNames.customerPayment:
                case entityNames.delivery:
                case entityNames.supplierPayment:
                    return manager.createEntity(Type, {
                        employeeID: currentUser().employeeID(),             // Employee ID
                        createdDate: moment().format("YYYY-MM-DD, h:mm a")  // CreatedAt    [Current Date + Time]
                    });
                case entityNames.category:
                    return manager.createEntity(Type, {
                        businessID: currentUser().businessID(),             // Business ID
                        createdDate: moment().format("YYYY-MM-DD, h:mm a")  // CreatedAt    [Current Date + Time]
                    });
                case entityNames.sale:
                case entityNames.supplier:
                case entityNames.incident:
                case entityNames.customer:
                    return manager.createEntity(Type, {
                        businessID: currentUser().businessID(),             // Business ID
                        employeeID: currentUser().employeeID(),             // Employee ID  [Current user]
                        createdDate: moment().format("YYYY-MM-DD, h:mm a")  // CreatedAt    [Current Date + Time]
                    });
                case entityNames.product:
                    return manager.createEntity(Type, {
                        businessID: currentUser().businessID(),             // Business ID
                        employeeID: currentUser().employeeID(),             // Employee ID  [Current user]
                        createdDate: moment().format("YYYY-MM-DD, h:mm a"), // CreatedAt    [Current Date + Time]
                        special: "No",                                      // Set special default to 'No'
                    });
                case entityNames.order:
                    return manager.createEntity(Type, {
                        businessID: currentUser().businessID(),             // Business ID
                        employeeID: currentUser().employeeID(),             // Employee ID  [Current user]
                        createdDate: moment().format("YYYY-MM-DD, h:mm a"), // CreatedAt    [Current Date + Time]
                        completed: "Outstanding"                            // Set status to 'Outstanding'
                    });
                case entityNames.customerAccount:
                    return manager.createEntity(Type, {
                        customerID: Identity,                               // Customer ID  [one ~ one]
                    });
                case entityNames.supplierAccount:
                    return manager.createEntity(Type, {
                        supplierID: Identity,                               // Supplier ID  [one ~ one]
                    });
                default:
                    return manager.createEntity(Type);
            }
        };

        //Check if manager has changes
        var hasChanges = ko.observable(false);
        manager.hasChangesChanged.subscribe(function (eventArgs) {
            hasChanges(eventArgs.hasChanges);
        });

        //Cancel changes of in entity manager
        var cancelChanges = function () {
            manager.rejectChanges();
            log('Canceled changes', null, true);
        };

        /* Detach an entity from the cache [single]
        * ===========================================================
        * @Entity:      [ko observable] entity requiring detaching */
        var entityDetached = function (Entity) {
            manager.detachEntity(Entity);
        };

        /* Save changes of the entity to the server
        * ==================================================================
        * @Reason:         [string] value representing the trigger of the save
        * @MessageName:    [string] message to display when saved */
        var saveChanges = function (Reason, MessageName) {

            return manager.saveChanges()
                .then(saveSucceeded)
                .fail(saveFailed);

            // Give notification of successful save to DB
            function saveSucceeded(saveResult) {
                var msg = '';

                switch (Reason) {
                    case saveReason.deletion:
                        msg = 'Deleted: ' + MessageName;
                        break;
                    case saveReason.add:
                        msg = 'Added: ' + MessageName;
                        break;
                    case saveReason.update:
                        msg = 'Updated: ' + MessageName;
                        break;
                    default:
                        msg = 'Saved data successfully';
                }
                createUpdateNotification(saveResult);

                if (Reason) {
                    log(msg, saveResult, true, logger.logType.logSuccess);
                }
                return true;
            }

            // Get list of updated entities
            function createUpdateNotification(saveResult) {
                var entityUpdateArray = [];

                // Push new entities to array
                for (var i = 0; i < saveResult.entities.length; i++) {

                    // Get entity type
                    switch (saveResult.entities[i].__proto__.entityType.shortName) {
                        case entityNames.category:
                            entityUpdateArray.push({ Entity: entityNames.category, UpdateNumber: saveResult.entities[i].categoryID() });
                            break;
                        case entityNames.product:
                            entityUpdateArray.push({ Entity: entityNames.product, UpdateNumber: saveResult.entities[i].productID() });
                            break;
                        case entityNames.business:
                            entityUpdateArray.push({ Entity: entityNames.business, UpdateNumber: saveResult.entities[i].businessID() });
                            break;
                        case entityNames.employee:
                        case entityNames.userSettings:
                            entityUpdateArray.push({ Entity: saveResult.entities[i].__proto__.entityType.shortName, UpdateNumber: saveResult.entities[i].employeeID() });
                            break;
                        case entityNames.supplier:
                        case entityNames.supplierAccount:
                        case entityNames.supplierPayment:
                            entityUpdateArray.push({ Entity: saveResult.entities[i].__proto__.entityType.shortName, UpdateNumber: saveResult.entities[i].supplierID() });
                            break;
                        case entityNames.customer:
                        case entityNames.customerAccount:
                        case entityNames.customerPayment:
                            entityUpdateArray.push({ Entity: saveResult.entities[i].__proto__.entityType.shortName, UpdateNumber: saveResult.entities[i].customerID() });
                            break;
                        case entityNames.sale:
                        case entityNames.salesItem:
                            entityUpdateArray.push({ Entity: entityNames.sale, UpdateNumber: saveResult.entities[i].saleID() });
                            break;
                        case entityNames.order:
                        case entityNames.productOrder:
                            entityUpdateArray.push({ Entity: saveResult.entities[i].__proto__.entityType.shortName, UpdateNumber: saveResult.entities[i].orderID() });
                            break;
                        case entityNames.incident:
                        case entityNames.productIncident:
                            entityUpdateArray.push({ Entity: entityNames.incident, UpdateNumber: saveResult.entities[i].incidentID() });
                            break;
                        case entityNames.delivery:
                            entityUpdateArray.push({ Entity: entityNames.delivery, UpdateNumber: saveResult.entities[i].orderID() });
                            break;
                        case entityNames.productDelivery:
                            entityUpdateArray.push({ Entity: entityNames.delivery, UpdateNumber: saveResult.entities[i].delivery().orderID() });
                            break;
                        default:
                            console.log(saveResult.entities[i].__proto__.entityType.shortName + ' [entity] does not have a case');
                            break;
                    }
                }

                // Send array to server
                sendUpdateNotification(entityUpdateArray);
            }

            // Show error message on failed save
            function saveFailed(error) {
                if (error.status == 404) {
                    return log('Saved locally', null, true, logger.logType.logSuccess, 'Off line');
                }
                var msg = 'Save failed: ' + getErrorMessages(error);
                logError(msg, error);
                error.message = msg;
                throw error;
            }

        };

        //#endregion 

        //#region Server Communication
        //---------------------------------------------------------------------

        //Variable of connection to server hub
        var sig = $.connection.JampUpdateHub;

        // Enable logging of signalR 
        $.connection.hub.logging = true;

        // Message on disconnection from server
        $.connection.hub.disconnected(function (data) {

            var tempList = ko.observableArray([]);
            getEntityList(tempList, entityAddress.employee, false).then(updateStatus);

            // Update all employess to offline
            function updateStatus() {
                ko.utils.arrayForEach(tempList(), function (employee) {
                    employee.online(false);
                    if (employee.lastSeen() === 'Now') {
                        if (employee.employeeID() === currentUser().employeeID()) {
                            employee.lastSeen('Disconnected');
                        } else {
                            employee.lastSeen(moment().format("h:mm a, YYYY/MM/DD"));
                        }
                    }
                    employee.entityAspect.setUnchanged();
                });
                log('Lost connection with server', null, true, logger.logType.logWarning, 'Disconnected');
            }
        });

        $.connection.hub.connectionSlow(function () {
            log('There seems to be some connectivity issues...', null, true, logger.logType.logWarning, 'Slow connectivity');
        });

        //Start connection & get on line employees
        var connectToServer = function () {
            return $.connection.hub.start()
                .done(getOnlineEmployees)
                .fail(failConnection);

            // Get Employees
            function getOnlineEmployees() {
                if (currentUser().roleName() !== 'Cashier') {
                    return getEntityList(null, entityAddress.employee, true).then(checkDevice).then(checkUnsavedChanges);
                } else {
                    return getEntitySingle(null, entityAddress.user, true).then(checkDevice).then(checkUnsavedChanges);
                }
            }

            // Get the unique ID assigned to the device
            function checkDevice() {
                var deviceID = window.localStorage.getItem(deviceName);
                if (deviceID) {
                    return currentUser().device(deviceID);
                } else {
                    return sig.server.assignUniqueID();
                }
            }

            // Check has unsaved changes
            function checkUnsavedChanges() {
                var changes = manager.getChanges();
                if (changes.length > 0) {
                    return saveChanges();
                }
                return true;
            }

            // Message if failed
            function failConnection() {
                log('Failed to connect', null, true, logger.logError);
            }

        };

        //#region Client side Methods

        // Get updates of other users in the application
        sig.client.access = function (accessType, empID) {
            var tempEmp = ko.observable(''),
                where = {
                    firstParm: 'employeeID',
                    operater: '==',
                    secondParm: empID
                };

            // Get user and change its status
            getEntitySingle(tempEmp, entityAddress.employee, false, null, where).then(updateStatus);

            // Update the users details
            function updateStatus() {
                if (accessType) {
                    tempEmp().online(true);
                    tempEmp().lastSeen('Now');
                } else {
                    tempEmp().online(false);
                    tempEmp().lastSeen(moment().format("h:mm a, YYYY/MM/DD"));
                }
                tempEmp().entityAspect.setUnchanged();

                // Notify of other users access
                if (currentUser().employeeID() !== tempEmp().employeeID()) {
                    log(tempEmp().fullName(), null, true, logger.logType.logInfo, accessType ? 'Signed In' : 'Signed Out');
                } else {
                    log('Connected to JAMP', null, true, logger.logType.logSuccess);
                }
            }
        };

        // Store device identifier
        sig.client.storgeLocally = function (identifier) {
            window.localStorage.setItem(deviceName, identifier);
            currentUser().device(identifier);
        };

        // Start the update process
        sig.client.updateMessage = function () {

            // IF auto update enabled
            if (currentUser().settings().autoUpdate() === "On") {
                retrieveUpdates();
            } else {
                updateAvailable(true);
            }
        };

        // Update the local cache with updates
        sig.client.entitesToUpdate = function (data) {
            // Notify UI 
            updateAvailable(false);
            busyUpdating(true);

            var predCategory = { address: entityAddress.category, predicate: [] },
                predProduct = { address: entityAddress.product, predicate: [] },
                predBusiness = { address: entityAddress.business, predicate: [] },
                predEmployee = { address: entityAddress.employee, predicate: [] },
                predUserSettings = { address: entityAddress.employee, predicate: [], select: 'settings' },
                predSupplier = { address: entityAddress.supplier, predicate: [] },
                predSupAccount = { address: entityAddress.supplier, predicate: [], select: 'supplierAccount' },
                predSupPayment = { address: entityAddress.supplier, predicate: [], expand: 'supplierAccount, supplierAccount.supplierPayments' },
                predCustomer = { address: entityAddress.customer, predicate: [] },
                predCustAccount = { address: entityAddress.customer, predicate: [], select: 'customerAccount' },
                predCustPayment = { address: entityAddress.customer, predicate: [], expand: 'customerAccount, customerAccount.customerPayments' },
                predSale = { address: entityAddress.sale, predicate: [], expand: 'salesItems' },
                predOrder = { address: entityAddress.order, predicate: [] },
                predProOrder = { address: entityAddress.order, predicate: [], select: 'productOrders' },
                predincident = { address: entityAddress.incident, predicate: [], expand: 'productIncidents' },
                predDelivery = { address: entityAddress.order, predicate: [], expand: 'deliveries, deliveries.productDeliveries' };


            // Sort by entity then by ID
            data.sort(function (a, b) {
                if (a.Entity < b.Entity) return -1;
                else if (a.Entity > b.Entity) return 1;
                else if (a.UpdateNumber < b.UpdateNumber) return -1;
                else if (a.UpdateNumber > b.UpdateNumber) return 1;
                else return 0;
            });

            // Delete all duplicates from the array
            for (var i = 0; i < data.length - 1; i++) {
                if (data[i].Entity === data[i + 1].Entity) {
                    if (data[i].UpdateNumber === data[i + 1].UpdateNumber) {
                        delete data[i];
                    }
                }
            }

            // Remove undefined 
            data = data.filter(function (el) { return (typeof el !== "undefined"); });

            // Build predicate arrays
            for (var i = 0; i < data.length; i++) {
                switch (data[i].Entity) {
                    case entityNames.category:
                        predCategory.predicate.push(Predicate.create("categoryID", "==", data[i].UpdateNumber));
                        updateList().category(true);
                        break;
                    case entityNames.product:
                        predProduct.predicate.push(Predicate.create("productID", "==", data[i].UpdateNumber));
                        updateList().product(true);
                        break;
                    case entityNames.business:
                        predBusiness.predicate.push(Predicate.create("businessID", "==", data[i].UpdateNumber));
                        break;
                    case entityNames.employee:
                        predEmployee.predicate.push(Predicate.create("employeeID", "==", data[i].UpdateNumber));
                        updateList().employee(true);
                        break;
                    case entityNames.userSettings:
                        predUserSettings.predicate.push(Predicate.create("employeeID", "==", data[i].UpdateNumber));
                        break;
                    case entityNames.supplier:
                        predSupplier.predicate.push(Predicate.create("supplierID", "==", data[i].UpdateNumber));
                        updateList().supplier(true);
                        break;
                    case entityNames.supplierAccount:
                        predSupAccount.predicate.push(Predicate.create("supplierID", "==", data[i].UpdateNumber));
                        updateList().supplier(true);
                        break;
                    case entityNames.supplierPayment:
                        predSupPayment.predicate.push(Predicate.create("supplierID", "==", data[i].UpdateNumber));
                        updateList().supplier(true);
                        break;
                    case entityNames.customer:
                        predCustomer.predicate.push(Predicate.create("customerID", "==", data[i].UpdateNumber));
                        updateList().customer(true);
                        break;
                    case entityNames.customerAccount:
                        predCustAccount.predicate.push(Predicate.create("customerID", "==", data[i].UpdateNumber));
                        updateList().customer(true);
                        break;
                    case entityNames.customerPayment:
                        predCustPayment.predicate.push(Predicate.create("customerID", "==", data[i].UpdateNumber));
                        updateList().customer(true);
                        break;
                    case entityNames.sale:
                        predSale.predicate.push(Predicate.create("saleID", "==", data[i].UpdateNumber));
                        updateList().sale(true);
                        break;
                    case entityNames.order:
                        predOrder.predicate.push(Predicate.create("orderID", "==", data[i].UpdateNumber));
                        updateList().order(true);
                        break;
                    case entityNames.productOrder:
                        predProOrder.predicate.push(Predicate.create("orderID", "==", data[i].UpdateNumber));
                        updateList().order(true);
                        break;
                    case entityNames.incident:
                        predincident.predicate.push(Predicate.create("incidentID", "==", data[i].UpdateNumber));
                        updateList().incident(true);
                        break;
                    case entityNames.delivery:
                        predDelivery.predicate.push(Predicate.create("orderID", "==", data[i].UpdateNumber));
                        updateList().delivery(true);
                        break;
                    default:
                        console.log(+ ' [entity] does not have a case to predicate for');
                        break;
                }
            }

            // Combine all updates
            var updateEntityList = [];
            updateEntityList.push(
                predCategory,
                predProduct,
                predBusiness,
                predEmployee,
                predUserSettings,
                predSupplier,
                predSupAccount,
                predSupPayment,
                predCustomer,
                predCustAccount,
                predCustPayment,                
                predSale,
                predOrder,
                predProOrder,
                predincident,
                predDelivery);

            // Get updates with Promise
            var updateEntityMethods = [];
            for (var i = 0; i < updateEntityList.length; i++) {
                if (updateEntityList[i].predicate.length > 0) {
                    if (updateEntityList[i].select) {
                        promise = getEntityList(null, updateEntityList[i].address, true, null, Predicate.or(updateEntityList[i].predicate), null, null, updateEntityList[i].select);
                    } else if (updateEntityList[i].expand) {
                        promise = getEntityList(null, updateEntityList[i].address, true, updateEntityList[i].expand, Predicate.or(updateEntityList[i].predicate));
                    } else {
                        promise = getEntityList(null, updateEntityList[i].address, true, null, Predicate.or(updateEntityList[i].predicate));
                    }
                    updateEntityMethods.push(promise);
                }
            }
            Q.all(updateEntityMethods).then(completeUpdate).fail(failUpdates);

            // Remove updates from sever & update UI
            function completeUpdate() {
                canRefresh(true);
                busyUpdating(false);

                // Tell server to delete update record
                sig.server.removeUpdates(currentUser().device());
            }

            // Something when wrong in getting updates
            function failUpdates(error) {
                busyUpdating(false);
                updateAvailable(true);
                log('Failed. Please try again', null, true, logger.logType.logWarning, 'Getting Updates');
                throw error;
            }
        };
        //#endregion

        //#region Server side Methods

        // Notify Business group of change
        function sendUpdateNotification(updateItems) {
            sig.server.updateNotification(currentUser().device(), updateItems);
        }

        // Unlink the device and remove updates
        function unlinkCurrentDevice() {
            sig.server.unlinkDevice(currentUser().device());
        }

        // Request the entities to be updated
        function retrieveUpdates() {
            return sig.server.getUpdates(currentUser().device());
        }
        //#endregion

        // Reset the page refresher
        var completedRefresh = function () {
            canRefresh(false);

            // Reset page update ready observables
            updateList().product(false);
            updateList().supplier(false);
            updateList().customer(false);
            updateList().category(false);
            updateList().order(false);
            updateList().incident(false);
            updateList().employee(false);
            updateList().sale(false);
            updateList().delivery(false);
        };

        //#endregion

        //#region Initialize & Terminate Data
        //---------------------------------------------------------------------

        //Get initial data sets
        var primeData = function () {
            return Q.fcall(checkLocalStorage)
                .then(getUser);


            // Get data from local storage
            function checkLocalStorage() {
                if (Modernizr.localstorage) {
                    var importData = window.localStorage.getItem(stashName);

                    // Check if has local data
                    if (importData) {
                        manager.importEntities(importData, { mergeStrategy: breeze.MergeStrategy.PreserveChanges });
                        return false;
                    }
                }
                return true;
            }

            // Get the current logged in user [cache/server]
            function getUser(result) {
                if (!result) {
                    return getEntitySingle(currentUser, entityAddress.user, false, model.entityNames.business);
                } else {
                    return getEntitySingle(currentUser, entityAddress.user, true, model.entityNames.business + ', settings').then(fetchDataSever);
                }
            }

            // Get data from server
            function fetchDataSever() {
                // Get role level data
                if (currentUser().roleName() !== 'Cashier') {
                    return getEntityList(null, entityAddress.business, true,   // [Owner + Manager]
                        'employees' +                                       // Employee List
                        ',categories' +                                     // Category List
                        ',products' +                                       // Product List
                        ',suppliers' +                                      // Supplier List
                        ',suppliers.supplierAccount' +                      // Supplier Account List 
                        ',suppliers.supplierAccount.supplierPayments' +     // Supplier Payments List 
                        ',customers' +                                      // Customer List 
                        ',customers.customerAccount' +                      // Customer Account List 
                        ',customers.customerAccount.customerPayments' +     // Customer Payments List 
                        ',sales' +                                          // Sales List 
                        ',sales.salesItems' +                               // Sales Items List 
                        ',orders' +                                         // Orders List 
                        ',orders.productOrders' +                           // Product Orders List 
                        ',orders.deliveries' +                              // Deliveries List 
                        ',orders.deliveries.productDeliveries' +            // Product Deliveries List 
                        ',incidents' +                                      // Incidents List 
                        ',incidents.productIncidents'                       // Product Incidents List 
                        );
                } else {
                    return getEntityList(null, entityAddress.business, true,  // [Cashier]
                       'categories' +                                     // Category List
                       ',products' +                                       // Product List
                       ',customers' +                                      // Customer List 
                       ',customers.customerAccount'                        // Customer Account List 
                       );
                }
            }
        };

        // Save entities to local storage
        window.onbeforeunload = function () {
            // Check if has local storage
            if (Modernizr.localstorage) {

                // Check if not logging off
                if (!window.loggingOff) {

                    // Save cache to local storage
                    var exportData = manager.exportEntities();
                    window.localStorage.setItem(stashName, exportData);

                    // Cancel pending changes
                    if (hasChanges()) {
                        cancelChanges();
                    }
                } else {
                    unlinkCurrentDevice();
                    window.localStorage.clear();
                }
            }
        };

        //#endregion

        var datacontext = {
            // Enums
            entityAddress: entityAddress,
            saveReason: saveReason,
            updateList: updateList,
            // Entity Retrieval
            getEntitySingle: getEntitySingle,
            getEntityList: getEntityList,
            getEntityDetails: getEntityDetails,
            getEntityCount: getEntityCount,
            // Manage Entities
            createObject: createObject,
            hasChanges: hasChanges,
            cancelChanges: cancelChanges,
            entityDetached: entityDetached,
            saveChanges: saveChanges,
            // Server Communication
            connectToServer: connectToServer,
            retrieveUpdates: retrieveUpdates,
            updateAvailable: updateAvailable,
            busyUpdating: busyUpdating,
            canRefresh: canRefresh,
            completedRefresh: completedRefresh,         
            // Initialize & Terminate Data
            primeData: primeData,
            currentUser: currentUser
        };

        return datacontext;

        //#region Internal helper methods 

        function getErrorMessages(error) {
            var msg = error.message;
            if (msg.match(/validation error/i)) {
                return getValidationMessages(error);
            }
            return msg;
        }

        function getValidationMessages(error) {
            try {
                //foreach entity with a validation error
                return error.entitiesWithErrors.map(function (entity) {
                    // get each validation error
                    return entity.entityAspect.getValidationErrors().map(function (valError) {
                        // return the error message from the validation
                        return valError.errorMessage;
                    }).join('; <br/>');
                }).join('; <br/>');
            }
            catch (e) { }
            return 'validation error';
        }

        /*Build Queries for cache and server calls
        * ==================================================================
        * @APIurl:      [string] value of the URL Api
        * @Expand:      [string] value representing the object to expand on
        * @Where:       [string] object representing the where clause parameters
        * @Take:        [int] value represents the number of object to retrieve
        * @Skip:        [int] value represents the number of object to ignore
        * @Select:      [string] value representing object attributes required 
        * @OrderBy:     [string] value representing the order to return the object */
        function queryBuilder(APIurl, Expand, Where, Take, Skip, Select, OrderBy) {
            var query = EntityQuery.from(APIurl);

            if (Expand) {
                query = query.expand(Expand);
            }
            if (Where) {
                if (Where.hasOwnProperty('firstParm')) {
                    query = query.where(Where.firstParm, Where.operater, Where.secondParm);
                } else {
                    query = query.where(Where);
                }
            }
            if (Take) {
                query = query.take(Take);
            }
            if (Skip) {
                query = query.skip(Skip);
            }
            if (Select) {
                query = query.select(Select);
            }
            if (OrderBy) {
                query = query.orderBy(OrderBy);
            }
            return query;
        }

        //log error for retrieving data from server
        function queryFailed(error) {
            var msg = 'Error retreiving data. ' + error.message;
            logError(msg, error);
            throw error;
        }

        /*
        * Config breeze to use camel casing
        * Add additional ko computed attributes to models
        * Links resource names to entityTypes */
        function configureBreezeManager() {
            var remoteServiceName = '../breeze/Jamp';

            breeze.NamingConvention.camelCase.setAsDefault();
            var mgr = new breeze.EntityManager(remoteServiceName);
            model.configureMetadataStore(mgr.metadataStore);
            return mgr;
        }

        //Notification  toastr logger
        function log(msg, data, showToast, toastType, toastTitle) {
            logger.log(msg, data, system.getModuleId(datacontext), showToast, toastType, toastTitle);
        }

        //Error Logger
        function logError(msg, error) {
            logger.logError(msg, error, system.getModuleId(datacontext), true);
        }

        //#endregion

    });