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

namespace PapaJonsDebtTerminator.Views
{
    /// <summary>
    /// Interaction logic for AddDebtToPersonView.xaml
    /// </summary>
    public partial class AddDebtToPersonView : UserControl
    {
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                LblNamePerson.Text = _selectedPerson.Name;
            }
        }

        private DbLayer.DbLayer database;
        private Person _selectedPerson;

        public AddDebtToPersonView()
        {
            InitializeComponent();

            InitView();
        }
        private void InitView()
        {
            database = DbLayer.DbLayer.Database;
        }

        private void BtnAddDebt_OnClick(object sender, RoutedEventArgs e)
        {
            var debt = new Debt()
            {
                Amout = decimal.Parse(TxtAmout.Text),
                DateStart = DateTime.Parse(TxtDateFrom.Text),
                DebtStatus = bool.Parse(TxtDebtStatus.Text),
                DueDate = DateTime.Parse(TxtDateTo.Text),
                PaidAmout = decimal.Parse(TxtPaidAmout.Text)
            };

            database.AddDebtToPerson(SelectedPerson, debt);
        }
    }
}
