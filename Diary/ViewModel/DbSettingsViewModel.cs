using Diary.Commands;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModel
{
    class DbSettingsViewModel : ViewModelBase
    {

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private bool _serverIsNotConnected;

        public bool ServerIsNotConnected
        {
            get { return _serverIsNotConnected; }
            set
            {
                _serverIsNotConnected = value;
                OnPropertyChanged();
            }
        }

        private DbSettings _dbSettings;

        public DbSettings DbSettings
        {
            get { return _dbSettings; }
            set
            {
                _dbSettings = value;
                OnPropertyChanged();
            }
        }
        public DbSettingsViewModel(bool isServerConnected)
        {
            _serverIsNotConnected = !isServerConnected;
            _dbSettings = new DbSettings();
            CloseCommand = new RelayCommand(Close, CanClose);
            ConfirmCommand = new RelayCommand(Save, CanSave);
        }

        private bool CanClose(object obj)
        {
            return CanCloseApp();
        }

        private bool CanCloseApp()
        {
            if (ServerIsNotConnected)
                return false;
            else
                return true;
        }

        private void Save(object obj)
        {
            SaveSettings();
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        void SaveSettings()
        {
            if (!DbSettings.AllSettingsOk)
                return;
            DbSettings.SaveSettings();
        }
        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
        private bool CanSave(object obj)
        {
            return CanSaveSettings();
        }
        private bool CanSaveSettings()
        {
            if (DbSettings.AllSettingsOk)
                return true;
            else
                return false;
        }
    }
}
