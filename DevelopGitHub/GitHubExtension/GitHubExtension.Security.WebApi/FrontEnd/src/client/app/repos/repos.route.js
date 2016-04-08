(function () {
    'use strict';

    angular
        .module('app.repos')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n'];
    /* @ngInject */
    function appRun(routerHelper, i18n) {
        routerHelper.configureStates(getStates(i18n));
    }

    function getStates(i18n) {
        return [
            {
                state: 'repos',
                config: {
                    url: '/repos',
                    templateUrl: 'FrontEnd/src/client/app/repos/repos.html',
                    controller: 'ReposController',
                    controllerAs: 'vmRepos',
                    title: 'Repo list',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-dashboard"></i>' + i18n.message.REPOSITORY
                    }
                }
            }
        ];
    }
})();
