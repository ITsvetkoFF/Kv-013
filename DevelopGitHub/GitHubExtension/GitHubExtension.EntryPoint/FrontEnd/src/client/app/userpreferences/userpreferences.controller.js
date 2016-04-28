(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger', 'API_URL', 'i18n'];

    function UserPreferencesController(userData, logger, API_URL, i18n) {
        var vm = this;
        vm.imageSource = ''; 

        vm.i18n = i18n.message;

        activate();

        function activate() {
            logger.info(vm.i18n.ACTIVATE_USER_PREFERENCES);
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
