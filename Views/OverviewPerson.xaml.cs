﻿using Probeaufgabe_WPF.Models;
using Probeaufgabe_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Probeaufgabe_WPF.Views
{
    /// <summary>
    /// Interaction logic for OverviewPerson.xaml
    /// </summary>
    public partial class OverviewPerson : Window, INotifyPropertyChanged
    {
        public OverviewPerson()
        {
            InitializeComponent();

            MainViewModel mainViewModel = new MainViewModel();
            this.mainViewModel = mainViewModel;
            this.DataContext = mainViewModel;
        }

        private MainViewModel mainViewModel;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PersonList.Items.Filter = FilterPerson;
        }

        private bool FilterPerson(object obj)
        {
            var person = (Person)obj;

            return person.Name.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addPerson= new AddPerson(mainViewModel, this.DataContext);
            addPerson.Show();

        }

    }
}
