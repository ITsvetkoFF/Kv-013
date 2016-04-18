/* global toastr:false, moment:false */
(function() {
    'use strict';

    angular
        .module('app.core')
        .constant('toastr', toastr)
        .constant('moment', moment)
        .constant('apiURLs', {
            apiLoginUrl: 'http://localhost:50859/api/Account/ExternalLogin',
            apiChangeAvatar: 'http://localhost:50859/api/account/avatar'
        });
})();
