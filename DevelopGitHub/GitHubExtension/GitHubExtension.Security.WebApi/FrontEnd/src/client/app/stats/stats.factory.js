(function () {
    'use strict';
    var module = angular.module('app.stats');

    module.factory('statsFactory', statsFactory);

    statsFactory.$inject = ['$http', 'baseStatsUrl'];

    function statsFactory($http, baseUrl) {
        var baseUrl = baseUrl.apiStatsUrl;

        function getCommitsFromRepos() {
            return $http({
                method: 'GET',
                url: baseUrl + '/commits'
            });
        }
        function getRepos() {
            return $http({
                method: 'GET',
                url: baseUrl + '/repos'
            });
        }

        function getCommitsFromCurrentRepo(repo) {
            return $http({
                method: 'GET',
                dataType: 'string',
                url: baseUrl + '/commits/' + repo.name
            });
        }

        return {
            getCommitsFromRepos: getCommitsFromRepos,
            getRepos: getRepos,
            getCommitsFromCurrentRepo: getCommitsFromCurrentRepo
        };
    }
})();
