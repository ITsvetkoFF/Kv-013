(function () {
    'use strict';

    angular.module('app.activity')
        .directive('geActivity', ActivityDirective);

    ActivityDirective.$inject = [
        '$templateCache',
        '$compile',
        'logger',
        'moment',
        'ACTIVITY_CONSTANTS',
        'i18n'
    ];

    // Directive gets template from templateCache base on property type of activity that is passed to it
    function ActivityDirective($templateCache, $compile, logger, moment, ACTIVITY_CONSTANTS, i18n) {
        function link(scope, element) {
            var type = scope.activity.type;
            scope.i18n = i18n.message;
            scope.formatDate = formatDate;
            scope.getPullRequestAction = getPullRequestAction;

            // We have new type of activity from GitHub
            if (!type) {
                logger.error(scope.i18n.UNKNOWN_ACTIVITY_TYPE_MESSAGE);
                return;
            }

            // get template from template cache
            // path to template format Frontend/src/client/app/activity/{typeOfEvent}-activity.html
            // path to template example: Frontend/src/client/app/activity/CommitCommentEvent-activity.html
            var template = $templateCache.get(
                ACTIVITY_CONSTANTS.ACTIVITY_PATH_PREFIX + type + ACTIVITY_CONSTANTS.ACTIVITY_PATH_POSTFIX
            );
            element.html(template).show();
            $compile(element.contents())(scope);
        }

        function formatDate(date) {
            return moment(date).format(ACTIVITY_CONSTANTS.DATE_FORMAT);
        }

        function getPullRequestAction(action, pullRequest) {
            return action === 'closed' && pullRequest.merged ? 'merged' : action;
        }

        return {
            restrict: 'E',
            link: link,
            scope: {
                activity: '='
            }
        };
    }
})
();
