/**
 * Created by Vladyslav on 26.02.2016.
 */

(function () {
    'use strict';
    var controllerId = 'AuthorizationController';
    angular.module('app.authorization')
        .controller(controllerId, authorizationController);

    authorizationController.$inject = ['common', 'Authorization'];

    function authorizationController(common, authorizationService) {
        var vm = this;
        vm.user = {};
        vm.submit = authorizationFormHandler;

        // Log Functions
        var getLogFn = common.logger.getLogFn;
        var logInfo = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'error');
        var logSuccess = getLogFn(controllerId, 'success');


        activate();


        function activate() {
            // Empty promises for now
            var promises = [];
            common.activateController(promises, controllerId)
                .then(function () { logInfo('Activated Authorization View'); });
            
        }

        function authorizationFormHandler() {
            authorizationService.signIn(vm.user).then(successFn, errorFn);
        }

        /**
         *
         * @param response
         * The response object has these properties:
         * - data – {string|Object} – The response body transformed with the transform functions.
         * - status – {number} – HTTP status code of the response.
         * - headers – {function([headerName])} – Header getter function.
         * - config – {Object} – The configuration object that was used to generate the request.
         * - statusText – {string} – HTTP status text of the response.
         */
        function successFn(response) {
            logSuccess("You are successfully signed in as " + response.data.login);
        }


        function errorFn(response) {
            logError("Sing in operation failed, check your username and password and try again");
        }
    }
})();
