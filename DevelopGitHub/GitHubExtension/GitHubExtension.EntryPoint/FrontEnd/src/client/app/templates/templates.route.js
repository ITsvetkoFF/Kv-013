(function () {
    'use strict';
    angular.module('app.templates')
    .run(appRun);

    appRun.$inject = ['routerHelper', '$cookies', 'i18n', 'userService'];

    function appRun(routerHelper, $cookies, i18n, userService) {
       routerHelper.configureStates(getStates(i18n, userService));
    }

    function getStates(i18n, userService) {
        return [
        {
            state: 'templates',
            config: {
                    url: '/templates',
                    templateUrl: './FrontEnd/src/client/app/templates/templates.html',
                    controller: 'TemplatesController',
                    controllerAs: 'vmTemplates',
                    title: 'Templates'
                }
            },
            {
                state: 'templates.pr',
                config: {
                    url: '/pr',
                    templateUrl: './FrontEnd/src/client/app/templates/PullRequestTemplate.html',
                    controller: 'PrController',
                    controllerAs: 'vmPrTemplates',
                    settings: {
                        nav: 4,
                        content: '<i class="fa fa-file-o"></i>' + i18n.message.TEMPLATES,
                        show: userService.isAuthenticated()
                    }
                }
            },
            {
                state: 'templates.issue',
                config: {
                    url: '/issue',
                    templateUrl: './FrontEnd/src/client/app/templates/IssueTemplate.html',
                    controller: 'IssueController',
                    controllerAs: 'vmIssueTemplates'
                }
            }
        ];
    }
})();

