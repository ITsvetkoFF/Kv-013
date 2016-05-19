using GitHubExtension.LocalizationTool.Translate;
using GitHubExtension.LocalizationTool.ViewModel;

namespace GitHubExtension.LocalizationTool.Model
{
    public class TranslationDataRow : NotifyProperyChangedBase
    {
        private string _name;

        private string _uk;

        private string _en;

        public TranslationDataRow()
        {
        }

        public TranslationDataRow(string name)
        {
            Name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                CheckPropertyChanged(() => Name);
            }
        }

        public string Uk
        {
            get
            {
                return _uk;
            }

            set
            {
                _uk = value;
                CheckPropertyChanged(() => Uk);
            }
        }

        public string En
        {
            get
            {
                return _en;
            }

            set
            {
                _en = value;
                CheckPropertyChanged(() => En);
            }
        }

        public bool IsRowEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(Name)
                    && string.IsNullOrWhiteSpace(Uk)
                    && string.IsNullOrWhiteSpace(En);
            }
        }

        public string this[Language index]
        {
            get
            {
                switch (index)
                {
                    case Language.Uk:
                        return Uk;
                    case Language.En:
                        return En;
                    default:
                        return string.Empty;
                }
            }

            set
            {
                switch (index)
                {
                    case Language.Uk:
                        Uk = value;
                        break;
                    case Language.En:
                        En = value;
                        break;
                }
            }
        }
    }
}
