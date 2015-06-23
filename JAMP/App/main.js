require.config({
    paths: { "text": "durandal/amd/text" }
});

define(function(require) {
    var system = require('durandal/system'),
        app = require('durandal/app'),
        router = require('durandal/plugins/router'),
        viewLocator= require('durandal/viewLocator'),
        logger = require('services/logger');
    
    // Enable debug message to show in the console 
    system.debug(true);

    app.start().then(function () {
       
       // toastr.options.backgroundpositionClass = 'toast-bottom-right';      

        // route will use conventions for modules
        // assuming viewmodels/views folder structure
        router.useConvention();

        // Defaults to viewmodels/views/views. 
        viewLocator.useConvention();
        
        // Adapt to touch devices
        app.adaptToDevice();

        //Show the app by setting the root view model 
        app.setRoot('viewmodels/shell');

        // override bad route behavior to write to 
        // console log and show error toast
        router.handleInvalidRoute = function (route, params) {
            logger.logError('No Route Found', route, 'main', true);
        };
    });
});