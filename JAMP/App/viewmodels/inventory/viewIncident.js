define([
    'services/datacontext',
    'durandal/plugins/router',
    'services/paging',
    'durandal/system',
    'durandal/app',
    'services/logger',
    'services/model'],
    function (datacontext, router, paging, system, app, logger, model) {
        var id,
            incident = ko.observable(),
            isSaving = ko.observable(false),
            isDeleting = ko.observable(false);

        //#region Durandal Methods
        var activate = function (routeData) {
            id = parseInt(routeData.id, 10);

            var Where = {
                firstParm: 'incidentID',
                operater: '==',
                secondParm: id
            };
            return datacontext.getEntitySingle(incident, datacontext.entityAddress.incident, false, 'productIncidents', Where);
        };

        var viewAttached = function (view) {
            paging.setActive(true, 'Inventory');
        };
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
            // Visible Methods
            goBack: goBack,
            // Binding Observables
            incident: incident,
            // UI Methods
            title: 'Incident Details '
        };

        return vm;
    });