(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger', 'API_URL'];

    function UserPreferencesController(userData, logger, API_URL) {
        var vm = this;

        activate();

        function activate() {
            logger.info('Activate User Preferences View');
        }

        vm.uploadFile = function (event) {
            logger.info('activate photo logining');
            userData.makeRequest(API_URL.UPLOADPHOTO, event.target.files);
        };

    }
}());
