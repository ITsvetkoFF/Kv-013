(function () {
    'use strict';
    var module = angular.module('app.roles');

    module.factory('githubCollaborators', githubCollaborators);

    githubCollaborators.$inject = ['$http'];

    /* @ngInject */
    function githubCollaborators($http) {

        function getCollaborators(username) {
            console.log(username);
            return $http.get('https://api.github.com/repos/' + username + "/MessageOfTheDay/collaborators?access_token=610b7b207ce17feaa6058286975ec34bb3cb4f59").then(function (response) {return response.data;});
        }

//        function getLogin(user) {
//            return $http.get(user['login'])
//                  .then(function (response) {
//                      return response.data;
//                  });
//        }

        return {
            getCollaborators: getCollaborators
//            getLogin: getLogin
        };

    }

}());