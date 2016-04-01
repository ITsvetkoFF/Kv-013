(function () {
    'use strict';

    angular.module('app')
    .controller('topNavController', ['$cookies', '$scope', 'authService', function ($cookies, $scope, authService) {

        $scope.logOut = function() {
                authService.logOut();
            };

        $scope.userName = $cookies.get('userName');
        $scope.isAuth = $cookies.get('isAuth');

    }]);
})();
