(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger', 'API_URL', 'i18n'];

    function UserPreferencesController(userData, logger, API_URL, i18n) {
        var vm = this;

        vm.i18n = i18n.message;

        activate();

        function activate() {
            logger.info(vm.i18n.ACTIVATE_USER_PREFERENCES);
        }

        vm.uploadFile = function (event) {
            userData.postImage(API_URL.UPLOADPHOTO, event.target.files);
        };

    }
}());
