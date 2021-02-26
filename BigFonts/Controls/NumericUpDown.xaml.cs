using Prism.Commands;
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

namespace WindowsFontsMetrics.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            IncreaseValueCommand = InitializeIncreaseCommand();
            DecreaseValueCommand = InitializeDecreaseCommand();
            InitializeComponent();
        }

        #region Value
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));
        #endregion

        #region Caption


        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register(nameof(Caption), typeof(string), typeof(NumericUpDown), new PropertyMetadata(String.Empty));
        #endregion

        #region Description
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(nameof(Description), typeof(string), typeof(NumericUpDown), new PropertyMetadata(String.Empty));
        #endregion

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(nameof(MinValue), typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));



        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(nameof(MaxValue), typeof(int), typeof(NumericUpDown), new PropertyMetadata(100));


        public ICommand IncreaseValueCommand { get; }
        public ICommand DecreaseValueCommand { get; }

        private ICommand InitializeIncreaseCommand()
        {
            return new DelegateCommand(() =>
            {
                var nextvalue = this.Value + 1;
                if (nextvalue > MaxValue)
                {
                    return;
                }
                else
                {
                    Value = nextvalue;
                }
            });
        }

        private ICommand InitializeDecreaseCommand()
        {
            return new DelegateCommand(() =>
            {
                var nextvalue = this.Value - 1;
                if(nextvalue < MinValue)
                {
                    return;
                }
                else
                {
                    Value = nextvalue;
                }
            });
        }
    }
}
