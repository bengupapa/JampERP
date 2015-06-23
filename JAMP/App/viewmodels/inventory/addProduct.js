define([
    'durandal/app',
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'services/model'],
    function (app, datacontext, router, paging, model) {

        var subscription = null,               
            isSaving = ko.observable(false),
            product = ko.observable(),
            categoryList = ko.observableArray();

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            // Create product
            product(datacontext.createObject(model.entityNames.product));
            // Get page data then check if has categories
            return refreshPage().then(checkCategoryList);

            // Check that there are categories
            function checkCategoryList() {
                if (categoryList().length == 0) {
                    var msg = 'Please first add a category?';
                    return app.showMessage(msg, 'Missing categories', ['Ok'])
                        .then(function (selectedOption) {
                            datacontext.cancelChanges();
                            var url = '#/Inventory_Categories';
                            return router.navigateTo(url);
                        });
                }
            }
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
        };

        var canDeactivate = function () {
            if (hasChanges()) {
                var msg = 'Do you want to leave and cancel?';
                return app.showMessage(msg, 'Navigate Away', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            datacontext.cancelChanges();
                        }
                        subscription.dispose();
                        return selectedOption;
                    });
            }
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
            if (product()) {
                return hasChanges() && !isSaving() && product().category() != null;
            } else {
                return hasChanges() && !isSaving();
            }
        });

        var save = function () {
            isSaving(true);
            datacontext.saveChanges(datacontext.saveReason.add, product().fullName())
                .then(complete);

            function complete() {
                isSaving(false);
                router.navigateBack();
            }
        };

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
            // Binding Observables
            product: product,
            categoryList: categoryList,
            // UI Methods
            title: 'Add a new Product'
        };

        return vm;

        //#region Internal Helper Methods

        // Refresh Page
        function refreshPage() {
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: false
            };
            return datacontext.getEntityList(categoryList, datacontext.entityAddress.category, false, null, where);
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().category()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }

        //#endregion
    });