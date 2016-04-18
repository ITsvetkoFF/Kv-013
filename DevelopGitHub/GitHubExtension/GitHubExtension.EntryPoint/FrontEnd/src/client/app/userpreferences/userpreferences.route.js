(function () {
    'use strict';

    angular
        .module('app.userpreferences')
        .run(appRun);

    appRun.$inject = ['routerHelper'];

    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'userpreferences',
                config: {
                    url: '/userpreferences',
                    templateUrl: 'FrontEnd/src/client/app/userpreferences/userpreferences.html',
                    controller: 'UserPreferencesController',
                    controllerAs: 'vmPreferences',
                    title: 'User Preferences',
                    settings: {
                        nav: 6,
                        content: '<i class="fa fa-dashboard"></i> Preferences'
                    }
                }
            }
        ];
    }
})();
