(function () {

    'use strict';

    angular
     .module('app.repos')
     .controller('ReposController', ReposController);

    ReposController.$inject = ['githubData', 'logger'];

    /* @ngInject */
    function ReposController(githubData, logger) {

        var vm = this;
        vm.title = 'Repositories';

        activate();

        function  activate() {
            logger.info('Activated Repositories View');
        }

        var onUserComplete = function (data) {
            vm.user = data;
            githubData.getRepos(vm.user).then(onRepos, onError);
        };

        var onRepos = function (data) {

            vm.repos = data;

        };

        var onError = function (reason) {
            vm.error = "Could not fetch the data.";
        };

        vm.search = function (username) {
            githubData.getUser(username).then(onUserComplete, onError);
        };


        vm.username = "angular";
        vm.message = "GithubViewer";
        vm.repoSortOrder = "-stargazers_count";
    };


}());