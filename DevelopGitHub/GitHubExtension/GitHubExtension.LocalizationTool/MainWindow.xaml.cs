using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using GitHubExtension.LocalizationTool.Translate;

namespace GitHubExtension.LocalizationTool
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Translator translator;

        private readonly JsonHelper jsonHelper;

        public MainWindow()
        {
            InitializeComponent();

            TranslationData = new ObservableCollection<TranslationDataRow>();

            translator = new Translator(this);

            jsonHelper = new JsonHelper(this);
        }

        public ObservableCollection<TranslationDataRow> TranslationData { get; set; }

        public Translator Translator
        {
            get
            {
                return translator;
            }
        }

        public JsonHelper JsonHelper
        {
            get
            {
                return jsonHelper;
            }
        }

        public static void ShowErrorMessageBox(string error, Exception exception)
        {
            MessageBox.Show(
                error + Environment.NewLine + exception.Message, 
                "Error - Localization Tool", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }

        public static void ShowInformationMessageBox(string information)
        {
            MessageBox.Show(information, "OK - Localization Tool", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DataGridLoaded(object sender, RoutedEventArgs e)
        {
            Table.ItemsSource = TranslationData;
        }

        private void AddEmptyRow(object sender, RoutedEventArgs e)
        {
            TranslationData.Add(new TranslationDataRow());
        }

        private void SaveJson(object sender, RoutedEventArgs e)
        {
            Translator.RemoveEmptyRows();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                File.WriteAllText(Translator.GetFileName(value), JsonHelper.GenerateJson(value));
            }

            ShowInformationMessageBox("Saved!");
        }

        private void OpenJson(object sender, RoutedEventArgs e)
        {
            TranslationData.Clear();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                JsonHelper.ReadJsonFromFile(value);
            }

            Translator.RemoveEmptyRows();
            ShowInformationMessageBox("Oppened!");
        }

        private void ClearTranslationData(object sender, RoutedEventArgs e)
        {
            TranslationData.Clear();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void TranslatingButton(object sender, RoutedEventArgs e)
        {
            TranslateButton.IsEnabled = false;
            TranslateButton.Content = "Translating...";
            var sourceLanguage = SourceLanguage.Text;
            var targetLanguage = TargetLanguage.Text;
            await Translator.WebTranslate(sourceLanguage, targetLanguage);
            TranslateButton.IsEnabled = true;
            TranslateButton.Content = "WebTranslate";
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
    }
}
