(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .directive('customOnChange', file);

    function file() {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var onChangeHandler = scope.$eval(attrs.customOnChange);
                element.bind('change', onChangeHandler);
            }
        };
    }
})();
