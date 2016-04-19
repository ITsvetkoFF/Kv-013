(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger', 'API_URL'];

    function UserPreferencesController(userData, logger, API_URL) {
        var vm = this;
        vm.files = {};

        activate();

        function activate() {
            logger.info('Activate User Preferences View');
        }

        vm.uploadFile = function (event) {
            vm.files = event.target.files;
            var fd = new FormData();
            fd.append('file', vm.files[0]);
            userData.makeRequest(API_URL.UPLOADPHOTO, fd);
        };

    }
}());
