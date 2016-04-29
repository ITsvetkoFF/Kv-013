using System.Collections.ObjectModel;

namespace GitHubExtension.LocalizationTool.Model
{
    public interface ITranslationData
    {
        ObservableCollection<TranslationDataRow> TranslationTable { get; set; }
    }
}
