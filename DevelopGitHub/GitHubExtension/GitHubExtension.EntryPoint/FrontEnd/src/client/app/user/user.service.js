(function() {
    'use strict';
    angular
        .module('app.user')
        .factory('userService', userService);

    userService.$inject = ['$cookies', '$http', '$state', 'API_URL', 'logger', 'i18n'];

    /* @ngInject */
    function userService($cookies, $http, $state, API_URL, logger, i18n) {
        var RepositoryList;
        var CurrentRepository;
        var i18n = i18n.message;

        function login() {
            location.href = API_URL.LOGIN;
        }

        function logout() {
            $http.post(API_URL.LOGOUT, {}).then(function() {
                var cookies = $cookies.getAll();
                angular.forEach(cookies, function (value, key) {
                    $cookies.remove(key);
                });
                $state.go('dashboard');
            });
        }

        function userName() {
            return $cookies.get('userName');
        }

        function isAuthenticated() {
            return !!$cookies.get('userName');
        }

        function loadRepositories() {
            return $http({
                method: 'GET',
                url: API_URL.REPOSITORY
            }).then( function(responce){
                RepositoryList = responce.data;
                logger.info(i18n.REPOSITORIES_LOAD_MESSAGE);
            });
        }

        function getCurrentRepository() {
            return CurrentRepository;
        }

        function setCurrentRepository(repository) {
            return $http({
                method: 'PATCH',
                dataType: 'json',
                url: API_URL.CURRENT_REPOSITORY,
                data: {
                    Id: repository.id
                }
            }).then( function(){
                CurrentRepository = repository;
                $state.reload();
                logger.info(i18n.CHANGE_REPOSITORY_MESSAGE);
            }, function() {
                logger.error(i18n.CHANGE_REPOSITORY_ERROR_MESSAGE);
            });
        }

        function getRepositoryList() {
            return RepositoryList;
        }

        return {
            login: login,
            logout: logout,
            userName: userName,
            isAuthenticated: isAuthenticated,
            getCurrentRepository: getCurrentRepository,
            setCurrentRepository: setCurrentRepository,
            loadRepositories: loadRepositories,
            getRepositoryList: getRepositoryList
        };
    }
})();
