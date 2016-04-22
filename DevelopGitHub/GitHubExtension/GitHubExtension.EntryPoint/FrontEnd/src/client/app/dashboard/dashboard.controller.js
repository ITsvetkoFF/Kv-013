(function () {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$q', 'logger', 'i18n', 'ActivityService', 'userService', 'ACTIVITY_CONSTANTS'];
    /* @ngInject */
    function DashboardController($q, logger, i18n, activityService, userService, ACTIVITY_CONSTANTS) {
        var vm = this;

        // add localization
        vm.i18n = i18n.message;
        vm.userService = userService;
        vm.title = 'Dashboard';
        vm.currentPage = ACTIVITY_CONSTANTS.FIRST_PAGE;

        // angular ui pagination directive accepts as a parameter total amount of items.
        // total amount of pages is calculated inside of it and is for read only
        // GitHub api only returns amount of pages. Each page has 30 events or less.
        // here I'm calculating amount of items manually
        vm.totalItems = ACTIVITY_CONSTANTS.EXTERNAL_ACTIVITY_ITEMS_PER_PAGE;
        vm.paginationDisabled = false;

        activate();

        function activate() {
            var promises = [];

            if (userService.isAuthenticated()) {
                promises.push(getExternalActivity(vm.currentPage), getInternalActivity());
            }
            $q.all(promises).then(dashboardActivated);
        }

        function dashboardActivated() {
            logger.info(vm.i18n.DASHBOARD_VIEW);
        }

        function activityError() {
            logger.error(vm.i18n.GET_ACTIVITIES_ERROR_MESSAGE);
        }

        function getExternalActivity(page) {
            return activityService.getExternalActivity(page).then(function(activity) {
                vm.externalActivities = activity.events;
                vm.numberOfPages = activity.amountOfPages || vm.numberOfPages;

                if (vm.numberOfPages) {
                    vm.totalItems = vm.numberOfPages * ACTIVITY_CONSTANTS.EXTERNAL_ACTIVITY_ITEMS_PER_PAGE;
                }

                return vm.externalActivities;
            }).catch(activityError);
        }

        function getInternalActivity() {
            return activityService.getInternalActivity().then(function(activity) {
                console.log(activity);
                vm.internalActivities = activity;
            });
        }

        vm.pageChanged = function() {
            vm.paginationDisabled = true;
            getExternalActivity(vm.currentPage).finally(function() {
                vm.paginationDisabled = false;
            });
        };
    }
})();
