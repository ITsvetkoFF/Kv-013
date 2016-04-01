(function () {
    'use strict';

    angular
        .module('app.repos')
        .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'repos',
                config: {
                    url: '/repos',
                    templateUrl: 'app/repos/repos.html',
                    controller: 'ReposController',
                    controllerAs: 'vmRepos',
                    title: 'Repo list',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-dashboard"></i> Repos'
                    }
                }
            }
        ];
    }
})();
