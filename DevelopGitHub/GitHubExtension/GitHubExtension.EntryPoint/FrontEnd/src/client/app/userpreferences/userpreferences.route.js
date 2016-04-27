(function () {
    'use strict';

    angular
        .module('app.userpreferences')
        .run(appRun);

    appRun.$inject = ['routerHelper', 'i18n'];

    function appRun(routerHelper, i18n) {
        routerHelper.configureStates(getStates(i18n));
    }

    function getStates(i18n) {
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
                        content: '<i class="fa fa-tachometer"></i>' + i18n.message.PREFERENCES
                    }
                }
            }
        ];
    }
})();
