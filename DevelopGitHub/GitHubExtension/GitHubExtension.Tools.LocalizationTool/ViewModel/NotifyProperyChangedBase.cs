using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GitHubExtension.LocalizationTool.ViewModel
{
    public abstract class NotifyProperyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool CheckPropertyChanged<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (oldValue == null && newValue == null)
            {
                return false;
            }

            if ((oldValue == null && newValue != null) || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                FirePropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        protected void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
