(function () {
    'use strict';

    angular
        .module('app.layout')
        .directive('htTopNav', htTopNav);

    /* @ngInject */
    function htTopNav() {
        var directive = {
            bindToController: true,
            controller: TopNavController,
            controllerAs: 'vm',
            restrict: 'EA',
            scope: {
                'navline': '='
            },
            templateUrl: 'app/layout/ht-top-nav.html'
        };

        TopNavController.$inject = ['i18n', '$state', 'routerHelper'];
        /* @ngInject */
        function TopNavController(i18n, $state, routerHelper) {
            var vm = this;

            // add i18n for localization
            vm.i18n = i18n;

            var states = routerHelper.getStates();

            activate();

            function activate() {
                getNavRoutes();
            }

            function getNavRoutes() {
                vm.navRoutes = states.filter(function (r) {
                    return r.settings && r.settings.topNav;
                }).sort(function (r1, r2) {
                    return r1.settings.topNav - r2.settings.topNav;
                });
            }
        }
        return directive;
    }
})();
