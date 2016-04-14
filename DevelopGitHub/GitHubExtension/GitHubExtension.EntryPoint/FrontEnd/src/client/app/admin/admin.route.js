(function() {
    'use strict';

    angular
        .module('app.admin')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n'];
    /* @ngInject */
    function appRun(routerHelper, i18N) {
        routerHelper.configureStates(getStates(i18N));
    }

    function getStates(i18N) {
        return [
            {
                state: 'admin',
                config: {
                    url: '/admin',
                    templateUrl: 'FrontEnd/src/client/app/admin/admin.html',
                    controller: 'AdminController',
                    controllerAs: 'vm',
                    title: 'Admin',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i>' + i18N.message.ADMIN
                    }
                }
            }
        ];
    }
})();
