(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .controller('UserPreferencesController', UserPreferencesController)

    UserPreferencesController.$inject = ['userData', 'logger'];

    function UserPreferencesController(userData, logger) {
        var vm = this;
        vm.checkfield = 'hello';
        vm.files = {};
        vm.title = 'UserPreferences';

        activate();

        function activate() {
            logger.info('Activate User Preferences View');
        }

        vm.uploadFile = function (event) {
            vm.files = event.target.files;
            var fd = new FormData();
            fd.append('file', vm.files[0]);
            userData.makeRequest(fd);
        };

    }
}());
