define([
    'services/datacontext',
    'services/model'],
    function (datacontext, model) {
        toastr.options.timeOut = 4000;
        toastr.options.positionClass = 'toast-bottom-right';

        var appTitle = 'JAMP';

        //#region Assign routes based on role
        // Get the current users role &
        // return routes
        var userRoutes = function (userRole) {

            if (userRole === 'Cashier') {
                return cashierRoutes;
            }
            else {
                return routes;
            }
        };
        //#endregion

        //#region Routes for Owner and manage
        var routes = [{
            //#region Profile Routes
            //======================================================================
            url: 'Profile',
            moduleId: 'viewmodels/profile',
            name: 'Profile',
            caption: 'Profile',
            visible: false
        }, {
            //#endregion
            //#region Dashboard Routes
            //======================================================================*/
            url: 'Dashboard',
            moduleId: 'viewmodels/dashboard/dashboard',
            name: 'Dashboard',
            caption: 'Dashboard',
            visible: true,
            settings: { navIcon: '<i class="icon-dashboard"></i>' }
        }, {
            //#endregion
            //#region Sales Routes
            //======================================================================*/
            url: 'Sales_Summary',
            moduleId: 'viewmodels/sales/summarySales',
            name: 'Sales',
            caption: 'Sales: Summary',
            visible: true,
            settings: { navIcon: '<i class="icon-shopping-cart"></i>' }
        }, {
            url: 'Sales_Create',
            moduleId: 'viewmodels/sales/addSales',
            name: 'Sales',
            caption: 'Sales: Create Sale',
            visible: false,
        }, {
            url: 'Sales_List',
            moduleId: 'viewmodels/sales/listSales',
            name: 'Sales',
            caption: 'Sales: List',
            visible: false
        }, {
            //#endregion
            //#region Customer Routes
            //======================================================================*/
            url: 'Customers_Summary',
            moduleId: 'viewmodels/customers/summaryCustomers',
            name: 'Customers',
            caption: 'Customers: Summary',
            visible: true,
            settings: { navIcon: '<i class="icon-male"></i><i class="icon-female"></i>' }
        }, {
            url: 'Customers_List',
            moduleId: 'viewmodels/customers/listCustomers',
            name: 'Customers',
            caption: 'Customers: List',
            visible: false
        }, {
            url: 'Customers_Account_List',
            moduleId: 'viewmodels/customers/listCustomerAccounts',
            name: 'Customers',
            caption: 'Customers: Account List',
            visible: false
        }, {
            url: 'Customers_Details/:id',
            moduleId: 'viewmodels/customers/viewCustomer',
            name: 'Customers',
            caption: 'Customers: Details',
            visible: false
        }, {
            url: 'Customers_Account_Details/:id',
            moduleId: 'viewmodels/customers/viewCustomerAccount',
            name: 'Customers',
            caption: 'Customers: Account Details',
            visible: false
        }, {
            url: 'Customers_Account_Payment/:id',
            moduleId: 'viewmodels/customers/addCustomerPayment',
            name: 'Customers',
            caption: 'Customers: Add Payment',
            visible: false
        }, {
            url: 'Customers_Create',
            moduleId: 'viewmodels/customers/addCustomer',
            name: 'Customers',
            caption: 'Customers: Create',
            visible: false
        }, {
            //#endregion
            //#region Inventory Routes
            //======================================================================*/
            url: 'Inventory_Summary',
            moduleId: 'viewmodels/inventory/summaryInventory',
            name: 'Inventory',
            caption: 'Inventory: Summary',
            visible: true,
            settings: { navIcon: '<i class="icon-tags"></i>' }
        }, {
            url: 'Inventory_Add_Product',
            moduleId: 'viewmodels/inventory/addProduct',
            name: 'Inventory',
            caption: 'Inventory: Add Product',
            visible: false
        }, {
            url: 'Inventory_Delivery',
            moduleId: 'viewmodels/inventory/addDelivery',
            name: 'Inventory',
            caption: 'Inventory: Delivery Form',
            visible: false
        }, {
            url: 'Inventory_Categories',
            moduleId: 'viewmodels/inventory/listCategories',
            name: 'Inventory',
            caption: 'Inventory: Categories',
            visible: false
        }, {
            url: 'Inventory_Products/:id',
            moduleId: 'viewmodels/inventory/listProducts',
            name: 'Inventory',
            caption: 'Inventory: Product List',
            visible: false
        }, {
            url: 'Inventory_Product_Details/:id',
            moduleId: 'viewmodels/inventory/viewProduct',
            name: 'Inventory',
            caption: 'Inventory: Product Details',
            visible: false
        }, {
            url: 'Inventory_Stock_Take',
            moduleId: 'viewmodels/inventory/addStockTake',
            name: 'Inventory',
            caption: 'Inventory: Stock Take',
            visible: false
        }, {
            url: 'Inventory_Out_of_Stock',
            moduleId: 'viewmodels/inventory/listOutofStockProducts',
            name: 'Inventory',
            caption: 'Inventory: Out of Stock',
            visible: false
        }, {
            url: 'Inventory_Reorder_List',
            moduleId: 'viewmodels/inventory/listReorderProducts',
            name: 'Inventory',
            caption: 'Inventory: Reorder List',
            visible: false
        }, {
            url: 'Inventory_Report_Incident',
            moduleId: 'viewmodels/inventory/addIncident',
            name: 'Inventory',
            caption: 'Inventory: Report Incident',
            visible: false
        }, {
            url: 'Inventory_Incident_List',
            moduleId: 'viewmodels/inventory/listIncidents',
            name: 'Inventory',
            caption: 'Inventory: Incident List',
            visible: false
        }, {
            url: 'Inventory_Incident/:id',
            moduleId: 'viewmodels/inventory/viewIncident',
            name: 'Inventory',
            caption: 'Inventory: Incident',
            visible: false
        }, {
            //#endregion
            //#region Suppliers Routes
            //======================================================================*/
            url: 'Suppliers_Summary',
            moduleId: 'viewmodels/suppliers/summarySuppliers',
            name: 'Suppliers',
            caption: 'Supplier: Summary',
            visible: true,
            settings: { navIcon: '<i class="icon-strikethrough"></i>' }
        }, {
            url: 'Suppliers_List',
            moduleId: 'viewmodels/suppliers/listSuppliers',
            name: 'Suppliers',
            caption: 'Suppliers: Listing',
            visible: false
        }, {
            url: 'Supplier_Details/:id',
            moduleId: 'viewmodels/suppliers/viewSupplier',
            name: 'Suppliers',
            caption: 'Suppliers: Details',
            visible: false
        }, {
            url: 'Supplier_Account_Details/:id',
            moduleId: 'viewmodels/suppliers/viewSupplierAccount',
            name: 'Suppliers',
            caption: 'Suppliers: Account Details',
            visible: false
        }, {
            url: 'Supplier_Account_Payment/:id',
            moduleId: 'viewmodels/suppliers/addSupplierPayment',
            name: 'Suppliers',
            caption: 'Suppliers: Account Payments',
            visible: false
        }, {
            url: 'Suppliers_Order_List',
            moduleId: 'viewmodels/suppliers/listOrders',
            name: 'Suppliers',
            caption: 'Suppliers: Order List',
            visible: false
        }, {
            url: 'Supplier_Add_Supplier',
            moduleId: 'viewmodels/suppliers/addSupplier',
            name: 'Suppliers',
            caption: 'Suppliers: Add Supplier',
            visible: false
        }, {
            url: 'Supplier_Create_Order',
            moduleId: 'viewmodels/suppliers/addOrder',
            name: 'Suppliers',
            caption: 'Suppliers: Create Order',
            visible: false
        }, {
            url: 'Supplier_Create_Order/:id',
            moduleId: 'viewmodels/suppliers/addOrder',
            name: 'Suppliers',
            caption: 'Suppliers: Create Order',
            visible: false
        }, {
            url: 'Supplier_Order_Details/:id',
            moduleId: 'viewmodels/suppliers/viewOrder',
            name: 'Suppliers',
            caption: 'Suppliers: Order Details',
            visible: false
        }, {
            url: 'Suppliers_Order_Received',
            moduleId: 'viewmodels/suppliers/listReceivedOrders',
            name: 'Suppliers',
            caption: 'Suppliers: Received Orders',
            visible: false
        },{
            url: 'Suppliers_Order_Overdue',
            moduleId: 'viewmodels/suppliers/listOverdueOrders',
            name: 'Suppliers',
            caption: 'Suppliers: Overdue Orders',
            visible: false
        },{
            url: 'Suppliers_Order_Outstanding',
            moduleId: 'viewmodels/suppliers/listOutstandingOrders',
            name: 'Suppliers',
            caption: 'Suppliers: Outstanding Orders',
            visible: false
        }, {
            //#endregion
            //#region Employee Routes
            //======================================================================*/
            url: 'Employees',
            moduleId: 'viewmodels/employees/summaryEmployees',
            name: 'Employees',
            caption: 'Employees: Summary',
            visible: true,
            settings: { navIcon: '<i class="icon-group"></i>' }
        }, {
            url: 'Employee_Details/:id',
            moduleId: 'viewmodels/employees/viewEmployee',
            name: 'Employees',
            caption: 'Employees: Deatils',
            visible: false
        }, {
            //#endregion
            //#region Reports Routes
            //======================================================================*/
            url: 'Reports',
            moduleId: 'viewmodels/reports/summaryReports',
            name: 'Reports',
            caption: 'Report: Summary',
            visible: true,
            settings: { navIcon: '<i class="icon-file-text"></i>' }
        }, {
            url: 'Sales_report',
            moduleId: 'viewmodels/reports/salesReport',
            name: 'Reports',
            caption: 'Report: Summary',
            visible: false,
        }, {
            url: 'Customer_report',
            moduleId: 'viewmodels/reports/customerReport',
            name: 'Reports',
            caption: 'Report: Summary',
            visible: false,

        }, {
            url: 'Supplier_report',
            moduleId: 'viewmodels/reports/supplierReport',
            name: 'Reports',
            caption: 'Report: Summary',
            visible: false,

        }, {
            url: 'Inventory_report',
            moduleId: 'viewmodels/reports/inventoryReport',
            name: 'Reports',
            caption: 'Report: Summary',
            visible: false,
        }, {
            //#endregion
            //#region Activity Feeds Routes
            //======================================================================*/
            url: 'Activity_Feed',
            moduleId: 'viewmodels/activityFeed/summaryActivityFeed',
            name: 'Activity_Feed',
            caption: 'Activity Feed: Summary',
            visible: true,
            settings: { navIcon: '<i class="icon-rss"></i>' }
        }, {
            url: 'Sale_Activities',
            moduleId: 'viewmodels/activityFeed/saleActivities',
            name: 'Activity Feed',
            caption: 'Activity Feed: Sales',
            visible: false
        }, {
            url: 'Customer_Activities',
            moduleId: 'viewmodels/activityFeed/customerActivities',
            name: 'Activity Feed',
            caption: 'Activity Feed: Customers',
            visible: false
        }, {
            url: 'Inventory_Activities',
            moduleId: 'viewmodels/activityFeed/inventoryActivities',
            name: 'Activity Feed',
            caption: 'Activity Feed: Inventory',
            visible: false
        }, {
            url: 'Supplier_Activities',
            moduleId: 'viewmodels/activityFeed/supplierActivities',
            name: 'Activity Feed',
            caption: 'Activity Feed: Suppliers',
            visible: false
        }, {
            url: 'Order_Activities',
            moduleId: 'viewmodels/activityFeed/orderActivities',
            name: 'Activity Feed',
            caption: 'Activity Feed: Order',
            visible: false
        }, {
            //#endregion
            //#region Settings Routes
            //======================================================================*/
            url: 'settings',
            moduleId: 'viewmodels/settings/settings',
            name: 'Settings',
            caption: 'Settings',
            visible: true,
            settings: { navIcon: '<i class="icon-cogs"></i>' }
        }];
        //#endregion
        //#endregion

        //#region Routes for a Cashier

        var cashierRoutes = [{
            url: 'Profile',
            moduleId: 'viewmodels/profile',
            name: 'Profile',
            caption: 'Profile',
            visible: false
        }, {
            url: 'Quick_Links',
            moduleId: 'viewmodels/dashboard/dashboardCashier',
            name: 'Quick_Links',
            caption: 'Cashier: Quick Links',
            visible: true,
            settings: { navIcon: '<i class="icon-link"></i>' }
        }, {
            url: 'Sales_Create',
            moduleId: 'viewmodels/sales/addSales',
            name: 'Sales',
            visible: true,
            settings: { navIcon: '<i class="icon-shopping-cart"></i>' }
        }, {
            url: 'Inventory',
            moduleId: 'viewmodels/inventory/listCategories',
            name: 'Inventory',
            caption: 'Inventory: Category List',
            visible: true,
            settings: { navIcon: '<i class="icon-tags"></i>' }
        }, {
            url: 'Inventory_Products/:id',
            moduleId: 'viewmodels/inventory/listProducts',
            name: 'Inventory',
            caption: 'Inventory: Product List',
            visible: false
        }, {
            url: 'Inventory_Product_Details/:id',
            moduleId: 'viewmodels/inventory/viewProduct',
            name: 'Inventory',
            caption: 'Inventory: Product Details',
            visible: false
        }, {
            url: 'settings',
            moduleId: 'viewmodels/settings/settings',
            name: 'Settings',
            caption: 'Settings',
            visible: true,
            settings: { navIcon: '<i class="icon-cogs"></i>' }
        }];
        //#endregion

        return {
            appTitle: appTitle,
            debugEnabled: ko.observable(true),
            userRoutes: userRoutes
        };
    });