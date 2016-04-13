(function () {
    'use strict';

    angular
    .module('app.login')
    .factory('authService', ['$cookies', '$http', function ($cookies, $http) {

        var authServiceFactory = {};
        authServiceFactory.logOut = function () {

            $cookies.remove('userName');
            $cookies.remove('isAuth');
            $http.post('http://localhost:50859/api/Account/logout', {}).finally(function () {
                window.location.reload();
            });
        };

        return authServiceFactory;
    }]);
})();
