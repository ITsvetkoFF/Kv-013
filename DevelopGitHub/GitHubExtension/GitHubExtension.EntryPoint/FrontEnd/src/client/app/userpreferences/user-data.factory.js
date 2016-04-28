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

        function getCheckboxValue() {

            return $http.get(API_URL.CHECKBOXVALUE)
                        .then(function (response) {
                            return response.data;
                        });
        }

        function changeVisibilityMail() {
            $http.put(API_URL.CHANGEVISIBILITYMAIL);
        }        

        return {
            postImage: postImage,
<<<<<<< 9af4be1f9eb83e400c4213a96222bc4a8cbfd1a1
            getImage: getImage
=======
            getCheckboxValue: getCheckboxValue,
            changeVisibilityMail: changeVisibilityMail
>>>>>>> Add front end
        };
    }
})();
