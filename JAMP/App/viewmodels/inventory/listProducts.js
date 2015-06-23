define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'services/model'],
    function (datacontext, router, paging, model) {

        var subscription,                    // Subscribe to DB changes
            id = 0,
            filter = ko.observable(""),
            productList = ko.observableArray([]),
            user = ko.observable();

        //#region Durandal Methods
        var activate = function (routeData) {
            id = parseInt(routeData.id, 10);
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
            bindEventToList(view, '.item-con', gotoDetails);
        };

        var canDeactivate = function () {
            subscription.dispose();
            filter("");
            return true;
        };
        //#endregion

        //#region Visible methods

        //Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        var addProduct = function () {
            return router.navigateTo('#/Inventory_Add_Product');
        };

        //Check if not cashier
        var canEdit = ko.computed(function () {
            if (datacontext.currentUser().roleName() === 'Cashier') {
                return false;
            }
            else { return true; }
        });

        // Filter Products
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return productList();
            } else {
                return ko.utils.arrayFilter(productList(), function (item) {
                    return ko.utils.stringStartsWith(item.fullName().toLowerCase(), search);
                });
            }
        });

        // Check if list is empty and search term entered
        var emptyList = ko.computed(function () {
            return (filteredItems() === 0) && (filter() != "");
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible methods
            emptyList: emptyList,
            goBack: goBack,
            canEdit: canEdit,
            addProduct: addProduct,
            // Binding Observables
            productList: productList,
            filter: filter,
            filteredItems: filteredItems,
            // UI Methods
            title: 'Products'
        };

        return vm;

        //#region Internal Helper methods

        //Go to product Details
        function gotoDetails(selectedProduct) {
            if (selectedProduct && selectedProduct.productID()) {
                var url = '#/Inventory_Product_Details/' + selectedProduct.productID();
                router.navigateTo(url);
            }
        }

        //bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var p = ko.dataFor(this);
                callback(p);
                return false;
            });
        }

        // Refresh Page
        function refreshPage() {
            var where = {
                firstParm: 'categoryID',
                operater: '==',
                secondParm: id
            };

            var temp = ko.observableArray();
            return datacontext.getEntityList(temp, datacontext.entityAddress.category, false, null, where, null, null, 'productList').then(removeArchived);

            function removeArchived() {
                productList([]);
                ko.utils.arrayForEach(temp()[0].productList, function (product) {
                    if (product.archived() === false) {
                        productList.push(product);
                        return;
                    }
                });
                return true;
            }
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().product()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }

        //#endregion

    });

