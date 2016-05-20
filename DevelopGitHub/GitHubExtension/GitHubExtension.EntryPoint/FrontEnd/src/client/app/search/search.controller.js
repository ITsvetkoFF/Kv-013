(function () {

    'use strict';

    angular
        .module('app.search')
        .controller('SearchController', SearchController);

    SearchController.$inject = ['logger', 'i18n', 'searchFactory', 'userService'];

    function SearchController(logger, i18n, searchFactory, userService) {
        var vm = this;
        vm.GitHubUrl = 'https://github.com/';
        vm.i18n = i18n.message;
        vm.user = userService;
        vm.isSearch = false;
        activate();

        function activate() {
            logger.info(vm.i18n.SEARCH);
        }

        vm.searchUsers = function(searchName) {
            searchFactory.getUsers(searchName).then(onFoundUsers, onNotFoundUsers);
        }

        function onFoundUsers(response) {
            vm.users = response.data;
            vm.isSearch = false;
        }

        function onNotFoundUsers(response) {
            vm.users = undefined;
            vm.isSearch = true;
        }
    }
}());
