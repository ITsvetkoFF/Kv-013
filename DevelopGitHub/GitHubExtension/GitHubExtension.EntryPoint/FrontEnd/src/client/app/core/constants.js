/* global toastr:false, moment:false */
(function() {
    'use strict';

    angular
        .module('app.core')
        .constant('toastr', toastr)
        .constant('moment', moment)
        .constant('API_URL', (function() {
            var resource = 'http://localhost:50859/api/';
            return {
                BASE_URL: resource,
                LOGIN: resource + 'account/ExternalLogin?provider=GitHub',
                LOGOUT: resource + 'account/logout',
                UPLOADPHOTO: resource + 'account/avatar'
            };
        })());
})();
