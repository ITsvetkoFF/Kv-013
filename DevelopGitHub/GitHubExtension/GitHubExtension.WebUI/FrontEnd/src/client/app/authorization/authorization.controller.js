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
           var data = new Object();
           data.Email = vm.user.username;

            $.ajax({
                type: 'POST',
                url: 'http://localhost:50859/api/Account/RegisterExternal',
                data: data,
                dataType: 'json'
            }).done(function (data) {
                self.result("Done!");
            });
        }

        function successFn(response) {
            logger.success('You are successfully signed in as ' + response.data.login);
        }

        function errorFn(response) {
            logger.error('Sing in operation failed, check your username and password and try again');
        }
    }
})();
