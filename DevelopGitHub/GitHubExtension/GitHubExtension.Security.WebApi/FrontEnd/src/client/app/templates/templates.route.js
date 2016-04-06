(function () {
    'use strict';
    angular.module('app.templates')
    .run(appRun);

    appRun.$inject = ['routerHelper', '$cookies', 'i18n'];
    /* @ngInject */
    function appRun(routerHelper, $cookies,i18n) {
        var isAuth = $cookies.get('isAuth');
        if (isAuth) {
            routerHelper.configureStates(getStates(i18n));
        }
    }

    function getStates(i18n) {
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
                        content: '<i class="fa fa-dashboard"></i>' + i18n.message.TEMPLATES
                    }
                }
            }
        ];
    }
})();

