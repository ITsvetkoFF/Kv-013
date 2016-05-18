(function() {
    'use strict';

    angular
        .module('app.stats')
        .controller('StatsController', StatsController);

    StatsController.$inject = ['$q', 'statsFactory', 'i18n', 'userService', 'localStorageService'];

    function StatsController($q, statsFactory, i18n, userService, localStorage) {
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
                    getCommitsFromCurrent(vm.repo),
                    getGroupCommits(),
                    getDate());

                $q.all(promises);
            }
        }

        function getFollowers() {
            if (localStorage.get('followers') !== null) {
                vm.followers = localStorage.get('followers');
            }
            else {
                return statsFactory.getFollowers().then(function (response) {
                    vm.followers = response.data;
                    localStorage.set('followers', vm.followers);
                });
            }
        }

        function getFollowing() {
            if (localStorage.get('following') !== null) {
                vm.following = localStorage.get('following');
            }
            else {
                return statsFactory.getFollowing().then(function (response) {
                    vm.following = response.data;
                    localStorage.set('following', vm.following);
                });
            }
        }

        function getRepositoriesCount(repositoryList) {
            vm.repositoriesCount = statsFactory.getRepositoriesCount(repositoryList);
        }

        function getActivityMonths() {
            if (!!localStorage.get('monthLabels')) {
                vm.labels = localStorage.get('monthLabels');
            }
            else {
                return statsFactory.getActivityMonths().then(function (response) {
                    vm.labels = response.data;
                    localStorage.set('monthLabels', vm.labels);
                });
            }
        }

        function getRepositories() {
            vm.repositories = userService.getRepositoryList();
        }

        function getRepositoriesNames(repositoryList) {
            vm.eachSeries = statsFactory.getRepositoriesNames(repositoryList);
        }

        function getCommitsRepositories() {
            if (!!localStorage.get('commitsRepositories')) {
                vm.eachData = localStorage.get('commitsRepositories');
            }
            else {
                return statsFactory.getCommitsRepositories().then(function (response) {
                    vm.eachData = response.data;
                    localStorage.set('commitsRepositories', vm.eachData);
                });
            }
        }

        function getGroupCommits() {
            if (!!localStorage.get('groupCommits')) {
                vm.barData = localStorage.get('groupCommits');
            }
            else {
                return statsFactory.getGroupCommits().then(function (response) {
                    vm.barData = response;
                    localStorage.set('groupCommits', vm.barData);
                });
            }
        }

        function getCommitsFromCurrent(repository) {
            return statsFactory.getCommitsFromCurrentRepo(repository).then(function (response) {
                vm.reposData = response;
            });
        }

        vm.refreshData = function() {
            localStorage.clearAll();
            activate();
        };

        function getDate() {
            if (!!localStorage.get('lastUpdateDate')) {
                vm.date = localStorage.get('lastUpdateDate');
            }
            else {
                vm.date = Date.now();
                localStorage.set('lastUpdateDate', vm.date);
            }
        }
    }
})();

