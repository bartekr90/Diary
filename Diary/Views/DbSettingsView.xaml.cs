using Diary.ViewModel;
using MahApps.Metro.Controls;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for DbSettingsView.xaml
    /// </summary>
    public partial class DbSettingsView : MetroWindow
    {
        public DbSettingsView(bool isConncetionOk)
        {
            InitializeComponent();
            DataContext = new DbSettingsViewModel(isConncetionOk);
        }
    }
}
