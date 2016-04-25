using System.Windows.Input;

namespace GitHubExtension.LocalizationTool.ViewModel
{
    public static class HotKeyCommands
    {
        public static readonly RoutedCommand AddRowRoutedCmd = new RoutedCommand();
        public static readonly RoutedCommand ClearTableRoutedCmd = new RoutedCommand();
        public static readonly RoutedCommand OpenJsonRoutedCmd = new RoutedCommand();
        public static readonly RoutedCommand SaveAsJsonRoutedCmd = new RoutedCommand();
    }
}
