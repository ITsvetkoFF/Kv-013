(function () {
    'use strict';

    angular
      .module('blocks.localization')
      .factory('i18n', i18nFactory);

    i18nFactory.$inject = ['i18nMessages', '$window'];
    function i18nFactory(i18nMessages, $window) {
        var service = {
            setLanguage: setLanguage,
            validLanguages: Object.keys(i18nMessages),
        };

        // Set default language as the first from the constants (En)
        var lang = $window.navigator.language || $window.navigator.userLanguage || 'en-US';

        service.setLanguage(lang);

        return service;

        function setLanguage(lang) {
            console.log(lang);
            service.currentLanguage = lang;
            console.log(i18nMessages[lang]);
            service.message = i18nMessages[lang];

            // if we haven't json file with language constants that we needed, we use en-US by default
            if (!service.message)
                service.message = i18nMessages['en-US'];
        }
    }
})();