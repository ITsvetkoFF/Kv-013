(function () {
    'use strict';
    var module = angular.module('app.templates');

    module.factory('githubTemplates', githubTemplates);

    githubTemplates.$inject = ['$http'];

    /* @ngInject */
    function githubTemplates($http) {
        var baseUrl = 'http://localhost:50859/api';

        function getPullRequestTemplate() {
            return $http({
                method: 'GET',
                url: baseUrl + '/templates/pullRequestTemplate'
            }).then(function (response) { return response.data; });
        }

        function getIssueTemplate() {
            return $http({
                method: 'GET',
                url: baseUrl + '/templates/issueTemplate'
            }).then(function (response) { return response.data; });
        }

        return {
            getPullRequestTemplate: getPullRequestTemplate,
            getIssueTemplate: getIssueTemplate
        };
    }
})();
