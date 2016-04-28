using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private static ITranslationData _translationDataTable;

        public static ITranslationData TranslationDataTable
        {
            set
            {
                _translationDataTable = value;
            }
        }

        private static ObservableCollection<TranslationDataRow> TranslationData
        {
            get
            {
                return _translationDataTable.TranslationData;
            }
        }

        public static void ReadJsonFromFile(Lang language)
        {
            var fileName = Translator.GetFileName(language);
            var fileText = File.ReadAllText(fileName);
            var file = JObject.Parse(fileText);
            var translation = file.Value<JObject>(Translator.GetLang(language));
            FillDataFromJson(language, translation);
        }

        public static string GenerateJson(Lang language)
        {
            RemoveEmptyRows();
            var result = new StringBuilder();
            result.Append(FirstOpenQuote);
            result.Append(Translator.GetLang(language));
            result.Append(SecondOpenQuote);
            if (TranslationData.Count != 0)
            {
                for (var i = 0; i < TranslationData.Count - 1; i++)
                {
                    result.Append(DoubleQuote + TranslationData[i].Name + DoubleQuote);
                    result.Append(Colon);
                    result.Append(DoubleQuote + TranslationData[i][language] + DoubleQuote);
                    result.Append(Comma);
                }

                result.Append(DoubleQuote + TranslationData[TranslationData.Count - 1].Name + DoubleQuote);
                result.Append(Colon);
                result.Append(DoubleQuote + TranslationData[TranslationData.Count - 1][language] + DoubleQuote);
            }

            result.Append(CloseBrackets);
            return result.ToString();
        }

        public static void RemoveEmptyRows()
        {
            for (var i = 0; i < TranslationData.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(TranslationData[i].Name))
                {
                    TranslationData.Remove(TranslationData[i--]);
                }
            }
        }

        public static void AddNewRow(Lang language, KeyValuePair<string, JToken> element)
        {
            var line = new TranslationDataRow(element.Key);
            line[language] = element.Value.ToString();
            TranslationData.Add(line);
        }

        private static void FillDataFromJson(Lang language, JObject translation)
        {
            foreach (var element in translation)
            {
                var index = IndexOfNamedElement(element.Key);
                if (index == -1)
                {
                    AddNewRow(language, element);
                }
                else
                {
                    TranslationData[index][language] = element.Value.ToString();
                }
            }
        }

        private static int IndexOfNamedElement(string key)
        {
            for (var i = 0; i < TranslationData.Count; i++)
            {
                if (TranslationData[i].Name == key)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}