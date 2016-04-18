(function () {
    'use strict';

    var module = angular.module('app.userpreferences');

    module.factory('userData', userData);

    userData.$inject = ['$http'];

    function userData($http) {
        var baseUrl = 'http://localhost:50859/api/account';

        function postImage(fd) {
            $http.post('http://localhost:50859/api/account/avatar', fd, {
                withCredentials: true,
                headers: {'Content-Type': undefined},
                transformRequest: angular.identity
            });
        }

        return {
            makeRequest: postImage,
        };
    }
})();
