(function () {
    'use strict';
    var serviceBase = 'http://localhost:50859/';
    angular.module('app.login', [
            'ngCookies'
        ])
        .constant('ngAuthSettings', {
            apiServiceBaseUri: serviceBase,
            clientId: 'ngAuthApp'
        });

})();

