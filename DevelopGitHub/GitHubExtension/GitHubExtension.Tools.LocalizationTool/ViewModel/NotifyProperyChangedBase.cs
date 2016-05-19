using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace GitHubExtension.LocalizationTool.ViewModel
{
    public abstract class NotifyProperyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void CheckPropertyChanged<T>(Expression<Func<T>> expression)
        {
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            string propertyName = memberExpression.Member.Name;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
