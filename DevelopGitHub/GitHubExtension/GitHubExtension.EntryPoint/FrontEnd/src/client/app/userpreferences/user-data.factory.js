(function () {
    'use strict';

    var module = angular.module('app.userpreferences');

    module.factory('userData', userData);

    userData.$inject = ['$http', 'API_URL'];

    function userData($http, API_URL) {

        function postImage(files) {
            var fd = new FormData();
            fd.append('file', files[0]);
            return $http.post(API_URL.USER_PHOTO, fd, {
                transformRequest: angular.identity, //to make authomatical serialisation
                headers: { 'Content-Type': undefined } //to make content-type multipart/from-data
            }).then(successCb);
        }

        function getImage() {
            return $http({
                method: 'GET',
                url: API_URL.USER_PHOTO
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
