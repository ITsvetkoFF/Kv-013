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
                MessageBox.Show("\n" + exception.Message);
            }
        }

        public string GenerateJson(Lang language)
        {
            Translator.RemoveEmptyDataRows();
            var result = new StringBuilder();
            result.Append("{\"");
            result.Append(Translator.GetLang(language));
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

        private void IterateJsonArray(Lang language, JObject translation)
        {
            foreach (var element in translation)
            {
                var index = Contains(element.Key);
                if (index == -1)
                {
                    Translator.AddNewRowToTranslationData(language, element);
                }
                else
                {
                    TranslationData[index][language] = element.Value.ToString();
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
    }
}