(function () {
    'use strict';

    angular
        .module('app.stats')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'stats',
                config: {
                    url: '/stats',
                    templateUrl: 'FrontEnd/src/client/app/stats/stats.html',
                    controller: 'statsController',
                    contollerAs: 'vmStats',
                    title: 'Stats',
                    settings: {
                        nav: 4,
                        content: '<i class="fa fa-lock">Stats</i>'
                    }
                }
            }
        ];
    }
})();
