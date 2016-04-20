using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GitHubExtension.LocalizationTool.Translate
{
    public class JsonHelper
    {
        private const string DoubleQuote = "\"";

        private const string CloseBrackets = "}}";

        private const string Ñolon = ":";

        private const string Comma = ",";

        private const string FirstOpenQuote = "{\"";

        private const string SecondOpenQuote = "\":{";

        private readonly MainWindow mainWindow;

        public JsonHelper(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            foreach (Lang value in Enum.GetValues(typeof(Lang)))
            {
                ReadJsonFromFile(value);
            }
        }

        private ObservableCollection<TranslationDataRow> TranslationData
        {
            get
            {
                return mainWindow.TranslationData;
            }
        }

        private Translator Translator
        {
            get
            {
                return mainWindow.Translator;
            }
        }

        public void ReadJsonFromFile(Lang language)
        {
            var fileName = Translator.GetFileName(language);
            try
            {
                var fileText = File.ReadAllText(fileName);
                var file = JObject.Parse(fileText);
                var translation = file.Value<JObject>(Translator.GetLang(language));
                IterateJsonArray(language, translation);
            }
            catch (JsonReaderException jsonEx)
            {
                MainWindow.ShowErrorMessageBox("Incorrect json format", jsonEx);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                MainWindow.ShowErrorMessageBox("File is missing", fileNotFoundException);
            }
            catch (Exception exception)
            {
                MainWindow.ShowErrorMessageBox("Unknown error", exception);
            }
        }

        public string GenerateJson(Lang language)
        {
            Translator.RemoveEmptyRows();
            var result = new StringBuilder();
            result.Append(FirstOpenQuote);
            result.Append(Translator.GetLang(language));
            result.Append(SecondOpenQuote);
            if (TranslationData.Count != 0)
            {
                for (var i = 0; i < TranslationData.Count - 1; i++)
                {
                    result.Append(DoubleQuote + TranslationData[i].Name + DoubleQuote);
                    result.Append(Ñolon);
                    result.Append(DoubleQuote + TranslationData[i][language] + DoubleQuote);
                    result.Append(Comma);
                }

                result.Append(DoubleQuote + TranslationData[TranslationData.Count - 1].Name + DoubleQuote);
                result.Append(Ñolon);
                result.Append(DoubleQuote + TranslationData[TranslationData.Count - 1][language] + DoubleQuote);
            }

            result.Append(CloseBrackets);
            return result.ToString();
        }

        private void IterateJsonArray(Lang language, JObject translation)
        {
            foreach (var element in translation)
            {
                var index = IndexOfNamedElement(element.Key);
                if (index == -1)
                {
                    Translator.AddNewRow(language, element);
                }
                else
                {
                    TranslationData[index][language] = element.Value.ToString();
                }
            }
        }

        private int IndexOfNamedElement(string key)
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
    }
}