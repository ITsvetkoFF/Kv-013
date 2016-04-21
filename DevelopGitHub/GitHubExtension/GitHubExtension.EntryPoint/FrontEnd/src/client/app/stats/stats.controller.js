(function() {
    'use strict';

    angular
        .module('app.stats')
        .controller('StatsController', StatsController);

    StatsController.$inject = ['$q', 'statsFactory', 'i18n'];

    function StatsController($q, statsFactory, i18n) {
        var vm = this;
        vm.i18n = i18n;

        vm.followers = i18n.message.WAITING;
        vm.following = i18n.message.WAITING;
        vm.repositoriesCount = i18n.message.WAITING;
        vm.labels = [];
        vm.repositories = [];
        vm.eachSeries = [];
        vm.eachData = [];
        vm.barData = [];
        vm.reposData = [];
        vm.rep = '';

        activate();

        function activate() {
            var promises = [];
            promises.push(
                getFollowers(),
                getFollowing(),
                getRepositoriesCount(),
                getActivityMonths(),
                getRepositories(),
                getRepositoriesNames(),
                getCommitsRepositories(),
                getGroupCommits());

            $q.all(promises);
        }

        function getFollowers() {
            return statsFactory.getFollowers().then(function(response) {
                vm.followers = response.data;
            });
        }

        function getFollowing() {
            return statsFactory.getFollowing().then(function (response) {
                vm.following = response.data;
            });
        }

        function getRepositoriesCount() {
            return statsFactory.getRepositoriesCount().then(function (response) {
                vm.repositoriesCount = response.data;
            });
        }

        function getActivityMonths() {
            return statsFactory.getActivityMonths().then(function (response) {
                vm.labels = response.data;
            });
        }

        function getRepositories() {
            return statsFactory.getRepositories().then(function (response) {
                vm.repositories = response.data;
                vm.eachSeries = response.data;
            });
        }

        function getRepositoriesNames() {
            return statsFactory.getRepositoriesNames().then(function (response) {
                vm.eachSeries = response;
            });
        }

        function getCommitsRepositories() {
            return statsFactory.getCommitsRepositories().then(function (response) {
                vm.eachData = response.data;
            });
        }

        function getGroupCommits() {
            return statsFactory.getGroupCommits().then(function (response) {
                vm.barData = response;
                vm.reposData = response;
            });
        }

        vm.getCommitsFromCurrent = function (repo) {
            return statsFactory.getCommitsFromCurrentRepo(repo).then(function (response) {
                vm.reposData = response;
            });
        };
    }
})();

