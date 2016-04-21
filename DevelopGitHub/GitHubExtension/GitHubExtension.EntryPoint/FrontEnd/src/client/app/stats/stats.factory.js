(function () {
    'use strict';
    var module = angular.module('app.stats');

    module.factory('statsFactory', statsFactory);

    statsFactory.$inject = ['$http', 'API_URL'];

    function statsFactory($http, API_URL) {

        function getFollowers() {
            return $http({
                method: 'GET',
                url: API_URL.GET_FOLLOWERS
            });
        }

        function getFollowing() {
            return $http({
                method: 'GET',
                url: API_URL.GET_FOLLOWING
            });
        }

        function getRepositoriesCount() {
            return $http({
                method: 'GET',
                url: API_URL.GET_REPOSITORIESCOUNT
            });
        }

        function getActibityMonths() {
            return $http({
                method: 'GET',
                url: API_URL.GET_ACTIVITYMONTHS
            });
        }

        function getRepositories() {
            return $http({
                method: 'GET',
                url: API_URL.GET_REPOSITORIES
            });
        }

        function getCommitsRepositories() {
            return $http({
                method: 'GET',
                url: API_URL.GET_COMMITSREPOSITORIES
            });
        }

        function getGroupCommits() {
            return $http({
                method: 'GET',
                url: API_URL.GET_GROUPCOMMITS
            });
        }

        function getCommitsFromCurrentRepo(repo) {
            return $http({
                method: 'GET',
                dataType: 'string',
                url: API_URL.GET_REPOBYNAME + '/' + repo.name
            });
        }

        return {
            getFollowers: getFollowers,
            getFollowing: getFollowing,
            getRepositoriesCount: getRepositoriesCount,
            getActibityMonths: getActibityMonths,
            getRepositories: getRepositories,
            getCommitsRepositories: getCommitsRepositories,
            getGroupCommits: getGroupCommits,
            getCommitsFromCurrentRepo: getCommitsFromCurrentRepo
        };
    }
})();
