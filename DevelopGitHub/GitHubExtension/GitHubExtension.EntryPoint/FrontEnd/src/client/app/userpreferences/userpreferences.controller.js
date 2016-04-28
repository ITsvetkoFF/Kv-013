(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger'];

    function UserPreferencesController(userData, logger) {
        var vm = this;
        vm.imageSource = ''; 

        activate();

        function activate() {
            uploadPhoto();
            logger.info('Activate User Preferences View');
        }

        function uploadPhoto() {
            return userData.getImage().then(function (newImageUrl) {
                vm.imageSource = newImageUrl;
            });
        }

        vm.uploadFile = function (event) {
            return userData.postImage(event.target.files).then(function (newImageUrl) {
                // use Math.random() to upload ng-src even if returns the same newImageUrl
                 vm.imageSource = newImageUrl +'?r='+ Math.round(Math.random() * 999999);
            });
        };

    }
}());
