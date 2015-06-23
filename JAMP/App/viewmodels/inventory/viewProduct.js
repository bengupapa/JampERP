define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/system',
    'durandal/app',
    'services/logger',
    'services/model'],
    function (datacontext, router, paging, system, app, logger, model) {
        var id = 0,
            subscription = null,                    
            product = ko.observable(),
            user = ko.observable(),
            categoryList = ko.observableArray([]),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false),
            canDelete = ko.observable();

        //#region Durandal Methods
        var activate = function (routeData) {
            // Get the id from the query string
            id = parseInt(routeData.id, 10);
            // Reset the can delete observable
            canDelete(false);
            // Get the details of the current logged in user
            user(datacontext.currentUser());
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
        };

        var canDeactivate = function () {
            if (isDeleting()) { return false; }

            if (hasChanges()) {
                var msg = 'Cancel changes made to "' +
                   product().fullName() + '" ?';
                var title = 'Navigate away?';

                return app.showMessage(msg, title, ['Yes', 'No'])
                    .then(checkAnswer);
            }
            subscription.dispose();
            return true;

            function checkAnswer(selectedOption) {
                if (selectedOption === 'Yes') {
                    subscription.dispose();
                    cancel();
                }
                return selectedOption;
            }
        };
        //#endregion

        //#region Visible methods

        //Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        //Observable has changes
        var hasChanges = ko.computed(function () {
            return datacontext.hasChanges();
        });

        //Cancel any changes made
        var cancel = function () {
            datacontext.cancelChanges();
        };

        //Check is there are changes
        //and object is not already saving
        var canSave = ko.computed(function () {
            return hasChanges() && !isSaving();
        });

        //Save changes
        var save = function (reason, msgName) {
            isSaving(true);
            if (reason != datacontext.saveReason.deletion) {
                reason = datacontext.saveReason.update;
                msgName = product().fullName();
            }
            return datacontext.saveChanges(reason, msgName)
                .fin(complete);

            function complete() {
                isSaving(false);
            }
        };

        //Delete the current product
        var deleteProduct = function () {
            var msg = 'Delete product "' + product().fullName() + '" ?';
            var title = 'Confirm Delete';
            isDeleting(true);
            return app.showMessage(msg, title, ['Yes', 'No'])
                .then(confirmDelete);

            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {
                    product().archived(true);
                    product().archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                    save(datacontext.saveReason.deletion, product().fullName())
                        .then(success)
                        .fail(failed)
                        .fin(finish);
                }
                isDeleting(false);

                function success() {
                    router.navigateBack();
                }

                function failed(error) {
                    cancel();
                    var errorMsg = 'Error: ' + error.message;
                    logger.logError(errorMsg, error, system.getModuleId(vm), true);
                }

                function finish() {
                    return selectedOption;
                }
            }
        };

        //Check if not cashier
        var canEdit = ko.computed(function () {
            if (user() != null && user().roleName() === 'Cashier') {
                return false;
            }
            else { return true; }
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods
            cancel: cancel,
            canEdit: canEdit,
            canSave: canSave,
            save: save,
            hasChanges: hasChanges,
            canDelete: canDelete,
            deleteProduct: deleteProduct,
            goBack: goBack,
            // Binding Observables
            product: product,
            categoryList: categoryList,
            user: user,
            // UI Methods
            title: 'Product Details '
        };

        return vm;

        //#region Internal Helper Methods
        // Refresh data on the page
        function refreshPage() {
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: false
            };

            return Q.all([
                datacontext.getEntityList(categoryList, datacontext.entityAddress.category, false, null, where),
                datacontext.getEntityDetails(id, model.entityNames.product, product)]).then(checkQuantity);

            function checkQuantity() {
                if (product().quantity() === 0) {
                    canDelete(true);
                }
                return true;
            }
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