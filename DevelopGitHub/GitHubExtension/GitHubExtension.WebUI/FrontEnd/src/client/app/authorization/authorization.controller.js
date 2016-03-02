/**
 * Created by Vladyslav on 26.02.2016.
 */

(function () {
    'use strict';

    angular.module('app.authorization')
        .controller('AuthorizationController', AuthorizationController);

    AuthorizationController.$inject = ['logger', 'Authorization'];

    function AuthorizationController(logger, AuthorizationService) {
        var vm = this;
        vm.user = {};
        vm.submit = authorizationFormHandler;

        activate();


        function activate() {
            logger.info('Activated Authorization View');
        }

        function authorizationFormHandler() {
            AuthorizationService.signIn(vm.user).then(successFn, errorFn);
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
            logger.success("You are successfully signed in as " + response.data.login);

        }


        function errorFn(response) {
            logger.error("Sing in operation failed, check your username and password and try again");
        }
    }
})();
