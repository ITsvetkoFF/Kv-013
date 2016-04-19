using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.LocalizationTool
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string YandexTranslateApiUrl =
            "https://translate.yandex.net/api/v1.5/tr.json/translate?" +
            "key=trnsl.1.1.20160322T103501Z.10b2b142f2f8bf7f.66c4f9f75232ede5cb9d8cc5ce17df5fd1d02d32&" +
            "lang=";

        public MainWindow()
        {
            InitializeComponent();

            TranslationData = new ObservableCollection<TranslationDataRow>();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                ReadJsonFromFile(value);
            }
        }

        public ObservableCollection<TranslationDataRow> TranslationData { get; set; }

        private static void ShowErrorMessageBox(string error, Exception exception)
        {
            MessageBox.Show(error + "\n" + exception.Message, "Error - Localization Tool", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void ShowInformationMessageBox(string information)
        {
            MessageBox.Show(information, "OK - Localization Tool", MessageBoxButton.OK, MessageBoxImage.Information);
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
                            YandexTranslateApiUrl + sourceLanguage + "-" + targetLanguage + textToTranslate));
                return result;
            }
            catch (WebException exception)
            {
                ShowErrorMessageBox("Истекло время ожидания ответа сервера", exception);
            }

            return null;
        }

        private void DataGridLoaded(object sender, RoutedEventArgs e)
        {
            Table.ItemsSource = TranslationData;
        }

        private void AddEmptyDataRow(object sender, RoutedEventArgs e)
        {
            TranslationData.Add(new TranslationDataRow());
        }

        private void SaveJson(object sender, RoutedEventArgs e)
        {
            RemoveEmptyDataRows();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                File.WriteAllText(GetFileName(value), GenerateJson(value));
            }

            ShowInformationMessageBox("Saved!");
        }

        private void OpenJson(object sender, RoutedEventArgs e)
        {
            TranslationData.Clear();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                ReadJsonFromFile(value);
            }

            RemoveEmptyDataRows();
            ShowInformationMessageBox("Oppened!");
        }

        private void ReadJsonFromFile(Lang language)
        {
            var fileName = GetFileName(language);
            try
            {
                var fileText = File.ReadAllText(fileName);
                var file = JObject.Parse(fileText);
                var translation = file.Value<JObject>(GetLang(language));
                IterateJsonArray(language, translation);
            }
            catch (JsonReaderException jsonEx)
            {
                ShowErrorMessageBox("Incorrect json format", jsonEx);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                ShowErrorMessageBox("File is missing", fileNotFoundException);
            }
            catch (Exception exception)
            {
                ShowErrorMessageBox("Unknown error", exception);
                MessageBox.Show("\n" + exception.Message);
            }
        }

        private void IterateJsonArray(Lang language, JObject translation)
        {
            foreach (var element in translation)
            {
                var index = Contains(element.Key);
                if (index == -1)
                {
                    AddNewRowToTranslationData(language, element);
                }
                else
                {
                    TranslationData[index][language] = element.Value.ToString();
                }
            }
        }

        private void AddNewRowToTranslationData(Lang language, KeyValuePair<string, JToken> element)
        {
            var line = new TranslationDataRow(element.Key);
            line[language] = element.Value.ToString();
            TranslationData.Add(line);
        }

        private string GenerateJson(Lang language)
        {
            RemoveEmptyDataRows();
            var result = new StringBuilder();
            result.Append("{\"");
            result.Append(GetLang(language));
            result.Append("\":{");
            if (TranslationData.Count != 0)
            {
                for (var i = 0; i < TranslationData.Count - 1; i++)
                {
                    result.Append("\"" + TranslationData[i].Name + "\"");
                    result.Append(":");
                    result.Append("\"" + TranslationData[i][language] + "\"");
                    result.Append(",");
                }

                result.Append("\"" + TranslationData[TranslationData.Count - 1].Name + "\"");
                result.Append(":");
                result.Append("\"" + TranslationData[TranslationData.Count - 1][language] + "\"");
            }

            result.Append("}}");
            return result.ToString();
        }

        private void ClearTranslationData(object sender, RoutedEventArgs e)
        {
            TranslationData.Clear();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void WebTranslate(string sourceLanguage, string targetLanguage)
        {
            if (sourceLanguage.Equals(targetLanguage))
            {
                return;
            }

            Lang sourceLanguageEnum;
            Lang targetLanguageEnum;
            TargetLanguageEnum(ref sourceLanguage, ref targetLanguage, out sourceLanguageEnum, out targetLanguageEnum);
            
            StringBuilder textToTranslate = GenerateTextParameter(sourceLanguageEnum);
            
            var result = Task<JObject>.Factory.StartNew(() => GetTranslationFromYandexApi(sourceLanguage, targetLanguage, textToTranslate));
            TranslateButton.IsEnabled = false;
            TranslateButton.Content = "Translating...";
            await result;
            TranslateButton.IsEnabled = true;
            TranslateButton.Content = "WebTranslate";

            if (result.Result == null)
            {
                return;
            }

            SaveTranslationResult(result, targetLanguageEnum);
        }

        private void SaveTranslationResult(Task<JObject> result, Lang language)
        {
            var i = 0;
            foreach (var item in result.Result.GetValue("text"))
            {
                TranslationData[i][language] = item.ToString();
                i++;
            }
        }

        private void TargetLanguageEnum(ref string sourceLanguage, ref string targetLanguage, out Lang sourceLanguageEnum, out Lang targetLanguageEnum)
        {
            sourceLanguageEnum = GetLang(sourceLanguage);
            targetLanguageEnum = GetLang(targetLanguage);
            sourceLanguage = sourceLanguage == "us" ? "en" : sourceLanguage;
            targetLanguage = targetLanguage == "us" ? "en" : targetLanguage;
        }

        private StringBuilder GenerateTextParameter(Lang language)
        {
            var textToTranslate = new StringBuilder();
            foreach (var translation in TranslationData)
            {
                textToTranslate.Append("&text=");
                textToTranslate.Append(translation[language]);
            }

            return textToTranslate;
        }

        private void TranslatingButton(object sender, RoutedEventArgs e)
        {
            var sourceLanguage = SourceLanguage.Text;
            var targetLanguage = TargetLanguage.Text;
            WebTranslate(sourceLanguage, targetLanguage);
        }

        private Lang GetLang(string lang)
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

        private string GetLang(Lang lang)
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

        private string GetFileName(Lang lang)
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

        private void LanguageChange(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            foreach (var column in Table.Columns)
            {
                if (column.Header.ToString().ToLower() == cb.Name.ToLower())
                {
                    column.Visibility = column.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        private int Contains(string key)
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

        private void RemoveEmptyDataRows()
        {
            for (var i = 0; i < TranslationData.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(TranslationData[i].Name))
                {
                    TranslationData.Remove(TranslationData[i--]);
                }
            }
        }
    }
}
