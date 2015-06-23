define([
    'durandal/app',
    'services/model'],
    function (app, model) {

        var amount = ko.observable(''),
            disablePeriod = ko.observable(false),
            result = '',
            requiredAmount = ko.observable(null),
            periodClick = false,
            periodAfter = 0;

        //#region Durandal Methods
        var activate = function (data) {
            resetModal();
            requiredAmount(data);
            return true;
        };

        //#endregion

        //#region Visible Methods

        // Cancel and close modal
        var cancel = function () {
            this.modal.close(result = 'cancel');
        };

        // Add number to the amount string
        var addNumber = function (data) {

            // Check that only 2 numbers after period
            if (periodAfter < 2) {

                // Increment the count after the period
                if (periodClick) {
                    periodAfter += 1;
                }

                // Check if period is clicked
                if (data === '.') {
                    periodClick = true;
                    disablePeriod(true);
                }

                amount(amount() + data);
            }

        };

        // Remove last number from amount
        var removeNumber = function () {

            var curValue = amount();
            var lastChar = curValue.substr(curValue.length - 1); // Get last char

            // Check if last char was a period
            // re enable period functionality
            if (lastChar == '.') {
                disablePeriod(false);
                periodClick = false;
            }

            // Decrement the count after the period
            if (periodClick) {
                periodAfter -= 1;
            }

            // Remove last char
            amount(curValue.slice(0, -1));
        };

        // Check if inputted amount is greater than required amount
        var canSave = ko.computed(function () {
            if (requiredAmount() != null) {
                var currentAmount = amount().length == 0 ? 0 : parseFloat(amount(), 10);

                return (currentAmount >= parseFloat(requiredAmount()));
            }
            return false;
        });

        // Send amount received
        var sendAmount = function () {
            this.modal.close(result = parseFloat(amount(), 10).toFixed(2));
        };

        //#endregion

        var vm = {
            // Durandal Methods
            activate: activate,
            // Visible Methods
            cancel: cancel,
            addNumber: addNumber,
            canSave: canSave,
            removeNumber: removeNumber,
            sendAmount: sendAmount,
            disablePeriod: disablePeriod,
            // Binding Observables
            amount: amount,
            requiredAmount: requiredAmount
        };

        return vm;

        //#region Internal Helper Methods

        // Reset modal
        function resetModal() {
            amount('');             // Input amount
            disablePeriod(false);   // Re enable period button
            periodClick = false;    // Reset period click
            periodAfter = 0;        // Reset number count after period
            requiredAmount(null);     // Reset the amount required
        }
        //#endregion
    })