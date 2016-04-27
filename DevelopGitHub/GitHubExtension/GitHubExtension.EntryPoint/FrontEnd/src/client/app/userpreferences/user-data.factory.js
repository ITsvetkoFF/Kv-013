(function () {
    'use strict';

    var module = angular.module('app.userpreferences');

    module.factory('userData', userData);

    userData.$inject = ['$http'];

    function userData($http) {

        function postImage(route, files) {
            var fd = new FormData();
            fd.append('file', files[0]);
            return $http.post(route, fd, {
                transformRequest: angular.identity, //to make authomatical serialisation
                headers: { 'Content-Type': undefined } //to make content-type multipart/from-data
            }).then(successCb);
        }

        function getImage(route) {
            return $http({
                method: 'GET',
                url: route
            }).then(successCb);
        }

        function successCb(response) {
            return response.data;
        }

        return {
            postImage: postImage,
            getImage: getImage
        };
    }
})();
