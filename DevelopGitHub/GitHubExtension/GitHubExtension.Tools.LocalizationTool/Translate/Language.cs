using System;

namespace GitHubExtension.LocalizationTool.Translate
{
    public enum Language
    {
        Uk,
        En
    }

    public static class LanguageExtension
    {
        public static string GetLanguageString(this Language language)
        {
            switch (language)
            {
                case Language.Uk:
                    return "uk";
                case Language.En:
                    return "en-US";
                default:
                    throw new Exception("Incorrect Language in Method GetLanguage");
            }
        }

        public static string GetFileName(this Language language)
        {
            switch (language)
            {
                case Language.Uk:
                    return "uk.json";
                case Language.En:
                    return "en-US.json";
                default:
                    throw new Exception("Incorrect Language in Method GetFileName");
            }
        }
    }
}
