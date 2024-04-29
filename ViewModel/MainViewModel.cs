using Probeaufgabe_WPF.Commands;
using Probeaufgabe_WPF.Models;
using Probeaufgabe_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Probeaufgabe_WPF.ViewModel
{
    public class MainViewModel
    {
        public List<Person> Persons { get; set; }

        public ICommand ShowWindowCommand { get; set; }


        public MainViewModel()
        {
            Persons = PersonManager.getPersons();
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);

        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            AddPerson addPersonWin = new AddPerson();
            addPersonWin.Show();
        }
        public void RefreshList()
        {
            Persons = PersonManager.getPersons();
        }
    }
}
