(function () {
    'use strict';

    angular
        .module('app.activity')
        .factory('ActivityService', ActivityService);

    ActivityService.$inject = ['$http', 'API_URL'];

    function ActivityService($http, API_URL) {

        function getExternalActivity(page) {
            return $http.get(API_URL.externalActivityUrl + page).then(successCb);
        }

        function getInternalActivity() {
            return $http.get(API_URL.internalActivityUrl).then(successCb);
        }

        function successCb(response) {
            return response.data;
        }

        return {
            'getExternalActivity': getExternalActivity,
            'getInternalActivity': getInternalActivity
        };
    }
})();
