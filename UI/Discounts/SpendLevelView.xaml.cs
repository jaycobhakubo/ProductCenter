using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using GTI.Modules.Shared.Business;

namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// Interaction logic for SpendLevels.xaml
    /// </summary>
    public partial class SpendLevelView
    {
        #region Constants
        private Brush ERROR_BRUSH
        {
            get { return Brushes.Red; }
        }

        private Brush ValidBrush 
        {
            get
            {
                return (Brush)TryFindResource("BlueBrush");
            }
        }
        #endregion

        #region VARIABLES (private)

        private bool m_spendInputValid;

        #endregion

        #region events

        /// <summary>
        /// event called when the spend level count is changed
        /// </summary>
        public EventHandler SpendLevelCountChanged;

        #endregion


        #region PROPERTIES

        public bool IsValidForSave
        {
            get
            {
                //validate the last spend level to see if its valid to save. 
                //if its empty (all zero's) then we ignore the last spend level and save
                //else if its not empty, then we validate it
                var lastSpendLevel = SpendLevelStackPanel.Children.OfType<SpendLevelControl>().LastOrDefault();
                var isEmpty =  lastSpendLevel != null && lastSpendLevel.IsEmpty;

                return isEmpty || IsSpendLevelInputValid();
            }
        }

        public List<DiscountItem.SpendLevel> ListOfSpendLevels
        {
            get
            {
                //returns a list of spend levels that are valid
                return SpendLevelStackPanel.Children.OfType<SpendLevelControl>()
                    .Where(i => i.IsValid)
                    .Select(control => control.SpendLevel).ToList();
            }
        }

        #endregion

        #region CONSTRUCTOR

        public SpendLevelView()
        {
            InitializeComponent();

            DataContext = this;
        }
        
        #endregion

        #region METHODS

        public void EnableSpendLevelContent(bool enable)
        {
            if (enable)
            {
                SpendLevelBorder.Visibility = Visibility.Visible;
                ContentDisabledBorder.Visibility = Visibility.Hidden;
            }
            else
            {
                SpendLevelBorder.Visibility = Visibility.Hidden;
                ContentDisabledBorder.Visibility = Visibility.Visible;
            }
        }

        public void ResetSpendLevels()
        {
            //clear
            SpendLevelStackPanel.Children.Clear();

            //add default
            AddSpendLevelControls(new SpendLevelControl(1));
        }

        public void LoadSpendLevels(List<DiscountItem.SpendLevel> spendLevels)
        {
            SpendLevelStackPanel.Children.Clear();

            //if empty, then add the default
            if (spendLevels == null ||
                spendLevels.Count <= 0)
            {
                //add default
                AddSpendLevelControls(new SpendLevelControl(1));

                return;
            }
            
            //load existing spend levels
            for (int i = 0; i < spendLevels.Count; i++)
            {
                //if its last item, then enable control, else disable
                var isEnable = i == spendLevels.Count - 1;
                AddSpendLevelControls(new SpendLevelControl(spendLevels[i]), isEnable);
            }
        }

        private void AddSpendLevelControls(SpendLevelControl spendLevelControl, bool isEnabled = true)
        {
            //attach events
            spendLevelControl.AddButton.Click += AddSpendLevelButtonClick;
            spendLevelControl.RemoveButton.Click += RemoveSpendLevelButtonClick;
            spendLevelControl.GotFocus += SpendLevelControlGotFocus;

            //add to stack panel UI
            SpendLevelStackPanel.Children.Add(spendLevelControl);

            //enable the controls for the spend level
            spendLevelControl.EnableControls(isEnabled);
            
            //scroll to end
            scrlViewerSpend.ScrollToEnd();
        }

        private bool IsSpendLevelInputValid()
        {
            bool result = true;
            decimal minSpend = 0;
            decimal maxSpend = 0;

            var previousMaxSpendValue = 0m;
            if (SpendLevelStackPanel.Children.Count > 1)
            {
                var control =
                    SpendLevelStackPanel.Children[SpendLevelStackPanel.Children.Count - 2] as SpendLevelControl;
                if (control != null)
                {
                    previousMaxSpendValue = control.SpendLevel.SpendMaxValue;
                }
            }

            var lastSpendLevel = SpendLevelStackPanel.Children.OfType<SpendLevelControl>().LastOrDefault();

            if (lastSpendLevel != null)
            {
                decimal value;
                //check spend from text box
                if (string.IsNullOrEmpty(lastSpendLevel.SpendFromTextbox.Text))
                {
                    lastSpendLevel.SpendFromTextbox.BorderBrush = ERROR_BRUSH;
                    m_spendInputValid = false;
                    result = false;
                }

                //check spend to text box
                if (string.IsNullOrEmpty(lastSpendLevel.SpendToTextbox.Text))
                {
                    lastSpendLevel.SpendValueTextbox.BorderBrush = ERROR_BRUSH;
                    m_spendInputValid = false;
                    result = false;
                }

                //check discount text box
                if (string.IsNullOrEmpty(lastSpendLevel.SpendValueTextbox.Text))
                {
                    lastSpendLevel.SpendValueTextbox.BorderBrush = ERROR_BRUSH;
                    m_spendInputValid = false;
                    result = false;
                }

                if (decimal.TryParse(lastSpendLevel.SpendFromTextbox.Text, out value))
                {
                    minSpend = value;
                }

                if (decimal.TryParse(lastSpendLevel.SpendToTextbox.Text, out value))
                {
                    maxSpend = value;
                }

                if (!result) // DE12908 break out early, but still mark all the fields that are blank
                {
                    return false;
                }

                if (minSpend > maxSpend)//If from > than to
                {
                    lastSpendLevel.SpendFromTextbox.BorderBrush = ERROR_BRUSH;
                    lastSpendLevel.SpendToTextbox.BorderBrush = ERROR_BRUSH;
                    m_spendInputValid = false;
                    result = false;
                }
                else if (minSpend <= previousMaxSpendValue)//if from <= ToPrv
                {
                    lastSpendLevel.SpendFromTextbox.BorderBrush = ERROR_BRUSH;
                    m_spendInputValid = false;
                    result = false;
                }
                else
                {
                    lastSpendLevel.SpendFromTextbox.BorderBrush = ValidBrush;
                    lastSpendLevel.SpendToTextbox.BorderBrush = ValidBrush;
                    lastSpendLevel.SpendValueTextbox.BorderBrush = ValidBrush;
                }
            }

            return result;
        }
        
        private void AddSpendLevelButtonClick(object sender, RoutedEventArgs e)
        {
            if (!IsSpendLevelInputValid())
            {
                return;
            }

            //we need to disable the previous control
            var count = SpendLevelStackPanel.Children.Count;
            if (count > 0)
            {
                var control = SpendLevelStackPanel.Children[count - 1] as SpendLevelControl;
                if (control != null)
                {
                    control.EnableControls(false);
                }
            }

            //add new control
            AddSpendLevelControls(new SpendLevelControl(count + 1));

            //raise event
            var handler = SpendLevelCountChanged;
            if (handler != null)
                handler(this, null);
        }

        private void RemoveSpendLevelButtonClick(object sender, RoutedEventArgs e)
        {
            //get count
            var count = SpendLevelStackPanel.Children.Count;

            //if only one then just clear contents
            if (count == 1)
            {
                var control = SpendLevelStackPanel.Children[count - 1] as SpendLevelControl;
                if (control != null)
                {
                    control.Reset();
                }
            }

            //if more then one, then remove
            if (count > 1)
            {
                SpendLevelStackPanel.Children.RemoveAt(count - 1);
            }

            //enable previous control
            count = SpendLevelStackPanel.Children.Count;
            if (count > 0)
            {
                var control = SpendLevelStackPanel.Children[count - 1] as SpendLevelControl;
                if (control != null)
                {
                    control.EnableControls(true);
                }
            }

            //raise event
            var handler = SpendLevelCountChanged;
            if (handler != null)
                handler(this, null);
        }

        private void SpendLevelControlGotFocus(object sender, RoutedEventArgs e)
        {
            var control = (SpendLevelControl)sender;

            //if valid then return
            if (m_spendInputValid)
            {
                return;
            }

            //else reset controls borders
            control.SpendFromTextbox.BorderBrush = ValidBrush;
            control.SpendToTextbox.BorderBrush = ValidBrush;
            control.SpendValueTextbox.BorderBrush = ValidBrush;
            m_spendInputValid = true;
        }
        #endregion
    }
}
