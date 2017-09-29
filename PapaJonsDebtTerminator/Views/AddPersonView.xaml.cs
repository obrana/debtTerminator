﻿using System;
using System.Collections.Generic;
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
using DbLayer;
using Models;

namespace PapaJonsDebtTerminator.Views
{
    /// <summary>
    /// Interaction logic for AddPersonView.xaml
    /// </summary>
    public partial class AddPersonView : UserControl
    {
        private DbLayer.DbLayer database;
        public AddPersonView()
        {
            InitializeComponent();

            InitView();
        }

        private void InitView()
        {
            database = DbLayer.DbLayer.Database;
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            var person = new Person()
            {
                Address = TxtAddress.Text,
                CPR = TxtCpr.Text,
                DOB = DateTime.Parse(TxtDOB.Text),
                Email = TxtEmail.Text,
                Gender = TxtGender.Text,
                Name = TxtName.Text,
                Phone = TxtPhone.Text
            };

            database.AddPerson(person);
        }
    }
}