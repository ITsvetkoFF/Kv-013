using System.Collections.Generic;
using System.IO;
using System.Text;
using GitHubExtension.LocalizationTool.Model;
using GitHubExtension.LocalizationTool.Translate;

using Newtonsoft.Json.Linq;

namespace GitHubExtension.LocalizationTool.Helper
{
    public static class JsonHelper
    {
        public static void ReadJsonFromFile(this ITranslationData translationData, Language language)
        {
            var fileName = language.GetFileName();
            var fileText = File.ReadAllText(fileName);
            var file = JObject.Parse(fileText);
            var translation = file.Value<JObject>(language.GetLanguageString());
            FillDataFromJson(language, translation, translationData);
        }

        public static string GenerateJsonFromData(this ITranslationData translationData, Language language)
        {
            translationData.RemoveEmptyRows();
            var jsonTable = new JObject();
            foreach (var translationRow in translationData.TranslationTable)
            {
                jsonTable.Add(translationRow.Name, translationRow[language]);
            }

            var jsonFile = new JObject { { language.GetLanguageString(), jsonTable } };
            return jsonFile.ToString();
        }

        public static void RemoveEmptyRows(this ITranslationData translationData)
        {
            for (var i = 0; i < translationData.TranslationTable.Count; i++)
            {
                if (translationData.TranslationTable[i].IsRowEmpty)
                {
                    translationData.TranslationTable.Remove(translationData.TranslationTable[i--]);
                }
            }
        }

        public static void AddNewRow(this ITranslationData translationData, Language language, KeyValuePair<string, JToken> jsonRow)
        {
            var line = new TranslationDataRow(jsonRow.Key);
            line[language] = jsonRow.Value.ToString();
            translationData.TranslationTable.Add(line);
        }

        private static void FillDataFromJson(Language language, JObject jsonTable, ITranslationData translationData)
        {
            foreach (var jsonRow in jsonTable)
            {
                var index = translationData.IndexOfNamedElement(jsonRow.Key);
                if (index == -1)
                {
                    translationData.AddNewRow(language, jsonRow);
                }
                else
                {
                    translationData.TranslationTable[index][language] = jsonRow.Value.ToString();
                }
            }
        }

        private static int IndexOfNamedElement(this ITranslationData translationData, string keyName)
        {
            for (var i = 0; i < translationData.TranslationTable.Count; i++)
            {
                if (translationData.TranslationTable[i].Name == keyName)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}