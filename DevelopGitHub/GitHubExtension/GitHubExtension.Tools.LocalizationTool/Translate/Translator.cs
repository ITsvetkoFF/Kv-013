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

        private const string UsLang = "us";

        private const string EnLang = "en";

        private const string Dash = "-";

        private const string JsonTextParameter = "text";

        private const string GetTextParameter = "&text=";

        private readonly ITranslationData _translationDataTable;

        private ObservableCollection<TranslationDataRow> TranslationData
        {
            get
            {
                return _translationDataTable.TranslationData;
            }
        }

        public Translator(ITranslationData translationDataTable)
        {
            _translationDataTable = translationDataTable;
        }

        public static Lang GetLang(string lang)
        {
            switch (lang)
            {
                case "uk":
                case "UK":
                case "uk-UK":
                case "uk-UK.json":
                    return Lang.Uk;
                case "us":
                case "US":
                case "en-Us":
                case "en-Us.json":
                    return Lang.Us;
                case "ru":
                case "RU":
                case "ru-RU":
                case "ru-RU.json":
                    return Lang.Ru;
                default:
                    throw new Exception("Incorrect Lang in Method GetLang");
            }
        }

        public static string GetLang(Lang lang)
        {
            switch (lang)
            {
                case Lang.Uk:
                    return "uk-UK";
                case Lang.Us:
                    return "en-US";
                case Lang.Ru:
                    return "ru-RU";
                default:
                    throw new Exception("Incorrect Lang in Method GetLang");
            }
        }

        public static string GetFileName(Lang lang)
        {
            switch (lang)
            {
                case Lang.Uk:
                    return "uk-UK.json";
                case Lang.Us:
                    return "en-US.json";
                case Lang.Ru:
                    return "ru-RU.json";
                default:
                    throw new Exception("Incorrect Lang in Method GetLang");
            }
        }

        public async Task WebTranslate(string sourceLanguage, string targetLanguage)
        {
            if (sourceLanguage.Equals(targetLanguage))
            {
                return;
            }

            Lang sourceLanguageEnum = GetLang(sourceLanguage);
            Lang targetLanguageEnum = GetLang(targetLanguage);
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
            return language == UsLang ? EnLang : language;
        }

        private void SaveTranslationResult(string result, Lang language)
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

        private StringBuilder GenerateTextParameter(Lang language)
        {
            var textToTranslate = new StringBuilder();
            foreach (var translation in TranslationData)
            {
                textToTranslate.Append(GetTextParameter);
                textToTranslate.Append(translation[language]);
            }

            return textToTranslate;
        }
    }
}