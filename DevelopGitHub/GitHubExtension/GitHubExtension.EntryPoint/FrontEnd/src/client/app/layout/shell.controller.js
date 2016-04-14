(function() {
    'use strict';

    angular
        .module('app.layout')
        .controller('ShellController', shellController);

    shellController.$inject = ['$rootScope', '$timeout', 'config', 'logger', 'i18n'];
    /* @ngInject */

    function shellController($rootScope, $timeout, config, logger, i18N) {
        var vm = this;

        // add i18n for localization
        vm.i18n = i18N;
        vm.busyMessage = 'Please wait ...';
        vm.isBusy = true;
        $rootScope.showSplash = true;
        vm.navline = {
            title: config.appTitle
        };

        activate();

        function activate() {
            logger.success(config.appTitle + ' loaded!', null);
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
