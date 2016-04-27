using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

using GalaSoft.MvvmLight;

using GitHubExtension.LocalizationTool.Model;
using GitHubExtension.LocalizationTool.Translate;

using Newtonsoft.Json;

namespace GitHubExtension.LocalizationTool.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Translator _translator;

        private readonly JsonHelper _jsonHelper;

        private string _targetLanguageText;

        private string _sourceLanguageText;

        private bool _translateButtonIsEnabled = true;

        private string _translateButtonContent = "Web Translate";

        public MainWindowViewModel()
        {
            TranslationData = new ObservableCollection<TranslationDataRow>();

            _translator = new Translator(TranslationData);

            _jsonHelper = new JsonHelper(TranslationData);

            OpenJson(null);
        }

        public ObservableCollection<TranslationDataRow> TranslationData { get; set; }

        public Translator Translator
        {
            get
            {
                return _translator;
            }
        }

        public JsonHelper JsonHelper
        {
            get
            {
                return _jsonHelper;
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

        private DelegateCommand addEmptyRowCommand;

        public ICommand AddEmptyRowCommand
        {
            get
            {
                if (addEmptyRowCommand == null)
                {
                    addEmptyRowCommand = new DelegateCommand(AddEmptyRow);
                }

                return addEmptyRowCommand;
            }
        }

        private void AddEmptyRow(object param)
        {
            TranslationData.Add(new TranslationDataRow());
        }

        private DelegateCommand saveJsonCommand;

        public ICommand SaveJsonCommand
        {
            get
            {
                if (saveJsonCommand == null)
                {
                    saveJsonCommand = new DelegateCommand(SaveJson);
                }

                return saveJsonCommand;
            }
        }

        private void SaveJson(object param)
        {
            JsonHelper.RemoveEmptyRows();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                File.WriteAllText(Translator.GetFileName(value), JsonHelper.GenerateJson(value));
            }

            ShowInformationMessageBox("Saved!");
        }

        private DelegateCommand openJsonCommand;

        public ICommand OpenJsonCommand
        {
            get
            {
                if (openJsonCommand == null)
                {
                    openJsonCommand = new DelegateCommand(OpenJson);
                }

                return openJsonCommand;
            }
        }

        private void OpenJson(object param)
        {
            TranslationData.Clear();
            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                try
                {
                    JsonHelper.ReadJsonFromFile(value);
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

            JsonHelper.RemoveEmptyRows();
            ShowInformationMessageBox("Oppened!");
        }

        private DelegateCommand clearTranslationDataCommand;

        public ICommand ClearTranslationDataCommand
        {
            get
            {
                if (clearTranslationDataCommand == null)
                {
                    clearTranslationDataCommand = new DelegateCommand(ClearTranslationData);
                }

                return clearTranslationDataCommand;
            }
        }

        private void ClearTranslationData(object param)
        {
            TranslationData.Clear();
        }

        private DelegateCommand translatingButtonCommand;

        public ICommand TranslatingButtonCommand
        {
            get
            {
                if (translatingButtonCommand == null)
                {
                    translatingButtonCommand = new DelegateCommand(TranslatingButton);
                }

                return translatingButtonCommand;
            }
        }

        private async void TranslatingButton(object param)
        {
            TranslateButtonIsEnabled = false;
            TranslateButtonContent = "Translating...";
            try
            {
                await Translator.WebTranslate(SourceLanguageText, TargetLanguageText);
            }
            catch (WebException exception)
            {
                ShowErrorMessageBox("The waiting time of server response has expired", exception);
            }

            TranslateButtonIsEnabled = true;
            TranslateButtonContent = "WebTranslate";
        }

        private DelegateCommand closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new DelegateCommand(Close);
                }

                return closeCommand;
            }
        }

        private void Close(object param)
        {
            Application.Current.MainWindow.Close();
        }
    }
}