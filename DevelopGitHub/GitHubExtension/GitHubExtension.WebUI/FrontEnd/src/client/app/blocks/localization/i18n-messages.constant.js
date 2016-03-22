﻿(function () {
    'use strict';

    var i18nMessages = {
        en: {
            'DASHBOARD': 'Dashboard',
            'ADMIN': 'Admin',
            'REPOS': 'Repositories',
            'COLLABORATORS': 'Collaborators',
            'MESSAGES': 'Messages',
            'AUTHORIZATION' : 'Authorization'
        },
        ua: {
            'DASHBOARD': 'Панель управління',
            'ADMIN': 'Адміністратор',
            'REPOS': 'Сховища',
            'COLLABORATORS': 'Коллаборатори',
            'MESSAGES': 'Повідомлення',
            'AUTHORIZATION': 'Авторизація'
        },
    };

    angular
      .module('blocks.localization')
      .constant('i18nMessages', i18nMessages);
})();