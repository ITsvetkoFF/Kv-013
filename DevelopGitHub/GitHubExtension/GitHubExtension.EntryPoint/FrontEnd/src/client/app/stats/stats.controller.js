(function() {
    'use strict';

    angular
        .module('app.stats')
        .controller('StatsController', StatsController);

    StatsController.$inject = ['$q', 'statsFactory', 'i18n', 'userService'];

    function StatsController($q, statsFactory, i18n, userService) {
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
            vm.repo = userService.getCurrentRepository();
            if (!!vm.repo) {
                var promises = [];
                promises.push(
                    getFollowers(),
                    getFollowing(),
                    getActivityMonths(),
                    getRepositories(),
                    getRepositoriesNames(vm.repositories),
                    getRepositoriesCount(vm.repositories),
                    getCommitsRepositories(),
                    getCommitsFromCurrent(userService.getCurrentRepository()),
                    getGroupCommits());

                $q.all(promises);
            }
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

        function getRepositoriesCount(repositoryList) {
            vm.repositoriesCount = statsFactory.getRepositoriesCount(repositoryList);
        }

        function getActivityMonths() {
            return statsFactory.getActivityMonths().then(function (response) {
                vm.labels = response.data;
            });
        }

        function getRepositories() {
            vm.repositories = userService.getRepositoryList();
        }

        function getRepositoriesNames(repositoryList) {
            vm.eachSeries = statsFactory.getRepositoriesNames(repositoryList);
        }

        function getCommitsRepositories() {
            return statsFactory.getCommitsRepositories().then(function (response) {
                vm.eachData = response.data;
            });
        }

        function getGroupCommits() {
            return statsFactory.getGroupCommits().then(function (response) {
                vm.barData = response;
            });
        }

        function getCommitsFromCurrent(repository) {
            return statsFactory.getCommitsFromCurrentRepo(repository).then(function (response) {
                vm.reposData = response;
            });
        }
    }
})();

