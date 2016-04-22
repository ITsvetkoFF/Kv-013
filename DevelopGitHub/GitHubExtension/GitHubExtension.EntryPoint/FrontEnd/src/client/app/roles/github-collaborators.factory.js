(function() {
    'use strict';
    var module = angular.module('app.roles');

    module.factory('githubCollaborators', githubCollaborators);

    githubCollaborators.$inject = ['$http', 'API_URL'];

    /* @ngInject */
    function githubCollaborators($http, API_URL) {
        var baseUrl = API_URL.BASE_URL;
        function getCollaborators(repo) {
            return $http({
                method: 'GET',
                url: baseUrl + '/repos/' + repo.name + '/collaborators'
            }).then(function (response) { return response.data; });
        }

        function getRoles() {
            return $http({
                method: 'GET',
                url: API_URL.ROLES
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
            getRoles: getRoles,
            assignRole: assignRole
        };
    }
})();
