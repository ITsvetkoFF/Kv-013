(function () {
    'use strict';

    angular.module('app.login')
    .controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings',
        function ($scope, $location, authService, ngAuthSettings) {

            $scope.message = '';

            $scope.authExternalProvider = function (provider) {

                var externalProviderUrl =
                    ngAuthSettings.apiServiceBaseUri + 'api/Account/ExternalLogin?provider=' + provider;

                window.$windowScope = $scope;
                location.href = externalProviderUrl;
            };

        }]);
})();
