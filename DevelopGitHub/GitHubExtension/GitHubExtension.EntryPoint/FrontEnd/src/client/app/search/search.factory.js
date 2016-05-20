(function() {
    'use strict';
    angular
        .module('app.search')
        .factory('searchFactory', searchFactory);

    searchFactory.$inject = ['$http', 'API_URL'];

    /* @ngInject */
    function searchFactory($http, API_URL) {

        function getUsers(searchName) {
            return $http({
                method: 'GET',
                url: API_URL.SEARCH_USERS + searchName
            });
        }

        return {
            getUsers: getUsers
        };
    }
})();
