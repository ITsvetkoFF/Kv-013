(function () {
    'use.strict';
    var serviceBase = 'http://localhost:50859/';
    angular.module('app.login', [
        'LocalStorageModule',
        'ngCookies'
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
})();


