(function () {
    'use strict';

    angular
        .module('app.admin')
        .controller('AdminController', AdminController);

    AdminController.$inject = ['logger', 'i18n'];
    /* @ngInject */
    function AdminController(logger, i18n) {
        var vm = this;

        // add localization
        vm.i18n = i18n;

        vm.title = 'Admin';

        activate();

        function activate() {
            logger.info('Activated Admin View');
        }
    }
})();
