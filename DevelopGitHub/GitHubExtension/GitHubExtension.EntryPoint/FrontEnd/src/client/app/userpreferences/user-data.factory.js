(function () {
    'use strict';

    var module = angular.module('app.userpreferences');

    module.factory('userData', userData);

    userData.$inject = ['$http'];

    function userData($http) {

        function postImage(route, files) {
            var fd = new FormData();
            fd.append('file', files[0]);
            $http.post(route, fd, {
                transformRequest: angular.identity, //to make authomatical serialisation
                headers: {'Content-Type': undefined} //to make content-type multipart/from-data
            });
        }

        return {
            makeRequest: postImage,
        };
    }
})();
