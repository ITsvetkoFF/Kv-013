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
            logger.info('Activated Roles View');
            vm.repo = userService.getCurrentRepository();
            if (!!vm.repo) {
                githubCollaborators.getRoles().then(onGetRoles, onError);
                githubCollaborators.getCollaborators(vm.repo).then(onGetCollaborators);
            }
        }

        function onGetCollaborators (data) {
            logger.info('Collaborators Loaded');
            vm.collaborators = data;
        }

        function onGetRoles(data) {
            logger.info('Roles Succeded');
            vm.roles = data;
        }

        vm.assignRole = function (collaborator, role) {
            githubCollaborators.assignRole(vm.repo, collaborator, role);
        };

        function onError(reason) {
            vm.error = 'Could not get collaborators.';
        }
    }
}());
