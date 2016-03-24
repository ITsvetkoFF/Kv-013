(function () {
    'use.strict';

    angular
    .module('app.login')
    .factory('authService', ['$cookies', function ($cookies) {

        var authServiceFactory = {};
        
        var _logOut = function () {

            $cookies.remove('userName');
            $cookies.remove('isAuth');
            window.location.reload();

        };

        authServiceFactory.logOut = _logOut;
        
        return authServiceFactory;
    }]);
})();