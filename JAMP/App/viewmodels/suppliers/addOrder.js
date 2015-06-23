define([
    'durandal/app',
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'services/model'],
    function (app, datacontext, router, paging, model) {

        var subscription,                    // Subscribe to DB changes
            id = 0,
            order = ko.observable(),
            isSaving = ko.observable(false),
            supplier = ko.observable(),
            productList = ko.observableArray([]),
            productOrderList = ko.observableArray([]),
            supplierList = ko.observableArray([]);

        //#region Durandal Methods
        var activate = function (routeData) {
            // Create order entity
            order(datacontext.createObject(model.entityNames.order));
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Get ID of routed product
            if (routeData.id) {
                id = parseInt(routeData.id, 10);
            }
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Suppliers');
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                var msg = 'Do you want to leave and cancel?';
                return app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            productOrderList.removeAll();
                            datacontext.cancelChanges();
                            subscription.dispose();
                            supplier(null);
                        }

                        return selectedOption;
                    });
            }
            productOrderList.removeAll();
            subscription.dispose();
            return true;
        };
        //#endregion

        //#region Visible Methods
        var goBack = function () {
            router.navigateBack();
        };

        var cancel = function (complete) {
            router.navigateBack();
        };

        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        var canSave = ko.computed(function () {
            if (supplier() != null) {
                return hasChanges() && !isSaving() && !creditLimit() && productOrderList().length > 0;
            }
            else { return false; }
        });

        var save = function () {
            isSaving(true);

            // Assign supplier to order
            order().supplier(supplier());

            // Assign computed total cost
            order().totalCost(orderTotalCost());

            // Update Supplier account balance
            supplier().supplierAccount().amountOwed(supplier().supplierAccount().amountOwed() + orderTotalCost());

            datacontext.saveChanges(datacontext.saveReason.add, model.entityNames.order)
                .fin(complete);

            function complete() {
                isSaving(false);
                router.navigateBack();
            }
        };

        // Call product search modal
        var addProduct = function () {
            showAddProductModal();
        };

        // Call supplier search modal
        var addSupplier = function () {
            showAddSupplierModal();
        };

        // Add product to list
        var addToList = function (selectedProductOrder) {
            productOrderList.push(selectedProductOrder);
        };

        // Remove product from list
        var removeFromList = function (selectedProductOrder) {
            // Add product back to product list
            productList.push(selectedProductOrder.product());
            // Remove Product Order from list
            productOrderList.remove(selectedProductOrder);
            // Detach entity from cache
            datacontext.entityDetached(selectedProductOrder);
        };

        // Increase the quantity of product
        var increaseQuantity = function (selectedProductOrder) {
            var currentVal = selectedProductOrder.quantityOrdered();
            selectedProductOrder.quantityOrdered(currentVal + 1);
        };

        // Decrease the quantity of product
        var decreaseQuantity = function (selectedProductOrder) {
            var currentVal = selectedProductOrder.quantityOrdered();
            if (currentVal >= 2) {
                selectedProductOrder.quantityOrdered(currentVal - 1);
            }
        };

        // Order Total cost
        var orderTotalCost = ko.computed(function () {
            var totalCost = 0;
            if (productOrderList().length > 0) {
                ko.utils.arrayFilter(productOrderList(), function (productOrder) {
                    totalCost += productOrder.product().costPrice() * productOrder.quantityOrdered();
                });
            }
            return totalCost;
        });

        // Check credit limit
        var creditLimit = ko.computed(function () {
            if (supplier() != null) {

                var totalAmount = supplier().supplierAccount().amountOwed() + orderTotalCost();
                if (supplier().supplierAccount().creditLimit() > totalAmount) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return false;
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            canSave: canSave,
            cancel: cancel,
            hasChanges: hasChanges,
            save: save,
            goBack: goBack,
            addProduct: addProduct,
            addSupplier: addSupplier,
            removeFromList: removeFromList,
            increaseQuantity: increaseQuantity,
            decreaseQuantity: decreaseQuantity,
            creditLimit: creditLimit,
            // Binding Observables
            order: order,
            supplier: supplier,
            productOrderList: productOrderList,
            orderTotalCost: orderTotalCost,
            // UI Methods
            title: 'Create Order',
        };

        return vm;

        //#region Internal Helper methods

        // Activate the add product modal
        function showAddProductModal() {
            app.showModal('viewmodels/inventory/findProductModal', productList)
                .then(function (result) {

                    if (result && result != 'cancel') {
                        createProductOrderItem(result);
                    }
                });
        }

        // Activate the add supplier modal
        function showAddSupplierModal() {
            app.showModal('viewmodels/suppliers/findSupplierModal', supplierList)
                .then(function (result) {

                    if (result && result != 'cancel') {

                        supplier(result);

                    }
                });
        }

        // Create the product order
        function createProductOrderItem(product) {
            var productOrder = datacontext.createObject(model.entityNames.productOrder);

            productList.remove(product); // Remove from list

            productOrder.product(product);
            productOrder.quantityOrdered(1);

            productOrder.order(order()); // link order to product order
            return addToList(productOrder);
        }

        // Refresh page
        function refreshPage() {
            var product = ko.observable();
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: 'false'
            };

            return Q.all([
                datacontext.getEntityList(supplierList, datacontext.entityAddress.supplier, false, null, where),
                datacontext.getEntityList(productList, datacontext.entityAddress.product, false, null, where)
            ]).then(checkProduct);

            // Check if there is a query string
            function checkProduct() {
                if (id != 0) {
                    datacontext.getEntityDetails(id, model.entityNames.product, product).then(updateProductList);
                }
                return true;
            }

            // Add product if found
            function updateProductList() {
                return createProductOrderItem(product());
            }

        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().product()) {
                    refreshPage().then(removedProducts);
                }
                datacontext.completedRefresh();
            }

            // Remove products already in incident
            function removedProducts() {
                return ko.utils.arrayForEach(productOrderList(), function (productOrder) {
                    return productList.remove(productOrder.product());
                });
            }
        }

        //#endregion

    });