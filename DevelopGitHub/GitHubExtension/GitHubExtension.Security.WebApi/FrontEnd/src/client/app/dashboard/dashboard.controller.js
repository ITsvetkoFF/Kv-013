(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$q', 'logger'];
    /* @ngInject */
    function DashboardController($q, logger) {
        var vm = this;
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
