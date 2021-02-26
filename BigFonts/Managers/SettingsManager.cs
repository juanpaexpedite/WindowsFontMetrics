using WindowsFontsMetrics.Models;
using WindowsFontsMetrics.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFontsMetrics.Managers
{
    /// <summary>
    /// This class is to backup the metrics of Windows before changing any value.
    /// </summary>
    public class SettingsManager
    {
        private static string settingspath = "Data\\settings.json";

        public static Settings ReadSettings()
        {
            //First time
            if(!FilesManager.Exists(settingspath))
            {
                var metrics = WindowsMetricsService.GetWindowsNonClientMetrics();
                var iconmetrics = WindowsIconMetricsService.GetWindowsIconMetrics();
                var settings = SetValues(metrics,iconmetrics);

                SaveSettings(settings);

                return settings;

            }
            else
            {
                var rawsettings = FilesManager.ReadString(settingspath);
                return JsonConvert.DeserializeObject<Settings>(rawsettings);
            }

        }

        private static void SaveSettings(Settings settings)
        {
            var rawsettings = JsonConvert.SerializeObject(settings);
            FilesManager.WriteString(settingspath, rawsettings);
        }

        private static Settings SetValues(NONCLIENTMETRICS metrics, ICONMETRICS iconmetrics)
        {
            var settings = new Settings();

            settings.CaptionFontSize = metrics.lfCaptionFont.lfHeight;
            settings.SecondarytitlebarSize = metrics.lfSmCaptionFont.lfHeight;
            settings.MenuSize = metrics.lfMenuFont.lfHeight;
            settings.MessageBoxSize = metrics.lfMessageFont.lfHeight;
            settings.StatusbarSize = metrics.lfStatusFont.lfHeight;
            settings.BorderSize = metrics.iBorderWidth;
            settings.IconCaptionSize = iconmetrics.lfFont.lfHeight;

            return settings;
        }
    }
}
