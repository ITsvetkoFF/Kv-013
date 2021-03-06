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
                USER_PHOTO: resource + 'user/avatar',
                REPOSITORY: resource + 'user/repos',
                CHECKBOXVALUE: resource + 'account/mailvisibility',
                CHANGEVISIBILITYMAIL: resource + 'account/changemail',
                ROLES: resource + 'roles',
                CURRENT_REPOSITORY: resource + 'repos/current',
                NOTE: resource + 'note',
                COLLABORATORS: '/collaborators/',
                DELETE_NOTE: resource + 'note/',
                INVITATION: resource + 'invitation',

                SEARCH_USERS: resource + 'users/search?username=',

                GET_REPOBYNAME: baseUserStats + '/commits',
                GET_FOLLOWERS: baseUserStatsInfo + '/followers',
                GET_FOLLOWING: baseUserStatsInfo + '/following',
                GET_REPOSITORIESCOUNT: baseUserStatsInfo + '/repositories-count',
                GET_ACTIVITYMONTHS: baseUserStats + '/activity-months',
                GET_REPOSITORIES: baseUserStats + '/repositories',
                GET_COMMITSREPOSITORIES: baseUserStats + '/repositories/commits',
                GET_GROUPCOMMITS: baseUserStats + '/repositories/group-commits',

                PULL_REQUEST: resource + 'templates/pull-request',
                ISSUE: resource + 'templates/issue',
                PR: resource + 'templates/pr',
                ISSUE_CATEGORIES: resource + 'templates/categories'
            };
        })());
})();
