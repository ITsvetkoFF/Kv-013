(function() {
    'use strict';

    angular
        .module('app.layout')
        .directive('htTopNav', htTopNav);

    /* @ngInject */
    function htTopNav () {
        var directive = {
            bindToController: true,
            controller: TopNavController,
            controllerAs: 'vm',
            restrict: 'EA',
            scope: {
                'navline': '='
            },
            templateUrl: 'FrontEnd/src/client/app/layout/ht-top-nav.html'
        };

        TopNavController.$inject = ['userService', 'i18n', '$state', 'routerHelper'];
        /* @ngInject */
        function TopNavController(userService, i18n, $state, routerHelper) {
            var vm = this;
            vm.i18n = i18n.message;
            var states = routerHelper.getStates();
            vm.user = userService;

            activate();

            function activate() {
                getNavRoutes();
                if (vm.user.isAuthenticated()) {
                    vm.user.loadRepositories();
                }
            }

            function getNavRoutes() {
                vm.navRoutes = states.filter(function(r) {
                    return r.settings && r.settings.topNav;
                }).sort(function(r1, r2) {
                    return r1.settings.topNav - r2.settings.topNav;
                });
            }
        }

        return directive;
    }
})();
