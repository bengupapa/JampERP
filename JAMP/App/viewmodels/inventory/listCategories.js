define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/app',
    'services/model'],
    function (datacontext, router, paging, app, model) {

        var subscription = null,                    
            categoryList = ko.observableArray([]),
            filter = ko.observable(""),
            user = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);

        //#region Durandal Methods
        var activate = function () {
            // Subscribe to DB changes
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            if ($(window).width() < 940) {
                var app = window.App;
                app.closeNav();
            }
            paging.setActive(true, 'Inventory');
            bindEventToList(view, '.item-info-con', gotoDetails);
            bindEventToList(view, '.cat-del', deleteCategory);
            bindEventToList(view, '.cat-edit', editCategory);
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        };
        //#endregion
         
        //#region Visible Methods

        // Back button navigation
        var goBack = function () {
            router.navigateBack();
        };

        // Add category
        var addCategory = function (item) {
            showCategoryModal();
        };
        
        // Check if not cashier
        var canEdit = ko.computed(function () {
            if (datacontext.currentUser().roleName() === 'Cashier') {
                return false;
            }
            else { return true; }
        });

        // Filter Categories
        var filteredItems = ko.computed(function () {
            var search = filter().toLowerCase();
            if (!search) {
                return categoryList();
            } else {
                return ko.utils.arrayFilter(categoryList(), function (item) {
                    return ko.utils.stringStartsWith(item.categoryName().toLowerCase(), search);
                });
            }
        });

        var emptyList = ko.computed(function () {
            return (filteredItems() == 0) && (filter() != "");
        });
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods           
            emptyList: emptyList,
            goBack: goBack,
            canEdit: canEdit,            
            addCategory: addCategory,
            // Binding Observables
            filter: filter,
            filteredItems: filteredItems,
            categoryList: categoryList,
            // UI Methods
            title: 'Categories'
        };

        return vm;

        //#region Internal Helper Methods

        // Go to product list
        function gotoDetails(selectedCategory) {
            if (selectedCategory && selectedCategory.categoryID()) {
                var url = '#/Inventory_Products/' + selectedCategory.categoryID();
                router.navigateTo(url);
            }
        }

        // Edit category
        function editCategory(selectedCategory) {
            if (selectedCategory && selectedCategory.categoryID()) {
                var category = ko.observable();
                datacontext.getEntityDetails(selectedCategory.categoryID(), model.entityNames.category, category);
                showCategoryModal(category);
            }
        }

        // Delete the current category
        function deleteCategory(selectedCategory) {
            if (selectedCategory && selectedCategory.categoryID()) {

                var msg = 'Delete category "' + selectedCategory.categoryName() + '" ?';
                var title = 'Confirm Delete';
                isDeleting(true);
                return app.showMessage(msg, title, ['Yes', 'No'])
                    .then(confirmDelete);
            }

            // Confirm the deletion
            function confirmDelete(selectedOption) {
                if (selectedOption === 'Yes') {
                    selectedCategory.archived(true);
                    selectedCategory.archivedDate(moment().format("YYYY-MM-DD, h:mm a"));
                    save(datacontext.saveReason.deletion, selectedCategory.categoryName())
                        .fail(failed)
                        .fin(finish);
                }
                isDeleting(false);

                function failed(error) {
                    cancel();
                    var errorMsg = 'Error: ' + error.message;
                    logger.logError(errorMsg, error, system.getModuleId(vm), true);
                }

                function finish() {
                    return selectedOption;
                }
            }
        }

        // Bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        // Save changes
        function save(reason, msgName) {
            isSaving(true);

            return datacontext.saveChanges(reason, msgName)
                .fin(complete);

            function complete() {
                isSaving(false);
                refreshPage();
            }
        }

        // Refresh data on the page
        function refreshPage() {
            var where = {
                firstParm: 'archived',
                operater: '==',
                secondParm: false
            };
            return datacontext.getEntityList(categoryList, datacontext.entityAddress.category, false, null, where);
        }

        // Activiate the category modal
        function showCategoryModal(object) {
            app.showModal('viewmodels/inventory/categoryModal', object)
                .then(function (result) {
                    if (!result) {
                        datacontext.cancelChanges();                     
                    }
                    refreshPage();
                });
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