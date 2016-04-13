(function () {

    'use strict';

    angular
     .module('app.roles')
     .controller('RolesController', RolesController);

    RolesController.$inject = ['githubCollaborators', 'logger', '$q'];

    /* @ngInject */
    function RolesController(githubCollaborators, logger, $q) {
        var vm = this;
        vm.title = 'Roles';
        vm.repo = '';
        vm.roleName = [];
        activate();

        function activate() {
            logger.info('Activated Roles View');
            githubCollaborators.getRepos().then(onGetRepos, onError);
            githubCollaborators.getRoles().then(onGetRoles, onError);
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

        vm.setCurrentProjectAndGetCollaborators = function(repo) {
            $q.all(githubCollaborators.updateCurrentProject(repo), getColaborators()).then(onError());
            function getColaborators() {
                return githubCollaborators.getCollaborators(repo).then(onGetCollaborators);
            }
        };

        function onError(reason) {
            vm.error = 'Could not get collaborators.';
        }
    }
}());
