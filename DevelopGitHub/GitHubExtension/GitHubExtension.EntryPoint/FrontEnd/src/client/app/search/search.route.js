(function () {
    'use strict';

    angular
        .module('app.search')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n', 'userService'];
    /* @ngInject */
    function appRun(routerHelper, i18n, userService) {
        routerHelper.configureStates(getStates(i18n, userService));
    }

    function getStates(i18n, userService) {
        return [
            {
                state: 'search',
                config: {
                    url: '/search',
                    templateUrl: 'FrontEnd/src/client/app/search/search.html',
                    controller: 'SearchController',
                    controllerAs: 'vmSearch',
                    title: 'Search',
                    settings: {
                        nav: 7,
                        content: '<i class="fa fa-search"></i>' + i18n.message.SEARCH,
                        show: userService.isAuthenticated()
                    }
                }
            }
        ];
    }
})();
