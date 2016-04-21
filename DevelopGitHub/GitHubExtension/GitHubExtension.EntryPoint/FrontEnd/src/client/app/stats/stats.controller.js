(function() {
    'use strict';

    angular
        .module('app.stats')
        .controller('StatsController', StatsController);

    StatsController.$inject = ['statsFactory', 'i18n'];

    function StatsController(statsFactory, i18n) {
        var vm = this;

        vm.rep = '';
        vm.i18n = i18n;

        getFollowers();
        getFollowing();
        getRepositoriesCount();
        getActivityMonths();
        getRepositories();
        getCommitsRepositories();
        getGroupCommits();

        function getFollowers() {
            return statsFactory.getFollowers().then(function(response) {
                vm.Followers = response.data;
            });
        }

        function getFollowing() {
            return statsFactory.getFollowing().then(function (response) {
                vm.Following = response.data;
            });
        }

        function getRepositoriesCount() {
            return statsFactory.getRepositoriesCount().then(function (response) {
                vm.RepositoriesCount = response.data;
            });
        }

        function getActivityMonths() {
            return statsFactory.getActibityMonths().then(function (response) {
                vm.labels = response.data;
            });
        }

        function getRepositories() {
            return statsFactory.getRepositories().then(function (response) {
                vm.Repositories = response.data;
                vm.eachSeries = response.data.map(function(el) {
                    return el.name;
                });
            });
        }

        function getCommitsRepositories() {
            return statsFactory.getCommitsRepositories().then(function (response) {
                vm.eachDate = response.data;
            });
        }

        function getGroupCommits() {
            return statsFactory.getGroupCommits().then(function (response) {
                vm.barData = [response.data];
                vm.reposData = [response.data];
            });
        }

        vm.getCommitsFromCurrent = function (repo) {
            return statsFactory.getCommitsFromCurrentRepo(repo).then(function (response) {
                vm.reposData = [response.data];
            });
        };
    }
})();

