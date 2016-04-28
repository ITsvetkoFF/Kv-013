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

        function onError(reason) {
            vm.error = vm.i18n.COULD_NOT_GET_COLLABORATORS;
        }
    }
}());
