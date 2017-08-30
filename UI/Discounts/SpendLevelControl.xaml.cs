
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GTI.Modules.Shared.Business;

namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// Interaction logic for SpendLevelControl.xaml
    /// </summary>
    public partial class SpendLevelControl
    {        
        #region Constants

        private Brush NoInputBrush
        {
            get { return Brushes.DarkGray; }
        }

        private Brush ValidBrush
        {
            get
            {
                return (Brush)TryFindResource("BlueBrush");
            }
        }
        #endregion

        #region Constructor

        public SpendLevelControl(int sequenceNumber)
        {
            InitializeComponent();

            //create new spendlevel
            SpendLevel = new DiscountItem.SpendLevel { Sequence = sequenceNumber };

            AttachEvents();
        }

        public SpendLevelControl(DiscountItem.SpendLevel spendLevel)
        {
            InitializeComponent();

            //create new spendlevel so we don't modify the original
            SpendLevel = new DiscountItem.SpendLevel(spendLevel);

            SpendFromTextbox.Foreground = ValidBrush;
            SpendToTextbox.Foreground = ValidBrush;
            SpendValueTextbox.Foreground = ValidBrush;

            //attach events after textboxes have been initialized
            AttachEvents();
        }
        #endregion

        #region Properties

        public DiscountItem.SpendLevel SpendLevel { get; private set; }

        public bool IsValid
        {
            get
            {
                //check if spend from is greater than spend to
                var isValid = !(SpendLevel.SpendMinValue >= SpendLevel.SpendMaxValue);

                //make sure max and value is not 0
                if (SpendLevel.SpendMaxValue == 0 || SpendLevel.SpendValue == 0)
                {
                    isValid = false;
                }

                return isValid;
            }
        }

        public bool IsEmpty
        {
            get
            {
                var from = decimal.Parse(SpendFromTextbox.Text);
                var to = decimal.Parse(SpendToTextbox.Text);
                var value = decimal.Parse(SpendValueTextbox.Text);

                return from + to + value == 0;
            }
        }

        #endregion

        #region Methods

        private void AttachEvents()
        {
            //initialize UI values
            SpendFromTextbox.Text = ((int)SpendLevel.SpendMinValue).ToString(CultureInfo.InvariantCulture);
            SpendToTextbox.Text = ((int)SpendLevel.SpendMaxValue).ToString(CultureInfo.InvariantCulture);
            SpendValueTextbox.Text = Helper.DecimalStringToMoneyString(SpendLevel.SpendValue.ToString(CultureInfo.InvariantCulture));

            //wire events after textboxes have been initialized
            SpendFromTextbox.TextChanged += SpendFromTextboxTextChanged;
            SpendFromTextbox.PreviewTextInput += SpendTextboxPreviewTextInput;
            SpendFromTextbox.PreviewKeyDown += SpendTextBoxPreviewKeyDown;
            SpendFromTextbox.LostFocus += SpendTextBoxLostFocus;
            SpendFromTextbox.GotFocus += SpendLevelTextboxGotFocus;

            SpendToTextbox.TextChanged += SpendToTextboxTextChanged;
            SpendToTextbox.PreviewTextInput += SpendTextboxPreviewTextInput;
            SpendToTextbox.PreviewKeyDown += SpendTextBoxPreviewKeyDown;
            SpendToTextbox.LostFocus += SpendTextBoxLostFocus;
            SpendToTextbox.GotFocus += SpendLevelTextboxGotFocus;

            SpendValueTextbox.TextChanged += SpendValueTextboxTextChanged;
            SpendValueTextbox.PreviewTextInput += SpendTextboxPreviewTextInput;
            SpendValueTextbox.PreviewKeyDown += SpendTextBoxPreviewKeyDown;
            SpendValueTextbox.LostFocus += SpendTextBoxLostFocus;
            SpendValueTextbox.GotFocus += SpendLevelTextboxGotFocus;
        }

        private void SpendFromTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(SpendFromTextbox.Text, out value))
            {
                return;
            }

            SpendLevel.SpendMinValue = value;
        }

        private void SpendToTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(SpendToTextbox.Text, out value))
            {
                return;
            }

            SpendLevel.SpendMaxValue = value;

        }

        private void SpendValueTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(SpendValueTextbox.Text, out value))
            {
                return;
            }

            SpendLevel.SpendValue = value;

        }

        private void SpendTextboxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool notAllow = false;
            TextBox y = (TextBox)sender;


            string x = y.Text;
            x = x.Insert(y.SelectionStart, e.Text);

            if (y.Name == "SpendFromTextbox" || y.Name == "SpendToTextbox")
            {
                Regex regex = new Regex("[^0-9]+");
                notAllow = regex.IsMatch(e.Text);
            }
            else
            {
                int count = x.Split('.').Length - 1;

                if (count > 1)//One decimal point only.
                {
                    notAllow = true;
                }
                else if ((Convert.ToChar(e.Text)) == '.')
                {
                }
                else if (Char.IsNumber(Convert.ToChar(e.Text)))
                {
                    if (Regex.IsMatch(x, @"\.\d\d\d"))//Only allow .## 
                    {
                        notAllow = true;
                    }
                }
                else
                {
                    notAllow = true;
                }
            }


            e.Handled = notAllow;
        }

        private void SpendTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var notAllow = e.Key == Key.Space;

            e.Handled = notAllow;
        }

        private void SpendTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox selectedTxtBx = (TextBox)sender;

            if (string.IsNullOrEmpty(selectedTxtBx.Text))
            {
                selectedTxtBx.Text = selectedTxtBx.Name == "SpendValueTextbox" ? "0.00" : "0";
                selectedTxtBx.Foreground = NoInputBrush;
            }
        }

        private void SpendLevelTextboxGotFocus(object sender, RoutedEventArgs e)
        {
            var selectedTxtBx = (TextBox)sender;

            if (Equals(selectedTxtBx.Foreground, NoInputBrush))
            {
                selectedTxtBx.Text = string.Empty;
                selectedTxtBx.Foreground = ValidBrush;
            }
        }

        public void EnableControls(bool enable)
        {
            SpendFromTextbox.IsEnabled = enable;
            SpendToTextbox.IsEnabled = enable;
            SpendValueTextbox.IsEnabled = enable;
            AddButton.IsEnabled = enable;
            RemoveButton.IsEnabled = enable;
        }

        public void Reset()
        {
            AddButton.IsEnabled = true;
            RemoveButton.IsEnabled = true;

            SpendFromTextbox.Text = "0";
            SpendToTextbox.Text = "0";
            SpendValueTextbox.Text = "0.00";

            SpendFromTextbox.Foreground = NoInputBrush;
            SpendToTextbox.Foreground = NoInputBrush;
            SpendValueTextbox.Foreground = NoInputBrush;

            IsEnabled = true;
        }

        #endregion
    }
}
