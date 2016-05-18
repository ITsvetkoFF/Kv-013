using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace GitHubExtension.LocalizationTool.ViewModel
{

    public abstract class NotifyProperyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool CheckPropertyChanged<T>(ref T oldValue, T newValue, Expression<Func<T>> expression)
        {
            if (oldValue == null && newValue == null)
            {
                return false;
            }

            if ((oldValue == null && newValue != null) || !oldValue.Equals(newValue))
            {
                oldValue = newValue;
                FirePropertyChanged(GetPropertyName(expression));
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
        protected string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }
    }
}
