(function () {
    'use strict';
    angular.module('app.templates')
    .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'templates',
                config: {
                    url: '/templates',
                    templateUrl: 'app/templates/templates.html',
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

