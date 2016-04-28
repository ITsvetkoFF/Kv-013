﻿(function() {
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
            }).then(function (response) {
                var collaboratorsExtended = response.data;
                collaboratorsExtended.forEach(function (collaborator) {
                    getPrivateNote(collaborator).then(function (data) {
                        collaborator.note = data.data.body;
                    });
                });
                return collaboratorsExtended;
            }) ;
        }

        function getRoles() {
            return $http({
                method: 'GET',
                url: API_URL.ROLES
            });
        }

        function addActivityRole(roleToAssign, collaboratorName) {
            return function() {
                return $http({
                    method: 'POST',
                    url: API_URL.internalActivityUrl + '/addRole',
                    data: {roleToAssign: roleToAssign, collaboratorName: collaboratorName}
                });
            };
        }

        function assignRole(repository, collaborator, role) {
            return $http({
                method: 'PATCH',
                dataType: 'string',
                url: baseUrl + '/repos/' + repository.id + '/collaborators/' + collaborator.id,
                data: '"' + role.name + '"'
            }).then(addActivityRole(role.name, collaborator.login));
        }

        function createPrivateNote(collaborator) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: API_URL.NOTE,
                data: {
                    collaboratorGitHubId: collaborator.id,
                    body: collaborator.note
                }
            });
        }

        function getPrivateNote(collaborator) {
            return $http({
                method: 'GET',
                url: API_URL.NOTE + API_URL.COLLABORATORS + collaborator.id
            });
        }

        return {
            getCollaborators: getCollaborators,
            getRoles: getRoles,
            assignRole: assignRole
        };
    }
})();
