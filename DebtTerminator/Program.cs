using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtTerminator
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = -1;

            while (choice != 0)
            {
                ShowMenu();
                int.TryParse(Console.ReadLine(), out choice);
            }
        }

        private static void ShowMenu()
        {
            var menu = "===== M E N U =====\n";
            menu += "1) \n";
            menu += "2) \n";
            menu += "3) \n";
            menu += "===================\n";
            menu += "0) \n";
            menu += "===================\n";
            Console.Clear();
            Console.WriteLine(menu);
            Console.WriteLine("Your choice: ");
        }
    }
}

