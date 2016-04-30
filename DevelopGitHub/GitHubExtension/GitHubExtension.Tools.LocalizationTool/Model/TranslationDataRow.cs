using GitHubExtension.LocalizationTool.Translate;
using GitHubExtension.LocalizationTool.ViewModel;

namespace GitHubExtension.LocalizationTool.Model
{
    public class TranslationDataRow : NotifyProperyChangedBase
    {
        private string _name;

        private string _ru;

        private string _uk;

        private string _us;

        public TranslationDataRow()
        {
        }

        public TranslationDataRow(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return _name; }
            set { CheckPropertyChanged(ref _name, value); }
        }

        public string Uk
        {
            get { return _uk; }
            set { CheckPropertyChanged(ref _uk, value); }
        }

        public string Us
        {
            get { return _us; }
            set { CheckPropertyChanged(ref _us, value); }
        }

        public string Ru
        {
            get { return _ru; }
            set { CheckPropertyChanged(ref _ru, value); }
        }

        public bool IsRowEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(Name)
                    && string.IsNullOrWhiteSpace(Uk)
                    && string.IsNullOrWhiteSpace(Us)
                    && string.IsNullOrWhiteSpace(Ru);
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
                    case Language.Us:
                        return Us;
                    case Language.Ru:
                        return Ru;
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
                    case Language.Us:
                        Us = value;
                        break;
                    case Language.Ru:
                        Ru = value;
                        break;
                }
            }
        }
    }
}
