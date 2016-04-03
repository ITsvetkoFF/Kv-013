(function() {
    'use strict';

    angular.module('app.stats')
        .controller('statsController', statsController);
    statsController.$inject = ['statsFactory', '$scope', 'logger'];

    function statsController(statsFactory, $scope, logger) {

        $scope.series = ['Series A'];

        getCommits().then(function () {
            logger.info('Got commits from GitHub!');
        });

        function getCommits() {
            return statsFactory.getCollaborators().then(function (response) {
                $scope.data = [response.data.commits];
                $scope.labels = response.data.months;
            });
        }
    }
})();

