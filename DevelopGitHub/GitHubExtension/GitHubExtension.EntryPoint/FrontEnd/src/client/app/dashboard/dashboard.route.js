(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n'];
    /* @ngInject */
    function appRun(routerHelper, i18N) {
        routerHelper.configureStates(getStates(i18N));
    }

    function getStates(i18N) {
        return [
            {
                state: 'dashboard',
                config: {
                    url: '/',
                    templateUrl: 'FrontEnd/src/client/app/dashboard/dashboard.html',
                    controller: 'DashboardController',
                    controllerAs: 'vm',
                    title: 'dashboard',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i>' + i18N.message.DASHBOARD
                    }
                }
            }
        ];
    }
})();
