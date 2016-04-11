(function() {
    'use strict';

    angular
        .module('app.stats')
        .controller('StatsController', StatsController);

    StatsController.$inject = ['statsFactory', 'i18n'];

    function StatsController(statsFactory, i18n) {
        var vm = this;
        vm.test = 1;

        vm.rep = '';
        vm.i18n = i18n;
     
        getCommits();
        getRepos();

        function getCommits() {
            return statsFactory.getCommitsFromRepos().then(function (response) {
                vm.reposData = [response.data.commits];
                vm.labels = response.data.months;
                vm.barData = [response.data.commits];
                vm.eachDate = response.data.commitsForEverRepository;
                vm.eachSeries = response.data.repositories.map(function (el) {
                    return el.name;
                });
                vm.userInfo = response.data.userInfo;
            });
        }

        function getRepos() {
            return statsFactory.getRepos().then(function(response) {
                vm.Repos = response.data;
            });
        }

        vm.getCommitsFromCurrent = function (repo) {
            return statsFactory.getCommitsFromCurrentRepo(repo).then(function (response) {
                vm.reposData = [response.data];
            });
        };
    }
})();

