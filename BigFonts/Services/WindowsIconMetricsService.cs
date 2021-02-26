using WindowsFontsMetrics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFontsMetrics.Services
{
    public class WindowsIconMetricsService
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In, Out] ICONMETRICS pvParam, int fWinIni);

        static int SPI_GETICONMETRICS = 45;
        static int SPI_SETICONMETRICS = 46;

        public static ICONMETRICS GetWindowsIconMetrics()
        {
            var _ncm = new ICONMETRICS();
            if (SystemParametersInfo(SPI_GETICONMETRICS, _ncm.cbSize, _ncm, 0))
            {
                return _ncm;
            }
            return null;
        }

        public static void SetWindowsIconMetrics(ICONMETRICS metrics)
        {
            SystemParametersInfo(SPI_SETICONMETRICS, 0, metrics, 3);
        }
    }
}
