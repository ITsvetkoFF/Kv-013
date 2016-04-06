(function () {

    'use strict';

    angular
     .module('app.repos')
     .controller('ReposController', ReposController);

    ReposController.$inject = ['githubData', 'logger', 'i18n'];

    /* @ngInject */
    function ReposController(githubData, logger, i18n) {

        var vm = this;
        vm.i18n = i18n;
        vm.title = 'Repositories';

        activate();

        function  activate() {
            logger.info('Activated Repositories View');
        }

        var onUserComplete = function (data) {
            vm.user = data;
            githubData.getRepos(vm.user).then(onRepos, onError);
        };

        function onRepos (data) {
            vm.repos = data;
        }

        function onError (reason) {
            vm.error = 'Could not fetch the data.';
        }

        vm.search = function (username) {
            githubData.getUser(username).then(onUserComplete, onError);
        };

        vm.username = 'angular';
        vm.message = 'GithubViewer';
        vm.repoSortOrder = '-stargazers_count';
    }

}());
