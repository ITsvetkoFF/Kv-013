(function () {
    'use strict';

    angular
        .module('app.roles')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n'];
    /* @ngInject */
    function appRun(routerHelper, i18n) {
        routerHelper.configureStates(getStates(i18n));
    }

    function getStates(i18n) {
        return [
            {
                state: 'roles',
                config: {
                    url: '/roles',
                    templateUrl: 'FrontEnd/src/client/app/roles/roles.html',
                    controller: 'RolesController',
                    controllerAs: 'vmRoles',
                    title: 'Collaborators list',
                    settings: {
                        nav: 3,
                        content: '<i class="fa fa-dashboard"></i>' + i18n.message.COLLABORATORS
                    }
                }
            }
        ];
    }
})();
