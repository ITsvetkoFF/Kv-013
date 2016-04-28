(function () {
    'use strict';
    var module = angular.module('app.templates');

    module.factory('githubTemplates', githubTemplates);

    githubTemplates.$inject = ['$http', 'API_URL'];

    /* @ngInject */
    function githubTemplates($http, API_URL) {
           
        function getPullRequestTemplate() {
            return $http({
                method: 'GET',
                url: API_URL.PULL_REQUEST
            }).success(function (response) {
                return response.data;
            }).error(function (response) {
                return "";
            });
        }

        function getIssueTemplate() {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'GET',
                url: API_URL.ISSUE
            }).success(function (response) {
                return response.data;
            }).error(function (response) {
                return "";
            });
        }

        function getPr() {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'GET',
                url: API_URL.PR
            }).success(function (response) {
                return  response.data;
            }).error(function (response) {
                return "";
            });
        }

        function createPrTemplate(prTemplate) {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'POST',
                url: API_URL.CREATE_UPDATE_PR,
                data : { message : prTemplate.comment, content : prTemplate.content}
            }).success(function (response) {
                return response.status;
            }).error(function (response) {
                return "";
            });
        }
        function createIssueTemplate(issueTemplate) {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'POST',
                url: API_URL.CREATE_UPDATE_ISSUE,
                data: { message: issueTemplate.comments, content: issueTemplate.content }
            }).success(function (response) {
                return response.status;
            }).error(function (response) {
                return "";
            });
        }

        function updatePrTemplate(prTemplate) {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'PUT',
                url: API_URL.CREATE_UPDATE_PR,
                data: { message: prTemplate.commentEdit, content: prTemplate.pullRequestTemplate }
            }).success(function (response) {
                return response.status;
            }).error(function (response) {
                return "";
            });
        }

        function updateIssueTemplate(issueTemplate) {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'PUT',
                url: API_URL.CREATE_UPDATE_ISSUE,
                data: { message: issueTemplate.commentsEdit, content: issueTemplate.issueTemplate }
            }).success(function (response) {
                return response.status;
            }).error(function (response) {
                return "";
            });
        }

        function getIssueCategories() {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'GET',
                url:API_URL.ISSUE_CATEGORIES
            }).success(function (response) {
                return response.data;
            }).error(function (response) {
                return "";
            });
        }

        function getIssueTemplateByCategoryId(index) {
            return $http({
                headers: { 'Content-Type': 'application/json' },
                method: 'GET',
                url: API_URL.ISSUE_CATEGORIES + '/' + index  
            }).success(function (response) {
                return response.data;
            }).error(function (response) {
                return "";
            });
        }

        return {
            getPullRequestTemplate: getPullRequestTemplate,
            getIssueTemplate: getIssueTemplate,
            getPr: getPr,
            createPrTemplate: createPrTemplate,
            updatePrTemplate: updatePrTemplate,
            updateIssueTemplate: updateIssueTemplate,
            getIssueCategories: getIssueCategories,
            getIssueTemplateByCategoryId: getIssueTemplateByCategoryId,
            createIssueTemplate: createIssueTemplate
        };
    }
})();
