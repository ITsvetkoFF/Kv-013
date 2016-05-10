using System;

namespace GitHubExtension.LocalizationTool.Translate
{
    public enum Language
    {
        Uk,
        En,
        Ru
    }

    public static class LanguageExtension
    {
        public static Language GetLanguageEnum(this string language)
        {
            switch (language)
            {
                case "uk":
                    return Language.Uk;
                case "en":
                    return Language.En;
                case "ru":
                    return Language.Ru;
                default:
                    throw new Exception("Incorrect Language in Method GetLanguage");
            }
        }

        public static string GetLanguageString(this Language language)
        {
            switch (language)
            {
                case Language.Uk:
                    return "uk-UK";
                case Language.En:
                    return "en-US";
                case Language.Ru:
                    return "ru-RU";
                default:
                    throw new Exception("Incorrect Language in Method GetLanguage");
            }
        }

        public static string GetFileName(this Language language)
        {
            switch (language)
            {
                case Language.Uk:
                    return "uk-UK.json";
                case Language.En:
                    return "en-US.json";
                case Language.Ru:
                    return "ru-RU.json";
                default:
                    throw new Exception("Incorrect Language in Method GetFileName");
            }
        }
    }
}
