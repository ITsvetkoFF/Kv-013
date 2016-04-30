using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

using GitHubExtension.LocalizationTool.Helper;
using GitHubExtension.LocalizationTool.Model;
using GitHubExtension.LocalizationTool.Translate;

using Newtonsoft.Json;

namespace GitHubExtension.LocalizationTool.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ITranslationData _translationDataTable;

        private readonly Translator _translator;

        private string _targetLanguageText;

        private string _sourceLanguageText;

        private bool _translateButtonIsEnabled = true;

        private string _translateButtonContent = "Web Translate";

        public MainWindowViewModel()
        {
            RegisterCommands();

            _translationDataTable = new TranslationDataTable
                {
                    TranslationTable = new ObservableCollection<TranslationDataRow>()
                };

            _translator = new Translator(_translationDataTable);

            OpenJson();
        }

        public ICommand CloseCommand { get; set; }

        public ICommand TranslatingButtonCommand { get; set; }

        public ICommand AddEmptyRowCommand { get; set; }

        public ICommand SaveJsonCommand { get; set; }

        public ICommand OpenJsonCommand { get; set; }

        public ICommand ClearTranslationDataCommand { get; set; }

        public ObservableCollection<TranslationDataRow> TranslationData
        {
            get
            {
                return _translationDataTable.TranslationTable;
            }

            set
            {
                _translationDataTable.TranslationTable = value;
            }
        }

        public string TargetLanguageText
        {
            get
            {
                return _targetLanguageText;
            }

            set
            {
                _targetLanguageText = value;
                RaisePropertyChanged(() => TargetLanguageText);
            }
        }

        public string SourceLanguageText
        {
            get
            {
                return _sourceLanguageText;
            }

            set
            {
                _sourceLanguageText = value;
                RaisePropertyChanged(() => SourceLanguageText);
            }
        }

        public string TranslateButtonContent
        {
            get
            {
                return _translateButtonContent;
            }

            set
            {
                _translateButtonContent = value;
                RaisePropertyChanged(() => TranslateButtonContent);
            }
        }

        public bool TranslateButtonIsEnabled
        {
            get
            {
                return _translateButtonIsEnabled;
            }

            set
            {
                _translateButtonIsEnabled = value;
                RaisePropertyChanged(() => TranslateButtonIsEnabled);
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

        private void RegisterCommands()
        {
            CloseCommand = new RelayCommand(Close);
            TranslatingButtonCommand = new RelayCommand(WebTranslate);
            AddEmptyRowCommand = new RelayCommand(AddEmptyRow);
            SaveJsonCommand = new RelayCommand(SaveJson);
            OpenJsonCommand = new RelayCommand(OpenJson);
            ClearTranslationDataCommand = new RelayCommand(ClearTranslationData);
        }

        private void AddEmptyRow()
        {
            TranslationData.Add(new TranslationDataRow());
        }

        private void SaveJson()
        {
            _translationDataTable.RemoveEmptyRows();
            foreach (Language value in Enum.GetValues(typeof(Language)))
            {
                File.WriteAllText(value.GetFileName(), _translationDataTable.GenerateJsonFromData(value));
            }

            ShowInformationMessageBox("Saved!");
        }

        private void OpenJson()
        {
            TranslationData.Clear();
            foreach (Language value in Enum.GetValues(typeof(Language)))
            {
                try
                {
                    _translationDataTable.ReadJsonFromFile(value);
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
                }
            }

            _translationDataTable.RemoveEmptyRows();
            ShowInformationMessageBox("Oppened!");
        }

        private void ClearTranslationData()
        {
            TranslationData.Clear();
        }

        private async void WebTranslate()
        {
            TranslateButtonIsEnabled = false;
            TranslateButtonContent = "Translating...";
            try
            {
                await _translator.WebTranslate(SourceLanguageText, TargetLanguageText);
            }
            catch (WebException exception)
            {
                ShowErrorMessageBox("The waiting time of server response has expired", exception);
            }

            TranslateButtonIsEnabled = true;
            TranslateButtonContent = "WebTranslate";
        }
        
        private void Close()
        {
            Application.Current.Shutdown();
        }
    }
}