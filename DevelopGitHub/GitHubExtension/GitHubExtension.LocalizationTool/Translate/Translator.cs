using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        private readonly MainWindow mainWindow;

        public Translator(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        private ObservableCollection<TranslationDataRow> TranslationData
        {
            get
            {
                return mainWindow.TranslationData;
            }
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

        public void AddNewRow(Lang language, KeyValuePair<string, JToken> element)
        {
            var line = new TranslationDataRow(element.Key);
            line[language] = element.Value.ToString();
            TranslationData.Add(line);
        }

        public async void WebTranslate(string sourceLanguage, string targetLanguage)
        {
            if (sourceLanguage.Equals(targetLanguage))
            {
                return;
            }

            Lang sourceLanguageEnum;
            Lang targetLanguageEnum;
            LanguageCheck(ref sourceLanguage, ref targetLanguage, out sourceLanguageEnum, out targetLanguageEnum);

            StringBuilder textToTranslate = GenerateTextParameter(sourceLanguageEnum);

            var result = Task<JObject>.Factory.StartNew(() => GetTranslationFromYandexApi(sourceLanguage, targetLanguage, textToTranslate));
            mainWindow.TranslateButton.IsEnabled = false;
            mainWindow.TranslateButton.Content = "Translating...";
            await result;
            mainWindow.TranslateButton.IsEnabled = true;
            mainWindow.TranslateButton.Content = "WebTranslate";

            if (result.Result == null)
            {
                return;
            }

            SaveTranslationResult(result, targetLanguageEnum);
        }

        public void RemoveEmptyRows()
        {
            for (var i = 0; i < TranslationData.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(TranslationData[i].Name))
                {
                    TranslationData.Remove(TranslationData[i--]);
                }
            }
        }

        private static void LanguageCheck(ref string sourceLanguage, ref string targetLanguage, out Lang sourceLanguageEnum, out Lang targetLanguageEnum)
        {
            sourceLanguageEnum = GetLang(sourceLanguage);
            targetLanguageEnum = GetLang(targetLanguage);
            sourceLanguage = sourceLanguage == UsLang ? EnLang : sourceLanguage;
            targetLanguage = targetLanguage == UsLang ? EnLang : targetLanguage;
        }

        private static JObject GetTranslationFromYandexApi(
            string sourceLanguage,
            string targetLanguage,
            StringBuilder textToTranslate)
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };
            try
            {
                var result =
                    JObject.Parse(
                        webClient.DownloadString(
                            YandexTranslateApiUrl + sourceLanguage + Dash + targetLanguage + textToTranslate));
                return result;
            }
            catch (WebException exception)
            {
                MainWindow.ShowErrorMessageBox("Истекло время ожидания ответа сервера", exception);
            }

            return null;
        }

        private void SaveTranslationResult(Task<JObject> result, Lang language)
        {
            var i = 0;
            foreach (var item in result.Result.GetValue(JsonTextParameter))
            {
                TranslationData[i][language] = item.ToString();
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