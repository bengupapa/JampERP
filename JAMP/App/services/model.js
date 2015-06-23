define([
    'durandal/system',
    'services/logger'],
    function (system, logger) {

        var entityNames = {
            category: 'Category',
            product: 'Product',
            supplier: 'Supplier',
            supplierAccount: 'SupplierAccount',
            supplierPayment: 'SupplierPayment',
            customer: 'Customer',
            customerAccount: 'CustomerAccount',
            customerPayment: 'CustomerPayment',
            order: 'Order',
            business: 'Business',
            sale: 'Sale',
            salesItem: 'SalesItem',
            employee: 'Employee',
            incident: 'Incident',
            productOrder: 'ProductOrder',
            productIncident: 'ProductIncident',
            productDelivery: 'ProductDelivery',
            delivery: 'Delivery',
            userSettings: 'SettingUser'
        };

        var model = {
            configureMetadataStore: configureMetadataStore,
            entityNames: entityNames
        };

        return model;

        //Configures Breeze managers Metadata Store
        function configureMetadataStore(metadataStore) {
            metadataStore.registerEntityTypeCtor(entityNames.category, null, categoryInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.employee, null, employeeInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.customer, null, customerInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.incident, null, incidentInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.productOrder, null, productOrderInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.product, null, productInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.order, null, orderInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.productDelivery, null, productDeliveryInitializer);
            metadataStore.registerEntityTypeCtor(entityNames.customerAccount, null, customerAccountInitializer);
            metadataStore.setEntityTypeForResourceName('UserInformation', 'Employee:#JAMP.Models');
            metadataStore.setEntityTypeForResourceName('CustomerPayment', 'CustomerPayment:#JAMP.Models');
            metadataStore.setEntityTypeForResourceName('SupplierPayment', 'SupplierPayment:#JAMP.Models');
            metadataStore.setEntityTypeForResourceName('Delivery', 'Delivery:#JAMP.Models');
        }

        // Add Full product name
        // Add number of spare units
        // Add stock count holder
        // Add turnover of stock
        // Add days left till reorder
        // Add profit margin
        function productInitializer(product) {
            product.fullName = ko.computed(function () {
                return product.brandName() + ' ' + product.productName() + ' ' + product.size();
            });
            product.spareStock = ko.computed(function () {
                return product.quantity() - product.reorderLevel()
            });
            product.stockCountQuantity = ko.observable(0);
            product.turnOverRate = ko.computed(function () {
                var totalQuantity = 0;
                ko.utils.arrayForEach(product.salesItems(), function (saleItem) {
                    var saleDate = moment(saleItem.sale().createdDate(), "YYYY-MM-DD, h:mm a");
                    var nowDateThirty = moment().subtract('days', 30);
                    if (moment(saleDate).isAfter(nowDateThirty)) {
                        totalQuantity += saleItem.quantity();
                    }
                });
                return totalQuantity > 0 ? (totalQuantity / 30).toFixed(2) : 0;
            });
            product.closeReorder = ko.computed(function () {
                if (product.turnOverRate() != 0) {
                    return Math.round(product.spareStock() / product.turnOverRate())
                }
                return 0;
            });
            product.lastSold = ko.computed(function () {
                if (product.salesItems().length != 0) {
                    var lastIndex = product.salesItems().length - 1;
                    var sale = product.salesItems()[lastIndex].sale();
                    return moment(sale.createdDate(), "YYYY-MM-DD, h:mm a").format("D MMM YYYY");
                }
                return null;
            });
            product.profitMargin = ko.computed(function () {
                if (product.sellingPrice() != null && product.costPrice() != null) {
                    return (((product.sellingPrice() - product.costPrice()) / product.costPrice())*100).toFixed(2);
                }
                return null;
            });
        }

        // Add Count of active products
        function categoryInitializer(category) {
            var count = 0;
            // Reset count
            if (count > 0) {count = 0;}
            category.activeCount = ko.computed(function () {
                ko.utils.arrayForEach(category.productList(), function (product) {
                    if (product.archived() === false) {
                        count += 1;
                    }
                });
                return count;
            });
        }

        // Add Full to employee
        // Placeholder for deviceID
        // Employee display date created
        function employeeInitializer(employee) {
            employee.fullName = ko.computed(function () {
                return employee.firstName() + ' ' + employee.lastName();
            });
            employee.device = ko.observable();
            employee.displayDate = ko.computed(function () {
                return moment(employee.createdDate(), "YYYY-MM-DD, h:mm a").format("D MMM YYYY");
            });
        }

        // Add Full to Customer
        function customerInitializer(cusomter) {
            cusomter.fullName = ko.computed(function () {
                return cusomter.customerName() + ' ' + cusomter.customerSurname();
            });
        }

        // Add amount owing limit
        function customerAccountInitializer(customerAccount) {
            customerAccount.nearLimit = ko.computed(function () {
                return customerAccount.amountOwing() *0.8;
            });
        }

        // Add date name to incident
        function incidentInitializer(incident) {
            incident.dateName = ko.computed(function () {
                return moment(incident.createdDate(), "YYYY-MM-DD, h:mm a").format("D MMMM YYYY");
            });
        }

        // Add Order created display Date
        // Add order due display Date
        function orderInitializer(order) {
            order.displayDateCreated = ko.computed(function () {
                return moment(order.createdDate(), "YYYY-MM-DD, h:mm a").format("D MMM YYYY");
            });
            order.displayDateDue = ko.computed(function () {
                if (order.dateDue()) {
                    return moment(order.dateDue(), "YYYY-MM-DD").format("D MMM YYYY");
                } else {
                    return 'NoDate';
                }
            });
        }

        // Add temporary value to delivered items
        // Add status of product order
        function productOrderInitializer(productOrder) {
            productOrder.tempDelivery = productOrder.quantityDelivered();
            productOrder.itemOrderComplete = ko.computed(function (value) {
                if (productOrder.quantityDelivered() === 0) {
                    if (productOrder.tempDelivery > 0) {
                        return 'Partial';
                    }
                    return 'Outstanding';
                } else if (productOrder.quantityOrdered() === productOrder.quantityDelivered()) {
                    return 'Complete';
                } else if (productOrder.quantityDelivered() > 0 && productOrder.quantityDelivered() < productOrder.quantityOrdered()) {
                    return 'Partial';
                } else {
                    return 'Over';
                }
            });
        }

        // Add quantity needed holder
        function productDeliveryInitializer(productDelivery) {
            productDelivery.quantityNeeded = ko.observable(null);
        }

        // Log messages
        function log(msg, data, showToast, toastType, toastTitle) {
            logger.log(msg, data, system.getModuleId(model), showToast, toastType, toastTitle);
        }
    });