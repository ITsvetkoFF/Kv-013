(function () {

    'use strict';

    angular
     .module('app.templates')
     .controller('PrController', PrController);

    PrController.$inject = ['githubTemplates', 'logger', 'i18n', '$state'];

    /* @ngInject */
    function PrController(githubTemplates, logger, i18n, $state) {
        var vm = this;
        vm.i18n = i18n;
        vm.prVar = false;
        vm.showEdit = false;
        activate();

        vm.prtoggle = function () {
            vm.prVar = !vm.prVar;
            if (vm.showEdit) {
                vm.showEdit = !vm.showEdit;
            }
        };

        vm.predit = function () {
            vm.showEdit = !vm.showEdit;
            if (vm.prVar) {
                vm.prVar = !vm.prVar;
            }
        };

        vm.showPrTemplate = function (index) {
            vm.prArray = [false, false];
            vm.prIndex = index;
            vm.prArray[index] = true;
        }

        vm.createPr = function (template) {
            githubTemplates.createPrTemplate(template);

            $state.go('templates.pr').then(function () {
                $state.reload();
            });
            githubTemplates.getPullRequestTemplate().success(onGetPullRequestTemplate, onError).error(onGetPullRequestTemplate);
        }

        vm.editPr = function (template) {
            githubTemplates.updatePrTemplate(template);
            $state.go('templates.pr').then(function () {
                $state.reload();
            });
            githubTemplates.getPullRequestTemplate().success(onGetPullRequestTemplate, onError).error(onGetPullRequestTemplate);
        }

        function activate() {
            logger.info(vm.i18n.message.ACTIVE_PR);
                githubTemplates.getPullRequestTemplate().success(onGetPullRequestTemplate, onError).error(onGetPullRequestTemplate);
                githubTemplates.getPr().success(function (data) {
                    vm.pr = data;
                });
        }

        function onError(reason) {
            vm.error = 'Could not fetch the data.';
        }

        function onGetPullRequestTemplate(data) {
            vm.pullRequestTemplate = data;
        }

    }

}());
