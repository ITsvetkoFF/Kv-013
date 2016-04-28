(function () {
    'use strict';

    angular
        .module('app.roles')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n', 'userService'];
    /* @ngInject */
    function appRun(routerHelper, i18n, userService) {
        routerHelper.configureStates(getStates(i18n, userService));
    }

    function getStates(i18n, userService) {
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
                    content: '<i class="fa fa-dashboard"></i>' + i18n.message.COLLABORATORS,
                    show: userService.isAuthenticated()
    }
                }
            }
        ];
    }
})();
