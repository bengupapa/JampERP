define(['durandal/system'],
    function (system) {

        var logType = {
            logInfo: 'info',
            logWarning: 'warning',
            logSuccess: 'success',
            logError: 'error'
        };

        var logger = {
            log: logIt,
            logError: logError,
            logType: logType
        };

        return logger;


        function logError(message, data, source, showToast) {
            logIt(message, data, source, showToast, 'error');
        }


        function logIt(message, data, source, showToast, toastType, toastTitle) {
            source = source ? '[' + source + '] ' : '';
            if (data) {
                system.log(source, message, data);
            } else {
                system.log(source, message);
            }
            if (showToast) {
                switch (toastType) {
                    case 'info':
                        toastr.info(message, toastTitle);
                        break;
                    case 'success':
                        toastr.success(message, toastTitle);
                        break;
                    case 'warning':
                        toastr.warning(message, toastTitle);
                        break;
                    case 'error':
                        toastr.error(message, toastTitle);
                        break;
                    default:
                        toastr.info(message, toastTitle);
                }
            }
        }
    });