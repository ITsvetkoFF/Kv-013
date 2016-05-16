(function () {

    'use strict';

    angular
     .module('app.roles')
     .controller('RolesController', RolesController);

    RolesController.$inject = ['githubCollaborators', 'logger', 'i18n', 'userService'];

    /* @ngInject */
    function RolesController(githubCollaborators, logger, i18n, userService) {
        var vm = this;
        vm.title = 'Roles';
        vm.i18n = i18n.message;
        vm.repo = {};
        activate();

        function activate() {
            logger.info(vm.i18n.ACTIVATED_ROLE_VIEW);
            vm.repo = userService.getCurrentRepository();
            if (!!vm.repo) {
                githubCollaborators.getRoles().then(onGetRoles, onError);
                githubCollaborators.getCollaborators(vm.repo).then(onGetCollaborators);
            }
        }

        function onGetCollaborators (data) {
            logger.info(vm.i18n.COLLABORATORS_LOADED);
            vm.collaborators = data;
        }

        function onGetRoles(data) {
            logger.info(vm.i18n.ROLES_SUCCEDED);
            vm.roles = data;
        }

        vm.assignRole = function (collaborator, role) {
            githubCollaborators.assignRole(vm.repo, collaborator, role);
        };

        vm.getPrivateNote = function(collaborator) {
            githubCollaborators.getPrivateNote(collaborator);
        };

        vm.createPrivateNote = function(collaborator) {
            var noteId = collaborator.noteId;
            if (typeof noteId === 'undefined') {
                githubCollaborators.createPrivateNote(collaborator).then(onNoteCreated, onNoteError);
            }
            else {
                githubCollaborators.deletePrivateNote(collaborator).then(function () {
                    githubCollaborators.createPrivateNote(collaborator).then(onNoteCreated, onNoteError);
                });
            }
        };

        vm.deletePrivateNote = function (collaborator) {
            githubCollaborators.deletePrivateNote(collaborator).then(function () {
                delete collaborator.noteId;
                delete collaborator.noteBody;
            });
        };

        vm.formError = function (reason) {
            logger.error(reason);
        };

        function onNoteCreated() {
            logger.info(vm.i18n.NOTE_CREATED);
        }

        function onNoteError() {
            logger.error(vm.i18n.NOTE_FAILED);
        }

        function onError(reason) {
            vm.error = vm.i18n.COULD_NOT_GET_COLLABORATORS;
        }
    }
}());
