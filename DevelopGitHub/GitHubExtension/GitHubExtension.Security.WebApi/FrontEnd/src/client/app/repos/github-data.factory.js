(function () {
    'use strict';
    var module = angular.module('app.repos');

    module.factory('githubData', githubData);

    githubData.$inject = ['$http'];

    /* @ngInject */
    function githubData($http) {

        function getUser (username) {

            return $http.get('https://api.github.com/users/' + username)
                        .then(function (response) {
                            return response.data;
                        });
        }

        function getRepos (user) {
            return $http.get(user['repos_url'])
                  .then(function (response) {
                      return response.data;
                  });
        }

        return {
            getUser: getUser,
            getRepos: getRepos
        };

    }

}());
