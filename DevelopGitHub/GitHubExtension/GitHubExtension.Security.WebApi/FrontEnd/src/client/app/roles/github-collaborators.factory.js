(function() {
    'use strict';
    var module = angular.module('app.roles');

    module.factory('githubCollaborators', githubCollaborators);

    githubCollaborators.$inject = ['$http'];

    /* @ngInject */
    function githubCollaborators($http) {
        var baseUrl = 'http://localhost:50859/api';
        function getCollaborators(repo) {
            return $http({
                method: 'GET',
                url: baseUrl + '/repos/' + repo.name + '/collaborators'
            }).then(function (response) { return response.data; });
        }

        function getRepos() {
            return $http({
                method: 'GET',
                url: baseUrl + '/user/repos'
            });
        }

        function getRoles() {
            return $http({
                method: 'GET',
                url: baseUrl + '/roles'
            });
        }

        function assignRole(repository, collaborator, role) {
            return $http({
                method: 'PATCH',
                dataType: 'string',
                url: baseUrl + '/repos/' + repository.id + '/collaborators/' + collaborator.id,
                data: '"' + role.name + '"'
            });
        }

        return {
            getCollaborators: getCollaborators,
            getRepos: getRepos,
            getRoles: getRoles,
            assignRole: assignRole
        };
    }
})();
