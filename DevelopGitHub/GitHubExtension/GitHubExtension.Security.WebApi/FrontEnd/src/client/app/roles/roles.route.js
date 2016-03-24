(function () {
    'use strict';

    angular
        .module('app.roles')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'roles',
                config: {
                    url: '/roles',
                    templateUrl: 'app/roles/roles.html',
                    controller: 'RolesController',
                    controllerAs: 'vmRoles',
                    title: 'Collaborators list',
                    settings: {
                        nav: 3,
                        content: '<i class="fa fa-dashboard"></i> Collaborators'
                    }
                }
            }
        ];
    }
})();
