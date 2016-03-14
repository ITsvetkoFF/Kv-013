(function () {

    'use strict';

    angular
     .module('app.roles')
     .controller('RolesController', RolesController);

    RolesController.$inject = ['githubCollaborators', 'Authorization', 'logger'];

    /* @ngInject */
    function RolesController(githubCollaborators, authorizationService, logger) {

        var vm = this;
        vm.title = 'Roles';


        vm.user = {};
        vm.reponame;
        vm.submit = authorizationFormHandler;
        vm.teammembers = searchCollaborators;
        activate();

        function activate() {
            logger.info('Activated Roles View');
        }

        function authorizationFormHandler() {
            authorizationService.signIn(vm.user).then(successFn, errorFn);
        }

        function successFn(response) {
            logger.success('You are successfully signed in as ' + response.data.login);
            vm.signedIn = true;
            authorizationService.getRepos(vm.user).then(onGetRepos, onError);
        }

        function searchCollaborators() {
            githubCollaborators.getCollaborators(vm.user, vm.reponame).then(onGetCollaborators, onError);
        };
        var onGetCollaborators = function (data) {
            logger.info('Collaborators Loaded');
            vm.collaborators = data;
        } 

        var onGetRepos = function (data) {
            logger.info('Repos Succeded');
            vm.repositories = data;
        };

        function errorFn(response) {
            logger.error('Sing in operation failed, check your username and password and try again');
        }

        function onError(reason) {
            vm.error = 'Could not get collaborators.';
        }
    }
}());
