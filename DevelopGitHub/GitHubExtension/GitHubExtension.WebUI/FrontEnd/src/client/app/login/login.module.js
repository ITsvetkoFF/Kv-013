(function() {
    'use.strict';

    angular.module('app.login', [
        'LocalStorageModule'
    ])
    .constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
    })
    //.config(function ($httpProvider) {
    //    $httpProvider.interceptors.push('authInterceptorService');
    //}
    .run(['authService', function (authService) {
        authService.fillAuthData();
    }])
    .run(appRun);

    appRun.$inject = ['routerHelper'];
    /* @ngInject */
    function appRun(routerHelper) {
        routerHelper.configureStates(getStates());
    }

    function getStates() {
        return [
            {
                state: 'login',
                config: {
                    url: '/login',
                    templateUrl: 'app/login/login.html',
                    controller: 'loginController',
                    controllerAs: 'vmLogin',
                    title: 'Login',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-dashboard"></i> Login'
                    }
                }
            },
            {
                state: 'associate',
                config: {
                    url: '/associate',
                    templateUrl: 'app/login/associate.html',
                    controller: 'associateController',
                    controllerAs: 'vmAssociate',
                    title: 'Repo list'
                }
            },
            {
                state: 'authComplete',
                config: {
                    url: '/authComplete',
                    templateUrl: 'app/login/authComplete.html'
                }
            }
        ];
    }
    var serviceBase = 'http://localhost:26264/';
    //var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
    
})();


