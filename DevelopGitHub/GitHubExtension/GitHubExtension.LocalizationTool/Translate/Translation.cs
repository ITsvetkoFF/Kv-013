﻿namespace GitHubExtension.LocalizationTool
{
    public class Translation : NotifyProperyChangedBase
    {
        private string _name;

        private string _ru;

        private string _uk;

        private string _us;

        public Translation()
        {
        }

        public Translation(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return _name; }
            set { CheckPropertyChanged("Name", ref _name, ref value); }
        }

        public string Uk
        {
            get { return _uk; }
            set { CheckPropertyChanged("Uk", ref _uk, ref value); }
        }

        public string Us
        {
            get { return _us; }
            set { CheckPropertyChanged("Us", ref _us, ref value); }
        }

        public string Ru
        {
            get { return _ru; }
            set { CheckPropertyChanged("Ru", ref _ru, ref value); }
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
