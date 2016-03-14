(function() {
    'use strict';
    var module = angular.module('app.roles');

    module.factory('githubCollaborators', githubCollaborators);

    githubCollaborators.$inject = ['$http'];

    /* @ngInject */
    function githubCollaborators($http) {
    
        function getCollaborators(repo) {
            return $http({
                method: 'GET',
                url: 'http://localhost:51382/api/repos/'+repo.name+'/collaborators'
            }).then(function (response) { return response.data; });
        }

        function getRepos() {
            return $http({
                method: 'GET',
                url: 'http://localhost:51382/api/user/repos'
            });
        }

        function getRoles() {
            return $http({
                method: 'GET',
                url: 'http://localhost:51382/api/roles'
            });
        }

        function assignRole(repository, collaborator, role) {
            console.log(arguments);
            return $http({
                method: 'PATCH',
                dataType: 'string',
                url: 'http://localhost:51382/api/repos/' + repository.gitHubId + '/collaborators/' + collaborator.id,
                data: role.name
            });
        }

        return {
            getCollaborators: getCollaborators,
            getRepos: getRepos,
            getRoles: getRoles,
            assignRole: assignRole
        }

    }
})();
