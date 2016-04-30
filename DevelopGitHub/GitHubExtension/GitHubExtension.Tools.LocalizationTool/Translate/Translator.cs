using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.LocalizationTool.Model;

using Newtonsoft.Json.Linq;

namespace GitHubExtension.LocalizationTool.Translate
{
    public class Translator
    {
        public const string YandexTranslateApiUrl =
            "https://translate.yandex.net/api/v1.5/tr.json/translate?" +
            "key=trnsl.1.1.20160322T103501Z.10b2b142f2f8bf7f.66c4f9f75232ede5cb9d8cc5ce17df5fd1d02d32&" +
            "lang=";

        private const string UsLanguage = "us";

        private const string EnLanguage = "en";

        private const string Dash = "-";

        private const string JsonTextParameter = "text";

        private const string GetTextParameter = "&text=";

        private readonly ITranslationData _translationDataTable;

        private ObservableCollection<TranslationDataRow> TranslationData
        {
            get
            {
                return _translationDataTable.TranslationTable;
            }
        }

        public Translator(ITranslationData translationDataTable)
        {
            _translationDataTable = translationDataTable;
        }

        public async Task WebTranslate(string sourceLanguage, string targetLanguage)
        {
            if (sourceLanguage.Equals(targetLanguage))
            {
                return;
            }

            Language sourceLanguageEnum = sourceLanguage.GetLanguageEnum();
            Language targetLanguageEnum = targetLanguage.GetLanguageEnum();
            sourceLanguage = CheckUsEnLanguage(sourceLanguage);
            targetLanguage = CheckUsEnLanguage(targetLanguage);
            StringBuilder textToTranslate = GenerateTextParameter(sourceLanguageEnum);
            string result;
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                result = await webClient.DownloadStringTaskAsync(
                                YandexTranslateApiUrl + sourceLanguage + Dash + targetLanguage + textToTranslate);
            }

            SaveTranslationResult(result, targetLanguageEnum);
        }

        private static string CheckUsEnLanguage(string language)
        {
            return language == UsLanguage ? EnLanguage : language;
        }

        private void SaveTranslationResult(string result, Language language)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }

            var jsonResult = JObject.Parse(result);
            var i = 0;
            var jsonResultObjects = jsonResult.GetValue(JsonTextParameter).Select(item => item.ToString());
            foreach (var value in jsonResultObjects)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    TranslationData[i][language] = value;
                }

                i++;
            }
        }

        private StringBuilder GenerateTextParameter(Language language)
        {
            var textToTranslate = new StringBuilder();
            foreach (var translationRow in TranslationData)
            {
                textToTranslate.Append(GetTextParameter);
                textToTranslate.Append(translationRow[language]);
            }

            return textToTranslate;
        }
    }
}