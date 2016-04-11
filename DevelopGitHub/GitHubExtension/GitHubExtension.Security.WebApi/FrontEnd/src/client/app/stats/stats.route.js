(function () {
    'use strict';

    angular
        .module('app.stats')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n'];
    /* @ngInject */
    function appRun(routerHelper, i18n) {
        routerHelper.configureStates(getStates(i18n));
    }

    function getStates(i18n) {
        return [
            {
                state: 'stats',
                config: {
                    url: '/stats',
                    templateUrl: 'FrontEnd/src/client/app/stats/stats.html',
                    controller: 'StatsController',
                    contollerAs: 'vm',
                    title: 'Stats',
                    settings: {
                        nav: 4,
                        content: '<i class="fa fa-lock">' + i18n.message.STATISTICS + '</i>'
                    }
                }
            }
        ];
    }
})();
