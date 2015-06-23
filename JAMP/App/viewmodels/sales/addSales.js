define([
    'services/datacontext',
    'services/logger',
    'services/model',
    'durandal/app',
    'durandal/plugins/router',
    'services/paging', ],
    function (datacontext, logger, model, app, router, paging) {

        var productlist = ko.observableArray([]),
            customerlist = ko.observableArray([]),
            salelist = ko.observableArray([]),
            sale = ko.observable(),
            saleItem = ko.observable(),
            isSaving = ko.observable(false),
            hasCustomer = ko.observable(false),
            filter = ko.observable("");

        var where = {
            firstParm: 'quantity',
            operater: '>',
            secondParm: 0
        };

        //#region Durandal Methods
        var activate = function () {
            if (!saleDisable()) {
                sale(datacontext.createObject(model.entityNames.sale));
                return Q.all([
                    datacontext.getEntityList(customerlist, datacontext.entityAddress.customer),
                    datacontext.getEntityList(productlist, datacontext.entityAddress.product, false, null, where)]);
            } else { return true; }
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            paging.setActive(true, 'Sales');
            bindEventToList(view, '.increaseQuantity', increaseQuantity);
            bindEventToList(view, '.decreaseQuantity', decreaseQuantity);
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                if (!datacontext.currentUser().online())
                {
                    resetPage();
                    return true;
                }
                var msg = 'Do you want to cancel the sale?';
                return app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {

                            ko.utils.arrayForEach(salelist(), function (saleItems) {
                                saleItems.product(null);
                                datacontext.entityDetached(saleItems);
                            });
                            resetPage();
                            datacontext.cancelChanges();
                        }
                        return selectedOption;
                    });
            }
            resetPage();
            return true;
        };
        //#endregion

        //#region Visible methods
        var goBack = function () {
            router.navigateBack();
        };

        //Filter Products
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return;
            } else {
                return ko.utils.arrayFilter(productlist(), function (item) {
                    return ko.utils.stringStartsWith(item.fullName().toLowerCase(), search);
                });
            }
        });

        // Add product to sales list
        var addItem = function (selectedProduct) {
            //remove product from search list
            productlist.remove(selectedProduct);

            //Add product + sale to a sale item + current price
            saleItem = datacontext.createObject(model.entityNames.salesItem);
            saleItem.product(selectedProduct);
            saleItem.price(selectedProduct.sellingPrice());
            saleItem.costPrice(selectedProduct.costPrice());
            saleItem.sale(sale());
            saleItem.quantity(1);
            salelist.push(saleItem);
        };

        // Call search customer modal
        var addCustomer = function () {
            showCustomerModal();
        };

        // Call amount received modal
        var enterReceivedAmount = function () {
            showAmountReceivedModal();
        };

        // Call sale summary modal if credit sale
        var finishSale = function () {
            sale().credit(true);  
            completeSale();
            showSaleSummaryModal(sale);
        };

        // Remove the customer from the sale
        var removeCustomer = function (acustomer) {

            // Add customer back to the customer list
            customerlist.push(acustomer);

            // Remove customer from the sale
            sale().customer(null);

            // Re enable the customer add button
            hasCustomer(false);
        };

        // Remove sales item
        var removeItem = function (selectSaleItem) {
            // Remove Product Sales from list
            salelist.remove(selectSaleItem);
            // Add product back to product list
            productlist.push(selectSaleItem.product());
            // Remove product from a sales item
            selectSaleItem.product(null);
            // Detach entity from cache
            datacontext.entityDetached(selectSaleItem);
        };

        // Calculate the total sales cost
        var saleTotal = ko.computed(function () {
            var totalamount = 0;
            for (i = 0; i < salelist().length; i++) {
                totalamount += salelist()[i].price() * salelist()[i].quantity();
            }

            return totalamount.toFixed(2);
        });

        // Calculate the change for a sale
        var saleChange = ko.computed(function () {
            if (sale()) {
                var salechange = 0;
                salechange = sale().amountReceived() - saleTotal();

                return salechange.toFixed(2);
            }
            return 0;

        });

        // Check if there are changes to entities
        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        // Check if can make a sale
        var saleDisable = ko.computed(function () {
            return datacontext.currentUser().business().disableSales() === 'On';
        });

        // Check credit limit
        var checkCustomerCredit = ko.computed(function () {
            var total = 0;
            if (hasCustomer()){
                if (sale().customer()) {
                    total = sale().customer().customerAccount().creditLimit().toFixed(2) - sale().customer().customerAccount().amountOwing().toFixed(2);
                    return total < saleTotal();
                }
            }
            return false;
        });

        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            canDeactivate: canDeactivate,
            viewAttached: viewAttached,
            // Visible Methods
            addItem: addItem,
            addCustomer: addCustomer,
            removeCustomer: removeCustomer,
            goBack: goBack,
            removeItem: removeItem,
            saleTotal: saleTotal,
            sale: sale,
            saleChange: saleChange,
            enterReceivedAmount: enterReceivedAmount,
            hasChanges: hasChanges,
            hasCustomer: hasCustomer,
            finishSale: finishSale,
            saleDisable: saleDisable,
            checkCustomerCredit: checkCustomerCredit,
            // Binding Observables
            filter: filter,
            filteredItems: filteredItems,
            salelist: salelist,
            // UI Methods
            title: 'Sales'
        };

        return vm;

        //#region Internal Helper Methods

        // Increase the quantity of saleitems
        function increaseQuantity(selectedSalesItem) {
            var currentVal = selectedSalesItem.quantity();
            if (currentVal < selectedSalesItem.product().quantity()) {
                selectedSalesItem.quantity(currentVal + 1);
            }
        };

        // Decrease the quantity of saleitems
        function decreaseQuantity(selectedSalesItem) {
            var currentVal = selectedSalesItem.quantity();
            if (currentVal >= 2) {
                selectedSalesItem.quantity(currentVal - 1);
            }
        };

        // Show sale amounts modal
        function showSaleSummaryModal(object) {
            app.showModal('viewmodels/sales/saleSummaryModal', object)
            .then(function (result) {
                if (result && result !== 'cancel') {
                    router.navigateBack();
                }
            });
        }

        // Show search customer modal
        function showCustomerModal() {
            app.showModal('viewmodels/customers/findCustomerModal', customerlist)
                .then(function (result) {

                    if (result && result !== 'cancel') {

                        hasCustomer(true);

                        customerlist.remove(result);

                        sale().customer(result);
                    }
                });
        }

        // Show amount received modal
        function showAmountReceivedModal() {
            app.showModal('viewmodels/sales/saleAmountReceivedModal', saleTotal())
                .then(function (result) {

                    if (result && result !== 'cancel') {

                        sale().credit(false);   // Cash sale
                        completeSale(result);

                        showSaleSummaryModal(sale);

                    }
                });
        }

        // Bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var p = ko.dataFor(this);
                callback(p);
                return false;
            });
        }

        // Complete Sale 
        function completeSale(data) {
            // Check if cash or credit sale
            if (data) {
                sale().amountReceived(data);                //Amount received [Cash]
            } else {
                sale().amountReceived(saleTotal());         //Amount received from customer
            }

            sale().amountCharged(saleTotal());              //Total sale price
            sale().change(saleChange());                    //Change given to customer
        }

        // Reset page
        function resetPage() {
            hasCustomer(false);
            filter("");
            salelist([]);
        }
        //#endregion

    });