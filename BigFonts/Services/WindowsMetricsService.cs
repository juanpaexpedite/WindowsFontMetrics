using WindowsFontsMetrics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowsFontsMetrics.Services
{
    public class WindowsMetricsService
    {
        #region Get All Windows Running with their sizes


        //GITHUB SOURCE CODE: wpf/src/Framework/System/Windows/SystemParameters.cs

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        public static extern bool SystemParametersInfo(int uiAction, int uiParam, [In, Out] NONCLIENTMETRICS pvParam, int fWinIni);

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(HandleRef hWnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(HandleRef hWnd, HandleRef hDC);

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);
        #endregion


        static int SPI_GETNONCLIENTMETRICS = 41;
        static int SPI_SETNONCLIENTMETRICS = 42;

        public static NONCLIENTMETRICS GetWindowsNonClientMetrics()
        {
            var _ncm = new NONCLIENTMETRICS();
            if (SystemParametersInfo(SPI_GETNONCLIENTMETRICS, _ncm.cbSize, _ncm, 0))
            {
                return _ncm;
            }
            return null;
        }

        public static void SetWindowsNonClientMetrics(NONCLIENTMETRICS metrics)
        {
            SystemParametersInfo(SPI_SETNONCLIENTMETRICS, 0, metrics, 3);
        }

       
    }
}
