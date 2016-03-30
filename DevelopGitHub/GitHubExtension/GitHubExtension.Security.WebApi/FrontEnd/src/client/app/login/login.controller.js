(function () {
    'use strict';

    angular.module('app.login')
    .controller('loginController', ['$scope', 'apiURLs',
        function ($scope, apiURLs) {

            $scope.authExternalProvider = function (provider) {

                var externalProviderUrl = apiURLs.apiLoginUrl + '?provider=' + provider;

                window.$windowScope = $scope;
                location.href = externalProviderUrl;
            };

        }]);
})();
