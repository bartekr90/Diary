using Diary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentCommand = new RelayCommand(RefreshStudent, CanRefreshStudent);
        }
        public ICommand RefreshStudentCommand { get; set; }

        private void RefreshStudent(object obj)
        {
            MessageBox.Show("Test");
        }

        private bool CanRefreshStudent(object obj)
        {
            return true;
        }

    }
}
