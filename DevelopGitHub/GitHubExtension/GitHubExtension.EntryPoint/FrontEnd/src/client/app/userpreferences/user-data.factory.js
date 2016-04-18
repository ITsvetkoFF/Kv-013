(function () {
    'use strict';

    var module = angular.module('app.userpreferences');

    module.factory('userData', userData);

    userData.$inject = ['$http'];

    function userData($http) {

        function postImage(route, fd) {
            $http.post(route, fd, {
            });
        }

        return {
            makeRequest: postImage,
        };
    }
})();
