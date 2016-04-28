(function () {

    'use strict';

    angular
     .module('app.templates')
     .controller('IssueController', IssueController);

    IssueController.$inject = ['githubTemplates', 'logger', 'i18n', '$state'];

    /* @ngInject */
    function IssueController(githubTemplates, logger, i18n, $state) {
        var vm = this;
        vm.i18n = i18n;
        vm.iVar = false;
        vm.showPr1 = false;
        vm.showPr2 = false;
        vm.showEditIssue = false;
        activate();

        vm.itoggle = function () {
            vm.iVar = !vm.iVar;
            if (vm.showEditIssue) {
                vm.showEditIssue = !vm.showEditIssue;
            }
        };

        vm.iedit = function () {
            vm.showEditIssue = !vm.showEditIssue;
            if (vm.iVar) {
                vm.iVar = !vm.iVar;
            }
        };

        vm.addtogglePR1 = function () {
            if (vm.showPr2) {
                vm.showPr2 = !vm.showPr2;
            }
            vm.showPr1 = !vm.showPr1;
        };

        vm.addtogglePR2 = function () {
            vm.showPr2 = !vm.showPr2;
            if (vm.showPr1) {
                vm.showPr1 = !vm.showPr1;
            }
        };

        vm.createIssue = function (template) {
            githubTemplates.createIssueTemplate(template);
            $state.go('templates.issue').then(function () {
                $state.reload();
            });
            githubTemplates.getIssueTemplate().success(onGetIssueTemplate, onError).error(onGetIssueTemplate);
        }

        vm.editIssue = function (template) {
            githubTemplates.updateIssueTemplate(template);
            $state.go('templates.issue').then(function () {
                $state.reload();
            });
            githubTemplates.getIssueTemplate().success(onGetIssueTemplate, onError).error(onGetIssueTemplate);
        }

        vm.showIssueByCategory = function (index, category) {
            vm.issueArray = [false, false, false];
            vm.showPr1 = true;
            vm.category = category;

            githubTemplates.getIssueTemplateByCategoryId(index).success(onGetIssueTemplateByCategoryId, onError).error(onGetIssueTemplateByCategoryId);
        }

        vm.showIssueTemplate = function (index) {
            vm.issueArray = [false, false, false];
            vm.index = index;
            vm.issueArray[index] = true;
        }

        function activate() {
            logger.info(vm.i18n.message.ACTIVE_ISSUE);
            githubTemplates.getIssueTemplate().success(onGetIssueTemplate, onError).error(onGetIssueTemplate);
            githubTemplates.getIssueCategories().success(onGetIssueCategories, onError).error(onGetIssueCategories);
        }
        function onError(reason) {
            vm.error = 'Could not fetch the data.';
        }

        function onGetIssueTemplate(data) {
            vm.issueTemplate = data;
        }
        function onGetIssueCategories(data) {
            vm.issueCategories = data;
        }

        function onGetIssueTemplateByCategoryId(data) {
            vm.issueTemplateByCategoryId = data;
        }

    }

}());
