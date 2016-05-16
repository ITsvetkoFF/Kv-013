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
            }).then(function (response) {
                var collaboratorsExtended = response.data;
                collaboratorsExtended.forEach(function (collaborator) {
                    getPrivateNote(collaborator).then(function (data) {
                        collaborator.noteId = data.data.id;
                        collaborator.noteBody = data.data.body;
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

        function assignRole(repository, collaborator, role) {
            return $http({
                method: 'PATCH',
                dataType: 'string',
                url: baseUrl + 'repos/' + repository.id + '/collaborators/' + collaborator.gitHubId,
                data: '"' + role.name + '"'
            });
        }

        function createPrivateNote(collaborator) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: API_URL.NOTE,
                data: {
                    collaboratorId: collaborator.userId,
                    body: collaborator.noteBody
                }
            }).then (function (response) {
                collaborator.noteId = response.data.id;
                collaborator.noteBody = response.data.body;
            });
        }

        function getPrivateNote(collaborator) {
            return $http({
                method: 'GET',
                url: API_URL.NOTE + API_URL.COLLABORATORS + collaborator.userId
            });
        }

        function deletePrivateNote(collaborator) {
            return $http({
                method: 'DELETE',
                url: API_URL.DELETE_NOTE + collaborator.noteId
            });
        }

        return {
            getCollaborators: getCollaborators,
            getRoles: getRoles,
            assignRole: assignRole,
            getPrivateNote: getPrivateNote,
            createPrivateNote: createPrivateNote,
            deletePrivateNote: deletePrivateNote
        };
    }
})();
