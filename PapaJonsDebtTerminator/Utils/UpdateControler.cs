using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PapaJonsDebtTerminator.Utils
{
    static class UpdateControler
    {
        public static List<IUpdater> controls = new List<IUpdater>();
        public static void Update()
        {
            foreach (var control in controls)
            {
                control.Update();
            }
        }
    }
}
