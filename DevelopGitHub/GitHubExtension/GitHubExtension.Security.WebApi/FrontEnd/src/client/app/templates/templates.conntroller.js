(function () {

    'use strict';

    angular
     .module('app.templates')
     .controller('TemplatesController', TemplatesController);

    TemplatesController.$inject = ['githubTemplates', 'logger'];

    /* @ngInject */
    function TemplatesController(githubTemplates,logger) {

        var vm = this;
        vm.title = 'Templates';
        activate();
        vm.prVar = false;
        vm.iVar = false;
        vm.prtoggle = function() {
            vm.prVar = !vm.prVar;
        };

        vm.itoggle = function () {
            vm.iVar = !vm.iVar;
        };

        function activate() {
            logger.info('Activated Templates View');
            githubTemplates.getPullRequestTemplate().then(onGetPullRequestTemplate,onError);
            githubTemplates.getIssueTemplate().then(onGetIssueTemplate,onError);
        }

        function onError(reason) {
            vm.error = 'Could not fetch the data.';
        }

        function onGetPullRequestTemplate(data) {
            vm.pullRequestTemplate = data;
        }

        function onGetIssueTemplate(data) {
            vm.issueTemplate = data;
        }

    }

}());
