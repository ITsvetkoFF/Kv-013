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
            //Take the first selected file
            fd.append('file', vm.files[0]);
            userData.makeRequest(fd);
            //githubCollaborators.changePhoto(fd);
            //$.ajax({
            //    type: "Patch",
            //    url: '/api/MyController/UploadFile?id=' + myID,
            //    contentType: false,
            //    processData: false,
            //    data: data,
            //    success: function (result) {
            //        console.log(result);
            //    },
            //    error: function (xhr, status, p3, p4) {
            //        var err = "Error " + " " + status + " " + p3 + " " + p4;
            //        if (xhr.responseText && xhr.responseText[0] == "{")
            //            err = JSON.parse(xhr.responseText).Message;
            //        console.log(err);
            //    }
            //});
            //userData.makeRequest(fd);
        };

    }
}());
