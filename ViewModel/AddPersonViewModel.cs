﻿using Microsoft.Win32;
using Probeaufgabe_WPF.Commands;
using Probeaufgabe_WPF.Data;
using Probeaufgabe_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Probeaufgabe_WPF.ViewModel
{
    internal class AddPersonViewModel :INotifyPropertyChanged
    {

        PersonEntities personEntities;
        private const bool V = true;
        public AddPersonViewModel()
        {
            personEntities = new PersonEntities();
            AddPersonCommand = new Command((s) => V, AddPerson);
            UploadPictureCommand = new Command((s) => V, UploadPicture);
        }

        public void UploadPicture(object obj)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*png";
            openDialog.FilterIndex = 1;
            if (openDialog.ShowDialog() == true)
            {
                Uri uri = new Uri(openDialog.FileName);
                Person.Picture = uri.ToString();
                ImageSource = new BitmapImage(uri);
            }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                onPropertyChanged(nameof(ImageSource));
            }
        }

        private void AddPerson(object obj)
        {
            personEntities.Person.Add(Person);
            personEntities.SaveChanges();
            Person = new Person();
        }

        public event EventHandler DialogClosed;
        public void OnDialogClosed()
        {
            DialogClosed?.Invoke(this, EventArgs.Empty);
        }
        private Person person = new Person();

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                onPropertyChanged(nameof(Person));
            }
        }

        public ICommand AddPersonCommand { get; set; }
        public ICommand UploadPictureCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}