(function() {
    'use strict';
    angular
        .module('app.user')
        .factory('userService', userService);

    userService.$inject = ['$cookies', '$http', '$state', 'API_URL'];

    /* @ngInject */
    function userService($cookies, $http, $state, API_URL) {
        function login() {
            location.href = API_URL.LOGIN;
        }

        function logout() {
            $http.post(API_URL.LOGOUT, {}).then(function() {
                $cookies.remove('userName');
                $state.go('dashboard');
            });
        }

        function userName() {
            return $cookies.get('userName');
        }

        function isAuthenticated() {
            return !!$cookies.get('userName');
        }

        return {
            login: login,
            logout: logout,
            userName: userName,
            isAuthenticated: isAuthenticated
        };
    }
})();
