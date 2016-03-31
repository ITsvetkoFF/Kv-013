(function () {

    'use strict';

    angular
     .module('app.templates')
     .controller('TemplatesController', TemplatesController);

    TemplatesController.$inject = ['logger'];

    /* @ngInject */
    function TemplatesController(logger) {

        var vm = this;
        vm.title = 'Templates';

        activate();

        function activate() {
            logger.info('Activated Templates View');
        }

        function onError(reason) {
            vm.error = 'Could not fetch the data.';
        }

    }

}());
