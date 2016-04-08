(function () {

    'use strict';

    angular
     .module('app.roles')
     .controller('RolesController', RolesController);

    RolesController.$inject = ['githubCollaborators', 'logger', 'i18n'];

    /* @ngInject */
    function RolesController(githubCollaborators, logger, i18n) {
        var vm = this;
        vm.i18n = i18n;
        vm.title = 'Roles';
        vm.repo = '';
        vm.roleName = [];
        activate();

        function activate() {
            logger.info('Activated Roles View');
            githubCollaborators.getRepos().then(onGetRepos, onError);
            githubCollaborators.getRoles().then(onGetRoles, onError);
        }

        function searchCollaborators() {
            githubCollaborators.getCollaborators(vm.repo).then(onGetCollaborators, onError);
        }

        function onGetCollaborators (data) {
            logger.info('Collaborators Loaded');
            vm.collaborators = data;
        }

        function onGetRepos(response) {
            logger.info('Repos Succeded');
            vm.repositories = response.data;
        }

        function onGetRoles(data) {
            logger.info('Roles Succeded');
            vm.roles = data;
        }
        /**
         *
         * @param {object} collaborator
         * @param {object} role
         */
        vm.assignRole = function (collaborator, role) {
            githubCollaborators.assignRole(vm.repo, collaborator, role);
        };

        vm.updateCurrentProject = function (repo) {
            githubCollaborators.updateCurrentProject(repo);
        };
        //function errorFn(response) {
        //    logger.error('Sing in operation failed, check your username and password and try again');
        //}

        function onError(reason) {
            vm.error = 'Could not get collaborators.';
        }
    }
}());
