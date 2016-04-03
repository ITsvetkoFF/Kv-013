(function () {
    'use strict';
    var module = angular.module('app.stats');

    module.factory('statsFactory', statsFactory);

    statsFactory.$inject = ['$http'];

    function statsFactory($http) {
        var baseUrl = 'api/user/commits';

        function getCollaborators() {
            return $http({
                method: 'GET',
                url: baseUrl
            });
        }

        return {
            getCollaborators: getCollaborators
        };
    }
})();
