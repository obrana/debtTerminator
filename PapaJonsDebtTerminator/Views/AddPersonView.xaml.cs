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
        private MainWindow _mainWindow;

        public MainWindow MainWindow { get; set; }
        public AddPersonView()
        {
            InitializeComponent();

            InitView();
        }

        private void InitView()
        {
            database = DbLayer.DbLayer.Database;
        }

        public void FillUpPerson(Person person)
        {
            TxtCpr.Text = person.CPR;
            TxtAddress.Text = person.Address;
            TxtEmail.Text = person.Email;
            TxtName.Text = person.Name;
            TxtPhone.Text = person.Phone;
            CbGender.SelectedValue = person.Gender;
            DpDob.SelectedDate = person.DOB;
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            var dob = DpDob.SelectedDate;

            var person = new Person()
            {
                Address = TxtAddress.Text,
                CPR = TxtCpr.Text,
                DOB = (DateTime)dob,
                Email = TxtEmail.Text,
                Gender = CbGender.SelectedValue.ToString(),
                Name = TxtName.Text,
                Phone = TxtPhone.Text
            };

            var result = database.AddPerson(person);
            if (result) this.MainWindow.LoadPersons();
        }
    }
}
