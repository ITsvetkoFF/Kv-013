(function () {
    'use strict';

    angular
        .module('app.stats')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n','userService'];
    /* @ngInject */
    function appRun(routerHelper, i18n, userService) {
        routerHelper.configureStates(getStates(i18n, userService));
    }

    function getStates(i18n, userService) {
        return [
            {
                state: 'stats',
                config: {
                    url: '/stats',
                    templateUrl: 'FrontEnd/src/client/app/stats/stats.html',
                    controller: 'StatsController',
                    controllerAs: 'vmStats',
                    title: 'Stats',
                    settings: {
                        nav: 4,
                        content: '<i class="fa fa-area-chart"></i>' + i18n.message.STATISTICS,
                        show : userService.isAuthenticated()
                    }
                }
            }
        ];
    }
})();
