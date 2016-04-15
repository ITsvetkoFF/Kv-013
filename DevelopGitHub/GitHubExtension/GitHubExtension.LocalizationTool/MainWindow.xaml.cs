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

            Translations = new ObservableCollection<Translation>();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                ReadJson(value);
            }
        }

        public ObservableCollection<Translation> Translations { get; set; }

        private static void ShowErrorMessageBox(string error, Exception exception)
        {
            MessageBox.Show(error + "\n" + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void ShowInformationMessageBox(string information)
        {
            MessageBox.Show(information, "OK", MessageBoxButton.OK, MessageBoxImage.Information);
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
            Table.ItemsSource = Translations;
        }

        private void AddRow(object sender, RoutedEventArgs e)
        {
            Translations.Add(new Translation());
        }

        private void SaveJson(object sender, RoutedEventArgs e)
        {
            RemoveEmptyRows();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                File.WriteAllText(GetFileName(value), BuildJson(value));
            }

            ShowInformationMessageBox("Saved!");
        }

        private void OpenJson(object sender, RoutedEventArgs e)
        {
            Translations.Clear();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                ReadJson(value);
            }

            RemoveEmptyRows();
            ShowInformationMessageBox("Oppened!");
        }

        private void ReadJson(Lang language)
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
                    AddNewLineToTranslation(language, element);
                }
                else
                {
                    Translations[index][language] = element.Value.ToString();
                }
            }
        }

        private void AddNewLineToTranslation(Lang language, KeyValuePair<string, JToken> element)
        {
            var line = new Translation(element.Key);
            line[language] = element.Value.ToString();
            Translations.Add(line);
        }

        private string BuildJson(Lang language)
        {
            RemoveEmptyRows();
            var result = new StringBuilder();
            result.Append("{\"");
            result.Append(GetLang(language));
            result.Append("\":{");
            if (Translations.Count != 0)
            {
                for (var i = 0; i < Translations.Count - 1; i++)
                {
                    result.Append("\"" + Translations[i].Name + "\"");
                    result.Append(":");
                    result.Append("\"" + Translations[i][language] + "\"");
                    result.Append(",");
                }

                result.Append("\"" + Translations[Translations.Count - 1].Name + "\"");
                result.Append(":");
                result.Append("\"" + Translations[Translations.Count - 1][language] + "\"");
            }

            result.Append("}}");
            return result.ToString();
        }

        private void ClearTable(object sender, RoutedEventArgs e)
        {
            Translations.Clear();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Translate(string sourceLanguage, string targetLanguage)
        {
            if (sourceLanguage.Equals(targetLanguage))
            {
                return;
            }

            var sourceLanguageEnum = GetLang(sourceLanguage);
            var targetLanguageEnum = GetLang(targetLanguage);

            var textToTranslate = TextParameterForTranslate(sourceLanguageEnum);

            sourceLanguage = sourceLanguage == "us" ? "en" : sourceLanguage;
            targetLanguage = targetLanguage == "us" ? "en" : targetLanguage;

            var result = GetTranslationFromYandexApi(sourceLanguage, targetLanguage, textToTranslate);

            if (result == null)
            {
                return;
            }

            var i = 0;
            foreach (var item in result.GetValue("text"))
            {
                Translations[i][targetLanguageEnum] = item.ToString();
                i++;
            }
        }

        private StringBuilder TextParameterForTranslate(Lang sourceLanguageEnum)
        {
            var textToTranslate = new StringBuilder();
            foreach (var translation in Translations)
            {
                textToTranslate.Append("&text=");
                textToTranslate.Append(translation[sourceLanguageEnum]);
            }

            return textToTranslate;
        }

        private void TranslatingButton(object sender, RoutedEventArgs e)
        {
            var sourceLanguage = SourceLanguage.Text;
            var targetLanguage = TargetLanguage.Text;
            Translate(sourceLanguage, targetLanguage);
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
            for (var i = 0; i < Translations.Count; i++)
            {
                if (Translations[i].Name == key)
                {
                    return i;
                }
            }

            return -1;
        }

        private void RemoveEmptyRows()
        {
            for (var i = 0; i < Translations.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(Translations[i].Name))
                {
                    Translations.Remove(Translations[i--]);
                }
            }
        }
    }
}
