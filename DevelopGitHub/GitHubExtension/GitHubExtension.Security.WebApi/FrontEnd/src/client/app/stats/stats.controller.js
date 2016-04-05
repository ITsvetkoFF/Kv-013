(function() {
    'use strict';

    angular.module('app.stats')
        .controller('statsController', statsController);
    statsController.$inject = ['statsFactory', '$scope', 'logger'];

    function statsController(statsFactory, $scope, logger) {
        $scope.rep = '';
        $scope.series = ['Series A'];

        getCommits().then(function () {
            logger.info('Got commits from GitHub!');
        });

        getRepos().then(function() {
            logger.info('Got repositories');
        });

        function getCommits() {
            return statsFactory.getCommitsFromRepos().then(function (response) {
                $scope.reposData = [response.data.commits];
                $scope.labels = response.data.months;
                $scope.barData = [response.data.commits];
                $scope.eachDate = response.data.commitsForEverRepository;
                $scope.eachSeries = response.data.repositoriesName;
            });
        }

        function getRepos() {
            return statsFactory.getRepos().then(function(response) {
                $scope.Repos = response.data;
            });
        }

        $scope.getCommitsFromCurrent = function (repo) {
            return statsFactory.getCommitsFromCurrentRepo(repo).then(function (response) {
                $scope.reposData = [response.data.commits];
            });
        };

        $scope.calculateQuantity = function () {
            logger.info('Okey!');
        };
    }
})();

