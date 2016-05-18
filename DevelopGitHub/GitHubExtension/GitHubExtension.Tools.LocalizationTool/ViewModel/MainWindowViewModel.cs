using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

        private bool _translateButtonIsEnabled = true;

        private string _translateButtonContent = "Web Translate";

        private Language _sourceLanguage;

        private Language _targetLanguage;

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

        public bool IsChecked
        {
            get;
            set;
        }

        public Language SourceLanguage
        {
            get
            {
                return _sourceLanguage;
            }

            set
            {
                _sourceLanguage = value;
                RaisePropertyChanged(() => SourceLanguage);
            }
        }

        public Language TargetLanguage
        {
            get
            {
                return _targetLanguage;
            }

            set
            {
                _targetLanguage = value;
                RaisePropertyChanged(() => TargetLanguage);
            }
        }

        public IEnumerable<Language> LanguageEnumValues
        {
            get
            {
                return Enum.GetValues(typeof(Language))
                    .Cast<Language>();
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
                await _translator.PerformWebTranslation(_sourceLanguage, _targetLanguage);
            }
            catch (WebException exception)
            {
                ShowErrorMessageBox("The waiting time of server response has expired", exception);
            }

            TranslateButtonIsEnabled = true;
            TranslateButtonContent = "Web Translate";
        }
        
        private void Close()
        {
            Application.Current.Shutdown();
        }
    }
}