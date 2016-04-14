(function () {
    'use strict';

    angular.module('app.login')
    .controller('loginController', ['$scope', 'apiURLs',
        function ($scope, apiUrLs) {

            $scope.authExternalProvider = function (provider) {

                var externalProviderUrl = apiUrLs.apiLoginUrl + '?provider=' + provider;

                window.$windowScope = $scope;
                location.href = externalProviderUrl;
            };

        }]);
})();
