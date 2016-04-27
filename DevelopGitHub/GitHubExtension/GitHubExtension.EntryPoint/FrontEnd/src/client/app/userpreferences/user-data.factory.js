(function () {
    'use strict';

    var module = angular.module('app.userpreferences');

    module.factory('userData', userData);

    userData.$inject = ['$http', 'API_URL'];

    function userData($http, API_URL) {

        function postImage(files) {
            var fd = new FormData();
            fd.append('file', files[0]);
            $http.post(API_URL.UPLOADPHOTO, fd, {
                transformRequest: angular.identity, //to make authomatical serialisation
                headers: {'Content-Type': undefined} //to make content-type multipart/from-data
            });
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
            getCheckboxValue: getCheckboxValue,
            changeVisibilityMail: changeVisibilityMail
        };
    }
})();
