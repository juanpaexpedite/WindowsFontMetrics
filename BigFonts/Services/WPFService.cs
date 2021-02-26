using WindowsFontsMetrics.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsFontsMetrics.Services
{
    public class WPFService
    {
        private static bool designmode = true;

        public static bool IsDesignMode()
        {
            return designmode == true;
        }

        public static void SetDesignMode(bool mode)
        {
            designmode = mode;
        }

        private static Window owner;

        public static void SetDialogsOwner(Window mainwindow)
        {
            owner = mainwindow;
        }

        public static void ShowDonateDialogBox()
        {
            var dlg = new DonateView() { Owner = owner };

            dlg.ShowDialog();
        }
    }
}
