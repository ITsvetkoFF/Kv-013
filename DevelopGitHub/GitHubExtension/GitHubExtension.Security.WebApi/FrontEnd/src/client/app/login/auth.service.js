(function () {
    'use strict';

    angular
    .module('app.login')
    .factory('authService', ['$cookies', function ($cookies) {

        var authServiceFactory = {};
        authServiceFactory.logOut = function () {

            $cookies.remove('userName');
            $cookies.remove('isAuth');
            window.location.reload();

        };

        return authServiceFactory;
    }]);
})();
