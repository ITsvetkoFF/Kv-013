(function () {
    'use strict';
    var module = angular.module('app.repos');

    module.factory('githubData', githubData);

    githubData.$inject = ['$http'];

    /* @ngInject */
    function githubData($http) {

        var getUser = function (username) {

            return $http.get("https://api.github.com/users/" + username)
                        .then(function (response) {
                            return response.data;
                        });
        };

        var getRepos = function (user) {
            return $http.get(user.repos_url)
                  .then(function (response) {
                      return response.data;
                  });
        }

        return {
            getUser: getUser,
            getRepos: getRepos
        };

    };

    

}());