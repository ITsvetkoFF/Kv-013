(function () {
    'use strict';

    angular
        .module('app.admin')
        .controller('AdminController', adminController);

    adminController.$inject = ['logger', 'i18n'];
    /* @ngInject */
    function adminController(logger, i18N) {
        var vm = this;

        // add localization
        vm.i18n = i18N;

        vm.title = 'Admin';

        activate();

        function activate() {
            logger.info('Activated Admin View');
        }
    }
})();
