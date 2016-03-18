﻿(function () {
    'use.strict';
    angular.module('app.login')
    .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'login',
                config: {
                    url: '/login',
                    templateUrl: 'app/login/login.html',
                    controller: 'loginController',
                    controllerAs: 'vmLogin',
                    title: 'Login'
                }
            },
            {
                state: 'associate',
                config: {
                    url: '/associate',
                    templateUrl: 'app/login/associate.html',
                    controller: 'associateController',
                    controllerAs: 'vmAssociate',
                    title: 'Repo list'
                }
            },
            {
                state: 'authComplete',
                config: {
                    url: '/authComplete',
                    templateUrl: 'app/login/authComplete.html'
                }
            }
        ];
    }
})();


