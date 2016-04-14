(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('DashboardController', dashboardController);

    dashboardController.$inject = ['$q', 'logger', 'i18n'];
    /* @ngInject */
    function dashboardController($q, logger, i18N) {
        var vm = this;

        // add localization
        vm.i18n = i18N;
        vm.news = {
            title: 'GitHubExtension',
            description: 'Hot Towel Angular is a SPA template for Angular developers.'
        };
        vm.messageCount = 0;
        vm.title = 'Dashboard';

        activate();

        function activate() {
            return logger.info('Activated Dashboard View');
        }
    }
})();
