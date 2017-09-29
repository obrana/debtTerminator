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

            DataGridPersons.ItemsSource = database.GetPersons();
        }
    }
}
