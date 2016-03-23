(function () {
    'use strict';

    angular
      .module('blocks.localization')
      .factory('i18n', i18nFactory);

    i18nFactory.$inject = ['i18nMessages'];
    function i18nFactory(i18nMessages) {
        var service = {
            setLanguage: setLanguage,
            validLanguages: Object.keys(i18nMessages),
        };
        // Set default language as the first from the constants (En)
        service.setLanguage(service.validLanguages[0]);
        return service;

        function setLanguage(newValue) {
            console.log(newValue);
            service.currentLanguage = newValue;
            console.log(i18nMessages[newValue]);
            service.message = i18nMessages[newValue];
        }
    }
})();