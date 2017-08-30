using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GTI.Modules.ProductCenter.Data
{
    public class EnumToString
    {
        /// <summary>
        /// returns the description of the enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(object value)
        {
            string output = value.ToString();

            try
            {
                // Get the enum type
                var enumVal = (Enum)value;
                var enumType = enumVal.GetType();

                // Get the description attribute
                var descriptionAttribute = enumType.GetField(enumVal.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                // Get the description (if there is one) or the name of the
                // enum otherwise
                output = (descriptionAttribute != null ?
                    descriptionAttribute.Description : enumVal.ToString());

            }
            catch { }

            return output;
        }
    }
}
