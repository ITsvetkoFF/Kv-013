(function () {
    'use strict';

    angular
    .module('app.userpreferences')
    .directive('geCustomOnChange', geCustomOnChange);

    function geCustomOnChange() {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var onChangeHandler = scope.$eval(attrs.geCustomOnChange);
                element.bind('change', onChangeHandler);
            }
        };
    }
})();
