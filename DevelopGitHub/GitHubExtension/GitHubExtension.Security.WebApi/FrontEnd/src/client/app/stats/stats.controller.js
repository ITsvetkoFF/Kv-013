(function() {
    'use strict';

    angular
        .module('app.stats')
        .controller('StatsController', StatsController);

    StatsController.$inject = ['statsFactory', '$scope', 'i18n'];

    function StatsController(statsFactory, $scope, i18n) {
        var vmStatistics = this;

        $scope.rep = '';
        $scope.i18n = i18n;

        getCommits();
        getRepos();

        function getCommits() {
            return statsFactory.getCommitsFromRepos().then(function (response) {
                $scope.reposData = [response.data.commits];
                $scope.labels = response.data.months;
                $scope.barData = [response.data.commits];
                $scope.eachDate = response.data.commitsForEverRepository;
                $scope.eachSeries = response.data.repositories.map(function(el) {
                    return el.name;
                });
                $scope.userInfo = response.data.userInfo;
            });
        }

        function getRepos() {
            return statsFactory.getRepos().then(function(response) {
                $scope.Repos = response.data;
            });
        }

        $scope.getCommitsFromCurrent = function (repo) {
            return statsFactory.getCommitsFromCurrentRepo(repo).then(function (response) {
                $scope.reposData = [response.data];
            });
        };
    }
})();

