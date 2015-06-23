define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'services/model'],
    function (datacontext, router, paging, model) {

        var subscription,                    // Subscribe to DB changes
            incidents = ko.observableArray();

        //#region Durandal Methods
        var activate = function () {
            subscription = datacontext.canRefresh.subscribe(severRefresh);
            return refreshPage();
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
            bindEventToList(view, '.item-info-con', gotoDetails);
        };

        var canDeactivate = function () {
            subscription.dispose();
            return true;
        }
        //#endregion

        //#region Visible Methods

        //Back button navigation
        var goBack = function () {
            router.navigateBack();
        };
        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            viewAttached: viewAttached,
            canDeactivate: canDeactivate,
            // Visible Methods  
            goBack: goBack,
            // Binding Observables
            incidents: incidents,
            // UI Methods
            title: 'Incidents'
        };

        return vm;

        //#region Internal Helper methods

        //Go to incident details
        function gotoDetails(selectedIncident) {
            if (selectedIncident && selectedIncident.incidentID()) {
                var url = '#/Inventory_Incident/' + selectedIncident.incidentID();
                router.navigateTo(url);
            }
        }

        //bind click event
        function bindEventToList(rootSelector, selector, callback, eventName) {
            var eName = eventName || 'click';
            $(rootSelector).on(eName, selector, function () {
                var category = ko.dataFor(this);
                callback(category);
                return false;
            });
        }

        // Refresh data on the page
        function refreshPage() {
            return datacontext.getEntityList(incidents, datacontext.entityAddress.incident, false, 'ProductIncidents', null, null, null, null, 'incidentID');
        }

        // When new data arrives refresh page 
        function severRefresh(newValue) {
            if (newValue) {
                if (datacontext.updateList().incident()) {
                    refreshPage();
                }
                datacontext.completedRefresh();
            }
        }

        //#endregion
    });