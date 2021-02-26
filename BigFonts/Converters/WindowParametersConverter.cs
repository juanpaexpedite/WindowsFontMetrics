using WindowsFontsMetrics.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFontsMetrics.Converters
{
    public  class WindowParametersConverter
    {
        /// <summary>
        /// Converts to points
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double ConvertFontHeight(int height)
        {
            var dpi = Dpi;
            if (dpi != 0)
            {
                return (double)(Math.Abs(height) * 96 / dpi);
            }
            else
            {
                // Could not get the DPI to convert the size, using the hardcoded fallback value
                return FallbackFontSize;
            }
        }

        public static int ConvertToSystemFontHeight(double height)
        {
            var dpi = Dpi;
            if (dpi != 0)
            {
                return (int)(-1 * (height) * 96.0 / dpi);
            }
            else
            {
                // Could not get the DPI to convert the size, using the hardcoded fallback value
                return (int)(-1 * FallbackFontSize);
            }
        }

        private const double FallbackFontSize = 11.0;   // To use if unable to get the system size



        // /wpf/src/Core/CSharp/MS/internal/FontCache/FontCacheUtil.cs
        private static object _dpiLock = new object();
        private static int _dpi;
        private static bool _dpiInitialized = false;
        private static int LOGPIXELSX = 88;
        internal static int Dpi
        {
            get
            {
                if (!_dpiInitialized)
                {
                    lock (_dpiLock)
                    {
                        if (!_dpiInitialized)
                        {
                            HandleRef desktopWnd = new HandleRef(null, IntPtr.Zero);

                            // Win32Exception will get the Win32 error code so we don't have to
                            IntPtr dc = WindowsMetricsService.GetDC(desktopWnd);

                            // Detecting error case from unmanaged call, required by PREsharp to throw a Win32Exception
                            if (dc == IntPtr.Zero)
                            {
                                throw new Win32Exception();
                            }

                            try
                            {
                                //src/Microsoft.DotNet.Wpf/src/ReachFramework/MS/Internal/Printing/Configuration/DeviceCap.cs
                                _dpi = WindowsMetricsService.GetDeviceCaps(new HandleRef(null, dc), LOGPIXELSX);
                                _dpiInitialized = true;
                            }
                            finally
                            {
                                WindowsMetricsService.ReleaseDC(desktopWnd, new HandleRef(null, dc));
                            }
                        }
                    }
                }
                return _dpi;
            }
        }


    }
}
