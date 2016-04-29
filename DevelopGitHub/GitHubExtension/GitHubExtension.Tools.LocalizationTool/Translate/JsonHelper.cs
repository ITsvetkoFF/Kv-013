using System.Collections.Generic;
using System.IO;
using System.Text;
using GitHubExtension.LocalizationTool.Model;

using Newtonsoft.Json.Linq;

namespace GitHubExtension.LocalizationTool.Translate
{
    public static class JsonHelper
    {
        private const string DoubleQuote = "\"";

        private const string CloseBrackets = "}}";

        private const string Colon = ":";

        private const string Comma = ",";

        private const string FirstOpenQuote = "{\"";

        private const string SecondOpenQuote = "\":{";

        public static void ReadJsonFromFile(Lang language, ITranslationData translationData)
        {
            var fileName = Translator.GetFileName(language);
            var fileText = File.ReadAllText(fileName);
            var file = JObject.Parse(fileText);
            var translation = file.Value<JObject>(Translator.GetLang(language));
            FillDataFromJson(language, translation, translationData);
        }

        public static string GenerateJson(Lang language, ITranslationData translationData)
        {
            RemoveEmptyRows(translationData);
            var result = new StringBuilder();
            result.Append(FirstOpenQuote);
            result.Append(Translator.GetLang(language));
            result.Append(SecondOpenQuote);
            if (translationData.TranslationTable.Count != 0)
            {
                for (var i = 0; i < translationData.TranslationTable.Count - 1; i++)
                {
                    result.Append(DoubleQuote + translationData.TranslationTable[i].Name + DoubleQuote);
                    result.Append(Colon);
                    result.Append(DoubleQuote + translationData.TranslationTable[i][language] + DoubleQuote);
                    result.Append(Comma);
                }

                result.Append(DoubleQuote + translationData.TranslationTable[translationData.TranslationTable.Count - 1].Name + DoubleQuote);
                result.Append(Colon);
                result.Append(DoubleQuote + translationData.TranslationTable[translationData.TranslationTable.Count - 1][language] + DoubleQuote);
            }

            result.Append(CloseBrackets);
            return result.ToString();
        }

        public static void RemoveEmptyRows(ITranslationData translationData)
        {
            for (var i = 0; i < translationData.TranslationTable.Count; i++)
            {
                if (translationData.TranslationTable[i].IsRowEmpty)
                {
                    translationData.TranslationTable.Remove(translationData.TranslationTable[i--]);
                }
            }
        }

        public static void AddNewRow(Lang language, KeyValuePair<string, JToken> element, ITranslationData translationData)
        {
            var line = new TranslationDataRow(element.Key);
            line[language] = element.Value.ToString();
            translationData.TranslationTable.Add(line);
        }

        private static void FillDataFromJson(Lang language, JObject translation, ITranslationData translationData)
        {
            foreach (var element in translation)
            {
                var index = IndexOfNamedElement(element.Key, translationData);
                if (index == -1)
                {
                    AddNewRow(language, element, translationData);
                }
                else
                {
                    translationData.TranslationTable[index][language] = element.Value.ToString();
                }
            }
        }

        private static int IndexOfNamedElement(string key, ITranslationData translationData)
        {
            for (var i = 0; i < translationData.TranslationTable.Count; i++)
            {
                if (translationData.TranslationTable[i].Name == key)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}