(function () {
    'use strict';
    var module = angular.module('app.stats');

    module.factory('statsFactory', statsFactory);

    statsFactory.$inject = ['$http', 'API_URL'];

    function statsFactory($http, API_URL) {

        function getFollowers() {
            return $http.get(API_URL.GET_FOLLOWERS);
        }

        function getFollowing() {
            return $http.get(API_URL.GET_FOLLOWING);
        }

        function getRepositoriesCount() {
            return $http.get(API_URL.GET_REPOSITORIESCOUNT);
        }

        function getActivityMonths() {
            return $http.get(API_URL.GET_ACTIVITYMONTHS);
        }

        function getRepositories() {
            return $http.get(API_URL.GET_REPOSITORIES);
        }

        function getRepositoriesNames() {
            return $http.get(API_URL.GET_REPOSITORIES).then(successCbMap);
        }

        function getCommitsRepositories() {
            return $http.get(API_URL.GET_COMMITSREPOSITORIES);
        }

        function getGroupCommits() {
            return $http.get(API_URL.GET_GROUPCOMMITS).then(successCb);
        }

        function getCommitsFromCurrentRepo(repo) {
            return $http.get(API_URL.GET_REPOBYNAME + '/' + repo.name).then(successCb);
        }

        function successCb(response) {
            return [response.data];
        }

        function successCbMap(response) {
            return response.data.map(function(el) {
                return el.name;
            });
        }

        return {
            getFollowers: getFollowers,
            getFollowing: getFollowing,
            getRepositoriesCount: getRepositoriesCount,
            getActivityMonths: getActivityMonths,
            getRepositories: getRepositories,
            getRepositoriesNames: getRepositoriesNames,
            getCommitsRepositories: getCommitsRepositories,
            getGroupCommits: getGroupCommits,
            getCommitsFromCurrentRepo: getCommitsFromCurrentRepo
        };
    }
})();
