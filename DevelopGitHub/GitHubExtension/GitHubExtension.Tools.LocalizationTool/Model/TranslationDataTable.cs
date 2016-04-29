using System.Collections.ObjectModel;

namespace GitHubExtension.LocalizationTool.Model
{
    class TranslationDataTable : ITranslationData
    {
        public ObservableCollection<TranslationDataRow> TranslationTable { get; set; }
    }
}
