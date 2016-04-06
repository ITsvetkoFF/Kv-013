(function () {
    'use strict';
    angular.module('app.templates')
    .run(appRun);

    appRun.$inject = ['routerHelper','$cookies'];
    /* @ngInject */
    function appRun(routerHelper, $cookies) {
        var isAuth = $cookies.get('isAuth');
        if (isAuth) {
            routerHelper.configureStates(getStates());
        }
    }

    function getStates() {
        return [
            {
                state: 'templates',
                config: {
                    url: '/templates',
                    templateUrl: './FrontEnd/src/client/app/templates/templates.html',
                    controller: 'TemplatesController',
                    controllerAs: 'vmTemplates',
                    title: 'Templates',
                    settings: {
                        nav: 5,
                        content: '<i class="fa fa-dashboard"></i> Templates'
                    }
                }
            }
        ];
    }
})();

