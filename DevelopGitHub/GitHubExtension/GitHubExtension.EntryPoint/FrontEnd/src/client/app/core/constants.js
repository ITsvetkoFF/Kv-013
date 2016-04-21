/* global toastr:false, moment:false */
(function() {
    'use strict';

    angular
        .module('app.core')
        .constant('toastr', toastr)
        .constant('moment', moment)
        .constant('API_URL', (function () {
            var resource = 'http://localhost:50859/api/';
            var baseUserStats = resource + 'user/stats';
            var baseUserStatsInfo = baseUserStats + '/info';
            return {
                BASE_URL: resource,
                LOGIN: resource + 'account/ExternalLogin?provider=GitHub',
                LOGOUT: resource + 'account/logout',
                internalActivityUrl: resource + 'activity/internal',
                externalActivityUrl: resource + 'activity/external?page=',
                UPLOADPHOTO: resource + 'account/avatar'

                GET_REPOBYNAME: baseUserStats + '/commits',
                GET_FOLLOWERS: baseUserStatsInfo + '/followers',
                GET_FOLLOWING: baseUserStatsInfo + '/following',
                GET_REPOSITORIESCOUNT: baseUserStatsInfo + '/repositories-count',
                GET_ACTIVITYMONTHS: baseUserStats + '/activity-months',
                GET_REPOSITORIES: baseUserStats + '/repositories',
                GET_COMMITSREPOSITORIES: baseUserStats + '/repositories/commits',
                GET_GROUPCOMMITS: baseUserStats + '/repositories/group-commits'
            };
        })());
})();
