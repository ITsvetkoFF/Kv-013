using System;

namespace GitHubExtension.LocalizationTool.Translate
{
    public enum Language
    {
        Uk,
        Us,
        Ru
    }

    public static class LanguageExtension
    {
        public static Language GetLanguageEnum(this string language)
        {
            switch (language)
            {
                case "uk":
                case "UK":
                case "uk-UK":
                case "uk-UK.json":
                    return Language.Uk;
                case "us":
                case "US":
                case "en-Us":
                case "en-Us.json":
                    return Language.Us;
                case "ru":
                case "RU":
                case "ru-RU":
                case "ru-RU.json":
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
                case Language.Us:
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
                case Language.Us:
                    return "en-US.json";
                case Language.Ru:
                    return "ru-RU.json";
                default:
                    throw new Exception("Incorrect Language in Method GetFileName");
            }
        }
    }
}
