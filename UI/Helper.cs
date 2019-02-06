using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GTI.Modules.ProductCenter.UI
{
    public static class Helper
    {
        #region Numeric Field event support
        public static void Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                string tempT = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength);
                if (!char.IsNumber(e.KeyChar) &&
                    (Keys)e.KeyChar != Keys.Back &&
                    (Keys)e.KeyChar != Keys.Delete &&
                    e.KeyChar != '-' &&
                    e.KeyChar != '+')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '-' || e.KeyChar == '+')
                {
                    if (tempT.Contains(e.KeyChar.ToString())) 
                        e.Handled = true;
                }
            }
        }

        public static void Number_Validating(object sender, CancelEventArgs e)
        {
            // if input is not in correct format, reject
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (!ValidateNumber(tb.Text))
                {
                    tb.Select(0, tb.Text.Length);
                    tb.BackColor = Color.LightPink;
                }
                else
                    tb.BackColor = Color.FromArgb(215, 251, 193);
            }
        }
        private static bool ValidateNumber(string inText)
        {
            bool isOk = false;
            if (inText.Length > 0)
            {
                int result;
                isOk = int.TryParse(inText, out result);
            }
            return isOk;
        }
        #endregion

        #region Decimal Field event support
        public static void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                string tempT = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength);
                if (!char.IsNumber(e.KeyChar) &&
                    (Keys)e.KeyChar != Keys.Back &&
                    (Keys)e.KeyChar != Keys.Delete &&
                    e.KeyChar != '-' &&
                    e.KeyChar != '+' &&
                    e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '-' ||
                    e.KeyChar == '+' ||
                    e.KeyChar == '.')
                {
                    if (tempT.Contains(e.KeyChar.ToString()))
                        e.Handled = true;
                }
            }
        }
        // FIX : DE3115 prevent negative values on some price fields
        public static void PositiveDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                string tempT = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength);
                // allow only numeric, backspace, delete, plus and period
                if (!char.IsNumber(e.KeyChar) &&
                    (Keys)e.KeyChar != Keys.Back &&
                    (Keys)e.KeyChar != Keys.Delete &&
                    e.KeyChar != '+' &&
                    e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '+' ||
                    e.KeyChar == '.')
                {
                    if (tempT.Contains(e.KeyChar.ToString()))
                        e.Handled = true;
                }
            }
        }
        // END : DE3115
        public static void Decimal_Validating(object sender, CancelEventArgs e)
        {
            // if input is not in correct format, reject
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (!ValidateDecimal(tb.Text))
                {
                    tb.Select(0, tb.Text.Length);
                    tb.BackColor = Color.LightPink;
                }
                else
                    tb.BackColor = Color.FromArgb(215, 251, 193);
            }
        }
        private static bool ValidateDecimal(string inText)
        {
            bool isOk = false;
            if (inText.Length > 0)
            {
                decimal result;
                isOk = decimal.TryParse(inText, out result);
            }
            return isOk;
        }
        #endregion

        #region Money Entry Field handling
        private static NumberFormatInfo GetNumberFormat()
        {
            NumberFormatInfo myFormat = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            switch (myFormat.CurrencyNegativePattern)
            {
                case 0: myFormat.CurrencyNegativePattern = 2;
                    break;
                case 4: myFormat.CurrencyNegativePattern = 5;
                    break;
                case 14: myFormat.CurrencyNegativePattern = 12;
                    break;
                case 15: myFormat.CurrencyNegativePattern = 8;
                    break;
            }
            return myFormat;
        }

        public static string DecimalStringToMoneyString(string decimalInput)
        {
            string decimalString = string.IsNullOrEmpty(decimalInput) ? "0.00" : decimalInput;
            decimalString = decimalString.Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol, "");
            decimal result;
            decimal.TryParse(decimalString, out result);
            return result.ToString("F", GetNumberFormat());
        }

        public static string DecimalStringToMoneyString(decimal decimalInput)
        {
            return decimalInput.ToString("F", GetNumberFormat());
        }

        
        public static string MoneyStringToDecimalString(string moneyInput)
        {
            return moneyInput.Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol, "");
           
        }

        public static void Money_Enter(object sender, EventArgs e)
        {
            // show money amount without $ sign
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.Text = MoneyStringToDecimalString(tb.Text);
               
            }
        }

        public static void Money_Validated(object sender, EventArgs e)
        {
            //  show money amount with $ sign
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.Text = DecimalStringToMoneyString(tb.Text);
                
            }
        }
        #endregion
    }
}
