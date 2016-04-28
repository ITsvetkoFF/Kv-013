(function () {
    'use strict';

    angular
        .module('app.userpreferences')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n', 'userService'];

    function appRun(routerHelper, i18n, userService) {
        routerHelper.configureStates(getStates(i18n, userService));
    }

    function getStates(i18n, userService) {
        return [
            {
                state: 'userpreferences',
                config: {
                    url: '/userpreferences',
                    templateUrl: 'FrontEnd/src/client/app/userpreferences/userpreferences.html',
                    controller: 'UserPreferencesController',
                    controllerAs: 'vmPreferences',
                    title: 'Preferences',
                    settings: {
                        nav: 6,
                        content: '<i class="fa fa-dashboard"></i>' + i18n.message.PREFERENCES,
                        show: userService.isAuthenticated()
                    }
                }
            }
        ];
    }
})();
