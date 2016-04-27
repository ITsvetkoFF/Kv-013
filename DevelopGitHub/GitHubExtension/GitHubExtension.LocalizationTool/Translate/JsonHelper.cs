using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using GitHubExtension.LocalizationTool.Model;
using GitHubExtension.LocalizationTool.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.LocalizationTool.Translate
{
    public class JsonHelper
    {
        private const string DoubleQuote = "\"";

        private const string CloseBrackets = "}}";

        private const string Colon = ":";

        private const string Comma = ",";

        private const string FirstOpenQuote = "{\"";

        private const string SecondOpenQuote = "\":{";

        private readonly ObservableCollection<TranslationDataRow> _translationData;

        public JsonHelper(ObservableCollection<TranslationDataRow> translationData)
        {
            _translationData = translationData;
        }

        public void ReadJsonFromFile(Lang language)
        {
            var fileName = Translator.GetFileName(language);
            var fileText = File.ReadAllText(fileName);
            var file = JObject.Parse(fileText);
            var translation = file.Value<JObject>(Translator.GetLang(language));
            FillDataFromJson(language, translation);
        }

        public string GenerateJson(Lang language)
        {
            RemoveEmptyRows();
            var result = new StringBuilder();
            result.Append(FirstOpenQuote);
            result.Append(Translator.GetLang(language));
            result.Append(SecondOpenQuote);
            if (_translationData.Count != 0)
            {
                for (var i = 0; i < _translationData.Count - 1; i++)
                {
                    result.Append(DoubleQuote + _translationData[i].Name + DoubleQuote);
                    result.Append(Colon);
                    result.Append(DoubleQuote + _translationData[i][language] + DoubleQuote);
                    result.Append(Comma);
                }

                result.Append(DoubleQuote + _translationData[_translationData.Count - 1].Name + DoubleQuote);
                result.Append(Colon);
                result.Append(DoubleQuote + _translationData[_translationData.Count - 1][language] + DoubleQuote);
            }

            result.Append(CloseBrackets);
            return result.ToString();
        }

        public void RemoveEmptyRows()
        {
            for (var i = 0; i < _translationData.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(_translationData[i].Name))
                {
                    _translationData.Remove(_translationData[i--]);
                }
            }
        }

        private void FillDataFromJson(Lang language, JObject translation)
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
                    _translationData[index][language] = element.Value.ToString();
                }
            }
        }

        public void AddNewRow(Lang language, KeyValuePair<string, JToken> element)
        {
            var line = new TranslationDataRow(element.Key);
            line[language] = element.Value.ToString();
            _translationData.Add(line);
        }

        private int IndexOfNamedElement(string key)
        {
            for (var i = 0; i < _translationData.Count; i++)
            {
                if (_translationData[i].Name == key)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}