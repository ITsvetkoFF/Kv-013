(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController);

    UserPreferencesController.$inject = ['userData', 'logger', 'apiURLs'];

    function UserPreferencesController(userData, logger, apiURLs) {
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
            userData.makeRequest(apiURLs.apiChangeAvatar, fd);
        };

    }
}());
