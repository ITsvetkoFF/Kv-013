(function () {
    'use strict';
    var module = angular.module('app.stats');

    module.factory('statsFactory', statsFactory);

    statsFactory.$inject = ['$http', 'baseStatsUrl'];

    function statsFactory($http, baseUrl) {
        var statsUrl = baseUrl.apiStatsUrl;

        function getCommitsFromRepos() {
            return $http({
                method: 'GET',
                url: baseUrl.apiGetCommitsRepos
            });
        }
        function getRepos() {
            return $http({
                method: 'GET',
                url: baseUrl.apiGetRepos
            });
        }

        function getCommitsFromCurrentRepo(repo) {
            return $http({
                method: 'GET',
                dataType: 'string',
                url: baseUrl.apiGetCommitsRepos + '/' + repo.name
            });
        }

        return {
            getCommitsFromRepos: getCommitsFromRepos,
            getRepos: getRepos,
            getCommitsFromCurrentRepo: getCommitsFromCurrentRepo
        };
    }
})();
