using Diary.Properties;
using System.ComponentModel;

namespace Diary
{
    public class DbSettings : IDataErrorInfo
    {
        private bool _isServerAdressOk;
        private bool _isServerNameOk;
        private bool _isDatabaseOk;
        private bool _isUserOk;
        private bool _isPasswordOk;
        public bool AllSettingsOk
        {
            get
            {
                return _isServerAdressOk
                && _isServerNameOk
                && _isDatabaseOk
                && _isUserOk
                && _isPasswordOk;
            }
        }
        public string ServerAdress
        {
            get { return Settings.Default.ServerAdress; }
            set { Settings.Default.ServerAdress = value; }
        }
        public string ServerName
        {
            get { return Settings.Default.ServerName; }
            set { Settings.Default.ServerName = value; }
        }
        public string Database
        {
            get { return Settings.Default.Database; }
            set { Settings.Default.Database = value; }
        }
        public string User
        {
            get { return Settings.Default.User; }
            set { Settings.Default.User = value; }
        }
        public string Password
        {
            get { return Settings.Default.Password; }
            set { Settings.Default.Password = value; }
        }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(ServerAdress):
                        if (string.IsNullOrWhiteSpace(ServerAdress))
                        {
                            Error = "Pole Adres jest wymagane.";
                            _isServerAdressOk = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerAdressOk = true;
                        }
                        break;

                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Pole Nazwa serwera jest wymagane.";
                            _isServerNameOk = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerNameOk = true;
                        }
                        break;

                    case nameof(Database):
                        if (string.IsNullOrWhiteSpace(Database))
                        {
                            Error = "Pole Nazwa bazy jest wymagane.";
                            _isDatabaseOk = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDatabaseOk = true;
                        }
                        break;

                    case nameof(User):
                        if (string.IsNullOrWhiteSpace(User))
                        {
                            Error = "Pole Login jest wymagane.";
                            _isUserOk = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserOk = true;
                        }
                        break;

                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Pole Hasło jest wymagane.";
                            _isPasswordOk = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordOk = true;
                        }
                        break;

                    default:
                        break;
                }
                return Error;
            }

        }
        public void SaveSettings()
        {
            Settings.Default.Save();
        }
    }
}
