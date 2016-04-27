(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger', 'API_URL'];

    function UserPreferencesController(userData, logger, API_URL) {
        var vm = this;
        vm.imageSource = ''; 

        activate();

        function activate() {
            uploadPhoto();
            logger.info('Activate User Preferences View');
        }

        function uploadPhoto() {
            return userData.getImage(API_URL.UPLOADPHOTO).then(function (newImageUrl) {
                vm.imageSource = newImageUrl;
            });
        }

        vm.uploadFile = function (event) {
            return userData.postImage(API_URL.UPLOADPHOTO, event.target.files).then(function (newImageUrl) {
                console.log(newImageUrl);
                vm.imageSource = newImageUrl +'?r='+ Math.round(Math.random() * 999999);
            });
        };

    }
}());
