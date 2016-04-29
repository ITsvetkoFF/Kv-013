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

        public string this[Lang index]
        {
            get
            {
                switch (index)
                {
                    case Lang.Uk:
                        return Uk;
                    case Lang.Us:
                        return Us;
                    case Lang.Ru:
                        return Ru;
                    default:
                        return string.Empty;
                }
            }

            set
            {
                switch (index)
                {
                    case Lang.Uk:
                        Uk = value;
                        break;
                    case Lang.Us:
                        Us = value;
                        break;
                    case Lang.Ru:
                        Ru = value;
                        break;
                }
            }
        }
    }
}
