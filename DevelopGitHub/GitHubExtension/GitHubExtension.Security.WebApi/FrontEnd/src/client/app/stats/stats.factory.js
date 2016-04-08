(function () {
    'use strict';
    var module = angular.module('app.stats');

    module.factory('statsFactory', statsFactory);

    statsFactory.$inject = ['$http'];

    function statsFactory($http) {
        var baseUrl = 'api/user';

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
            console.log(repo);
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
