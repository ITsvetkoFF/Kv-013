(function () {
    'use.strict';

    angular.module('app')
    .controller('topNavController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

        $scope.logOut = function () {
            authService.logOut();
            $location.path('/roles');
        }

        $scope.authentication = authService.authentication;

    }]);
})();