(function () {
    'use.strict';

    angular.module('app.login')
    .controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

        $scope.message = "";

        $scope.authExternalProvider = function (provider) {

            var redirectUri = "http://localhost:3000/authComplete";

            var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                        + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                        + "&redirect_uri=" + redirectUri;

            window.$windowScope = $scope;
            location.href = externalProviderUrl;
        };
        
    }]);
})();
