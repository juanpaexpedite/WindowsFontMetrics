using WindowsFontsMetrics.Converters;
using WindowsFontsMetrics.Managers;
using WindowsFontsMetrics.Models;
using WindowsFontsMetrics.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;
using static WindowsFontsMetrics.Converters.WindowParametersConverter;
using static WindowsFontsMetrics.Services.WindowsMetricsService;
using WPFCore;
using WindowsFontsMetrics.Views;

namespace WindowsFontsMetrics.ViewModel
{
    public class FontInformationViewModel : BindableBase
    {
        #region Properties
        private FontInformation captionfontinformation;
        public FontInformation CaptionFontInformation
        {
            get { return captionfontinformation; }
            set
            {
                SetProperty(ref captionfontinformation, value);
            }
        }

        private FontInformation secondarytitlebarinformation;
        public FontInformation SecondarytitlebarInformation
        {
            get { return secondarytitlebarinformation; }
            set
            {
                SetProperty(ref secondarytitlebarinformation, value);
            }
        }

        private FontInformation menuinformation;
        public FontInformation MenuInformation
        {
            get { return menuinformation; }
            set
            {
                SetProperty(ref menuinformation, value);
            }
        }

        private FontInformation messageboxinformation;
        public FontInformation MessageBoxInformation
        {
            get { return messageboxinformation; }
            set
            {
                SetProperty(ref messageboxinformation, value);
            }
        }

        private FontInformation iconinformation;
        public FontInformation IconInformation
        {
            get { return iconinformation; }
            set
            {
                SetProperty(ref iconinformation, value);
            }
        }

        private FontInformation statusbarinformation;
        public FontInformation StatusbarInformation
        {
            get { return statusbarinformation; }
            set
            {
                SetProperty(ref statusbarinformation, value);
            }
        }

        private IntegerInformation borderinformation;
        public IntegerInformation BorderInformation
        {
            get { return borderinformation; }
            set
            {
                SetProperty(ref borderinformation, value);
            }
        }
        #endregion

        #region Constructor and Initialization
        public FontInformationViewModel()
        {
            ApplyCommand = InitializeApplyCommand();
            ReadSettingsCommand= InitializeReadSettingsCommand();
            DonateCommand = InitializeDonateCommand();

            Initialize();
        }


        private void Initialize()
        {
            CaptionFontInformation = new FontInformation("Captions","Title bar and Caption buttons",9);
            SecondarytitlebarInformation = new FontInformation("Secondary caption bar","Small captions", 9);
            MenuInformation = new FontInformation("Menu bar","Menu and Ribbons", 9);
            MessageBoxInformation = new FontInformation("Messagebox bar", "Messagebox and contextmenu", 9);
            IconInformation = new FontInformation("Icon Caption","Icon and explorer descriptions", 9);
            StatusbarInformation = new FontInformation("Status bar","For status bar", 9);
            BorderInformation = new IntegerInformation("Border", "Window Padding", 1);

            if (!WPFService.IsDesignMode())
            {
                InitializeSystemValues();
            }
            
        }
        #endregion

        #region I/O Values

        private Settings windows10settings;

        private async void InitializeSystemValues()
        {
            await Task.Delay(1000);
            windows10settings = SettingsManager.ReadSettings();
            ReadSystemValues();
        }

        private void ReadSystemValues()
        {
            var metrics = GetWindowsNonClientMetrics();
            if (metrics != null)
            {
                CaptionFontInformation.Size = (int)ConvertFontHeight(metrics.lfCaptionFont.lfHeight);
                SecondarytitlebarInformation.Size = (int)ConvertFontHeight(metrics.lfSmCaptionFont.lfHeight);
                MenuInformation.Size = (int)ConvertFontHeight(metrics.lfMenuFont.lfHeight);
                MessageBoxInformation.Size = (int)ConvertFontHeight(metrics.lfMessageFont.lfHeight);
                StatusbarInformation.Size = (int)ConvertFontHeight(metrics.lfStatusFont.lfHeight);
                BorderInformation.Size = metrics.iBorderWidth;
            }

            var iconmetrics = WindowsIconMetricsService.GetWindowsIconMetrics();
            if(iconmetrics != null)
            {
                IconInformation.Size = (int)ConvertFontHeight(iconmetrics.lfFont.lfHeight);
            }
        }

        private void SetSystemValues()
        {
            var metrics = GetWindowsNonClientMetrics();
            if (metrics != null)
            {
                metrics.lfCaptionFont.lfHeight = ConvertToSystemFontHeight(CaptionFontInformation.Size);
                metrics.lfSmCaptionFont.lfHeight = ConvertToSystemFontHeight(SecondarytitlebarInformation.Size);
                metrics.lfMenuFont.lfHeight = ConvertToSystemFontHeight(MenuInformation.Size);
                metrics.lfMessageFont.lfHeight = ConvertToSystemFontHeight(MessageBoxInformation.Size);
                metrics.lfStatusFont.lfHeight = ConvertToSystemFontHeight(StatusbarInformation.Size);

                metrics.iBorderWidth = BorderInformation.Size;

                SetWindowsNonClientMetrics(metrics);
            }

            var iconmetrics = WindowsIconMetricsService.GetWindowsIconMetrics();
            if (iconmetrics != null)
            {
                iconmetrics.lfFont.lfHeight = ConvertToSystemFontHeight(IconInformation.Size);

                WindowsIconMetricsService.SetWindowsIconMetrics(iconmetrics);
            }
        }

        public ICommand ApplyCommand { get; }

        private ICommand InitializeApplyCommand()
        {
            return new DelegateCommand(() =>
            {
                SetSystemValues();
            });
        }

        public ICommand ReadSettingsCommand { get; }

        private ICommand InitializeReadSettingsCommand()
        {
            return new DelegateCommand(() =>
            {
                ReadSettingsValues();
            });
        }

        private async void ReadSettingsValues()
        {
            var metrics = GetWindowsNonClientMetrics();
            if (metrics != null)
            {
                metrics.lfCaptionFont.lfHeight = windows10settings.CaptionFontSize;
                metrics.lfSmCaptionFont.lfHeight = windows10settings.SecondarytitlebarSize;
                metrics.lfMenuFont.lfHeight = windows10settings.MenuSize;
                metrics.lfMessageFont.lfHeight = windows10settings.MessageBoxSize;
                metrics.lfStatusFont.lfHeight = windows10settings.StatusbarSize;
                metrics.iBorderWidth = windows10settings.BorderSize;
                SetWindowsNonClientMetrics(metrics);
            }

            var iconmetrics = WindowsIconMetricsService.GetWindowsIconMetrics();

            if (iconmetrics != null)
            {
                iconmetrics.lfFont.lfHeight = windows10settings.IconCaptionSize;

                WindowsIconMetricsService.SetWindowsIconMetrics(iconmetrics);
            }

            await Task.Delay(1000);
            ReadSystemValues();
        }
        #endregion

        #region Donation
        public ICommand DonateCommand { get; }

        private ICommand InitializeDonateCommand()
        {
            return new DelegateCommand(() =>
            {
                WPFService.ShowDialogBox<DonateView>();
            });
        }
        #endregion
    }
}
