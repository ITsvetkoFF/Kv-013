/**
 * Created by Vladyslav on 26.02.2016.
 */

(function() {
        'use strict';
        angular
            .module('app.authorization')
            .run(appRun);

        appRun.$inject = ['routerHelper', 'i18n'];
        /* @ngInject */
        function appRun(routerHelper, i18n) {
            routerHelper.configureStates(getStates(i18n));
        }

        function getStates(i18n) {
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
                            content: i18n.message.AUTHORIZATION
                        }
                    }
                }
            ];
        }
    }
)();
