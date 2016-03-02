/**
 * Created by Vladyslav on 26.02.2016.
 */

(function() {
        'use strict';
        angular
            .module('app.authorization')
            .run(appRun);

        appRun.$inject = ['routerHelper'];
        /* @ngInject */
        function appRun(routerHelper) {
            routerHelper.configureStates(getStates());
        }

        function getStates() {
            return [
                {
                    state: 'authorization',
                    config: {
                        url: '/authorization',
                        templateUrl: 'app/authorization/authorization.html',
                        controller: 'AuthorizationController',
                        controllerAs: 'vm',
                        title: 'Authorization',
                        settings: {
                            topNav: 1,
                            content: 'Authorization'
                        }
                    }
                }
            ];
        }
    }
)();
