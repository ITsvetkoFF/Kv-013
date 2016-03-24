(function () {
    'use.strict';

    angular.module('app')
    .controller('topNavController', ['$cookies', '$scope', '$location', 'authService', function ($cookies, $scope, $location, authService) {

        $scope.logOut = function () {
            authService.logOut();
        }

        $scope.userName = $cookies.get('userName');
        $scope.isAuth=$cookies.get('isAuth');

        }]);
})();