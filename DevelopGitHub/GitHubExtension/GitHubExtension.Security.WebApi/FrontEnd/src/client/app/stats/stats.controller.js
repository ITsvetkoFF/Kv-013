(function() {
    'use strict';

    angular.module('app.stats')
        .controller('statsController', statsController);
    statsController.$inject = ['$scope'];

    function statsController($scope) {
        $scope.labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
        $scope.series = ['Series A'];
        $scope.data = [
            [65, 59, 80, 81, 56, 55, 40],
            [28, 48, 40, 19, 86, 27, 90]
        ];
        $scope.onClick = function(points, evt) {
            console.log(points, evt);
        };
    }
})();
