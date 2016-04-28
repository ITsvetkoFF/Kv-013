(function() {
    'use strict';

    angular
        .module('app.layout')
        .controller('ShellController', ShellController);

    ShellController.$inject = ['$rootScope', '$timeout', 'config', 'logger', 'i18n'];
    /* @ngInject */

    function ShellController($rootScope, $timeout, config, logger, i18n) {
        var vm = this;

        // add i18n for localization
        vm.i18n = i18n.message;
        vm.busyMessage = vm.i18n.PLEASE_WAIT;
        vm.isBusy = true;
        $rootScope.showSplash = true;
        vm.navline = {
            title: config.appTitle
        };

        activate();

        function activate() {
            logger.success(config.appTitle + vm.i18n.LOADED, null);
            hideSplash();
        }

        function hideSplash() {
            //Force a 1 second delay so we can see the splash.
            $timeout(function() {
                $rootScope.showSplash = false;
            }, 1000);
        }
    }
})();
