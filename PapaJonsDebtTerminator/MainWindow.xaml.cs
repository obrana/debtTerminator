using System;
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
using Models;
using PapaJonsDebtTerminator.Views;

namespace PapaJonsDebtTerminator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DbLayer.DbLayer database;
        public MainWindow()
        {
            InitializeComponent();

            InitView();
        }
        private void InitView()
        {
            database = DbLayer.DbLayer.Database;

            AddPersonView.MainWindow = this;
            
            LoadPersons();
        }

        public void LoadPersons()
        {
            DataGridPersons.ItemsSource = database.GetPersons();
            if (DataGridPersons.Items.Count > 0)
            {
                DataGridPersons.SelectedItem = DataGridPersons.Items[0];
                LoadDebtOfPerson(DataGridPersons.SelectedItem as Person);
            }
        }
        public void LoadDebtOfPerson(Person person)
        {
            DataGridDebts.ItemsSource = database.GetDebtsOfPerson(person);
            if (DataGridDebts.Items.Count > 0)
            {
                DataGridDebts.SelectedItem = DataGridDebts.Items[0];
                AddDebtView.FillUpDebt(DataGridDebts.Items[0] as Debt);
            }
        }

        private void DataGridPersons_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridPersons.SelectedItem != null)
            {
                var person = (DataGridPersons.SelectedItem as Person);
                if(person==null)return;

                AddPersonView.FillUpPerson(person);
                AddDebtView.SelectedPerson = person;
                LoadDebtOfPerson(person);
                txtDebtsOfPerson.Text = "Debts of person: " + person.Name;
            }
        }

        private void DataGridDebts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridDebts.SelectedItem != null)
            {
                var debt = (DataGridDebts.SelectedItem as Debt);
                if (debt == null) return;

                AddDebtView.FillUpDebt(debt);
            }
        }

        private void CreateNewUser_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }
    }
}
