(function () {

    'use strict';

    angular
     .module('app.templates')
     .controller('TemplatesController', TemplatesController);

    TemplatesController.$inject = ['githubTemplates', 'logger', 'i18n', 'userService'];

    /* @ngInject */
    function TemplatesController(githubTemplates, logger, i18n, userService) {
        var vm = this;
        vm.i18n = i18n;
        vm.userService = userService;
        vm.title = 'Templates';

        vm.editorOptions = {
            lineWrapping: true,
            lineNumbers: true,
            mode: "markdown",
            viewportMargin: Infinity
        };

    }

}());
