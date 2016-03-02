(function() {
    'use strict';

    angular
        .module('app.layout')
        .directive('htTopNav', htTopNav);

    htTopNav.$in
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
            templateUrl: 'app/layout/ht-top-nav.html'
        };

        TopNavController.$inject = ['$state', 'routerHelper'];
        /* @ngInject */
        function TopNavController($state, routerHelper) {
            var vm = this;
            var states = routerHelper.getStates();

            activate();

            function activate() {
                getNavRoutes();
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
