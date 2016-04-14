(function () {
    'use strict';

    angular
      .module('blocks.localization')
      .factory('i18n', i18NFactory);

    i18NFactory.$inject = ['i18nMessages', '$window'];
    function i18NFactory(i18NMessages, $window) {
        var service = {
            setLanguage: setLanguage,
            validLanguages: Object.keys(i18NMessages),
        };

        // Set default language as the first from the constants (En)
        var lang = $window.navigator.language || $window.navigator.userLanguage || 'en-US';

        service.setLanguage(lang);

        return service;

        function setLanguage(lang) {
            service.currentLanguage = lang;
            service.message = i18NMessages[lang];

            // if we haven't json file with language constants that we needed, we use en-US by default
            if (!service.message) {
                service.message = i18NMessages['en-US'];
            }
        }
    }
})();
