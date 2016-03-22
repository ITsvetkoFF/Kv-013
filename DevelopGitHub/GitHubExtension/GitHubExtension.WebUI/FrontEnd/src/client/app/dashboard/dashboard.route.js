(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n'];
    /* @ngInject */
    function appRun(routerHelper, i18n) {
        routerHelper.configureStates(getStates(i18n));
    }

    function getStates(i18n) {
        return [
            {
                state: 'dashboard',
                config: {
                    url: '/',
                    templateUrl: 'app/dashboard/dashboard.html',
                    controller: 'DashboardController',
                    controllerAs: 'vm',
                    title: 'dashboard',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i>' + i18n.message.DASHBOARD
                    }
                }
            }
        ];
    }
})();
