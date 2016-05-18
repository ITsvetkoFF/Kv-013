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

        function getRepositoriesCount(repositoryList) {
            return repositoryList.length;
        }

        function getActivityMonths() {
            return $http.get(API_URL.GET_ACTIVITYMONTHS);
        }

        function getRepositoriesNames(repositoryList) {
            return mapToNames(repositoryList);
        }

        function getCommitsRepositories() {
            return $http.get(API_URL.GET_COMMITSREPOSITORIES);
        }

        function getGroupCommits() {
            return $http.get(API_URL.GET_GROUPCOMMITS).then(wrappToArray);
        }

        function getCommitsFromCurrentRepo(repo) {
            return $http.get(API_URL.GET_REPOBYNAME + '/' + repo.name).then(wrappToArray);
        }

        function wrappToArray(response) {
            return [response.data];
        }

        function mapToNames(repositoryList) {
            return repositoryList.map(function(el) {
                return el.name;
            });
        }

        return {
            getFollowers: getFollowers,
            getFollowing: getFollowing,
            getRepositoriesCount: getRepositoriesCount,
            getActivityMonths: getActivityMonths,
            getRepositoriesNames: getRepositoriesNames,
            getCommitsRepositories: getCommitsRepositories,
            getGroupCommits: getGroupCommits,
            getCommitsFromCurrentRepo: getCommitsFromCurrentRepo
        };
    }
})();
