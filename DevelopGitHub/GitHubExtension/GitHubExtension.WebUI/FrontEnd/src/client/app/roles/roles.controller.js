(function () {

    'use strict';

    angular
     .module('app.roles')
     .controller('RolesController', RolesController);

//    RolesController.$inject = ['githubCollaborators', 'Authorization', 'logger'];

    RolesController.$inject = ['githubCollaborators', 'logger'];

    /* @ngInject */
    function RolesController(githubCollaborators, logger) {
        var vm = this;
        vm.title = 'Roles';
        vm.repo = '';
        vm.roleName = [];
        vm.teammembers = searchCollaborators;
        activate();

        function activate() {
            logger.info('Activated Roles View');
            githubCollaborators.getRepos().then(onGetRepos, onError);
            githubCollaborators.getRoles().then(onGetRoles, onError)
        }

//        function authorizationFormHandler() {
//            authorizationService.signIn(vm.user).then(successFn, errorFn);
//        }

//        function successFn(response) {
//            logger.success('You are successfully signed in as ' + response.data.login);
//            vm.signedIn = true;
//            githubCollaborators.getRepos().then(onGetRepos, onError);
//                        authorizationService.getRepos().then(onGetRepos, onError);
//        }

        function searchCollaborators() {
            githubCollaborators.getCollaborators(vm.repo).then(onGetCollaborators, onError);
        };

//        function searchRepos() {
//            githubCollaborators.getRepos().then(onGetRepos, onError);
//        };

        var onGetCollaborators = function (data) {
            logger.info('Collaborators Loaded');
            vm.collaborators = data;
        } 

        function onGetRepos(response) {
            logger.info('Repos Succeded');
            vm.repositories = response.data;
        };

        function onGetRoles(data) {
            logger.info('Roles Succeded');
            vm.roles = data;
        };
        /**
         * 
         * @param {} colaborator
         * @param {object} role  
         */
        vm.assignRole = function (colaborator, role) {
            console.log(vm.repo);
            console.log(arguments);
            githubCollaborators.assignRole(vm.repo, colaborator, role);
        }

//        function errorFn(response) {
//            logger.error('Sing in operation failed, check your username and password and try again');
//        }

        function onError(reason) {
            vm.error = 'Could not get collaborators.';
        }
    }
}());
