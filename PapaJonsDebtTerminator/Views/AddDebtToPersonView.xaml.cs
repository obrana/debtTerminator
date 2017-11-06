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
using PapaJonsDebtTerminator.Utils;

namespace PapaJonsDebtTerminator.Views
{
    /// <summary>
    /// Interaction logic for AddDebtToPersonView.xaml
    /// </summary>
    public partial class AddDebtToPersonView : UserControl,IUpdater
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
        private Debt ActualDebt;

        public AddDebtToPersonView()
        {
            InitializeComponent();

            UpdateControler.controls.Add(this);
            InitView();
        }
        private void InitView()
        {
            database = DbLayer.DbLayer.Database;
        }

        private void BtnAddDebt_OnClick(object sender, RoutedEventArgs e)
        {
            var dateFrom = DpDateFrom.SelectedDate;
            var dateTo = DpDateTo.SelectedDate;

            var debt = new Debt()
            {
                Amout = decimal.Parse(TxtAmout.Text),
                DateStart = (DateTime)dateFrom,
                DebtStatus = (bool)ChbDebtStatus.IsChecked,
                DueDate = (DateTime)dateTo,
                PaidAmout = decimal.Parse(TxtPaidAmout.Text)
            };

            var result = database.AddDebtToPerson(SelectedPerson, debt);
            if (result)
            {
                MessageBox.Show("The debt was added!", "Success", MessageBoxButton.OK);
                UpdateControler.Update();
            }
            else
            {
                MessageBox.Show("The debt was not added!", "Success", MessageBoxButton.OK);
            }
        }

        public void FillUpDebt(Debt debt)
        {
            if (debt != null)
            {
                DpDateFrom.SelectedDate = debt.DateStart;
                DpDateTo.SelectedDate = debt.DueDate;
                ChbDebtStatus.IsChecked = debt.DebtStatus;
                TxtPaidAmout.Text = debt.PaidAmout.ToString();
                TxtAmout.Text = debt.Amout.ToString();
                ActualDebt = debt;
            }
            else
            {
                DpDateFrom.SelectedDate = DateTime.Now;
                DpDateTo.SelectedDate = DateTime.Now;
                ChbDebtStatus.IsChecked = false;
                TxtPaidAmout.Text = string.Empty;
                TxtAmout.Text = string.Empty;
            }
        }

        private void ChbDebtStatus_OnChecked(object sender, RoutedEventArgs e)
        {
            ChbDebtStatus.Content = (bool)ChbDebtStatus.IsChecked?"PAID":"UNPAID";
        }

        private void BtnIncrease_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parsedValue = int.Parse(TxtIncreaseValue.Text);
                var result = database.IncreaseDebt(ActualDebt, parsedValue);
                if (result)
                {
                    FillUpDebt(ActualDebt);
                    UpdateControler.Update();
                    MessageBox.Show("The debt amount was increased!", "Success",MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("The debt amount was not increased!", "Success", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        public void Update()
        {
            FillUpDebt(ActualDebt);
        }
    }
}
