(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger', 'API_URL'];

    function UserPreferencesController(userData, logger, API_URL) {
        var vm = this;
        vm.isEmailPrivate = false;

        activate();

        function activate() {
            logger.info('Activate User Preferences View');
        }

        vm.uploadFile = function (event) {
            userData.postImage(event.target.files);
        };

        vm.changeVisibilityMail = function () {
            userData.changeVisibilityMail();
        } 

        userData.getCheckboxValue()
            .then(function (data) { vm.isEmailPrivate = data; });
    }
}());
