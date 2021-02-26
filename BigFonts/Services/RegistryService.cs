using BigFonts.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigFonts.Services
{
    public class RegistryService
    {
        public static string ReadUserStringValue(FontInformation information)
        {
            RegistryKey hkcu = Registry.CurrentUser; //HKCU Registry

            var key = hkcu.OpenSubKey(information.Path);

            var keyvalue = key.GetValue("CaptionFont");

            return null;
        }

        public static void ReadFontSizeValue(FontInformation information)
        {
            RegistryKey hkcu = Registry.CurrentUser; //HKCU Registry

            var pathkey = hkcu.OpenSubKey(information.Path);

            var keyvalue = pathkey.GetValue(information.Key);

            if(keyvalue != null && keyvalue is byte[] values)
            {
                var fontsize = values[0];
                information.Size = fontsize;
            }

            
        }

        public static void WriteFontSizeValue(FontInformation information)
        {
            RegistryKey hkcu = Registry.CurrentUser; //HKCU Registry

            var pathkey = hkcu.OpenSubKey(information.Path,true);

            var keyvalue = pathkey.GetValue(information.Key);

            if (keyvalue != null && keyvalue is byte[] values 
                && information.Size <= byte.MaxValue
                && information.Size >= byte.MinValue)
            {
                values[0] = (byte)information.Size;

                try
                {
                    pathkey.SetValue(information.Key, values);
                }
                catch (Exception ex)
                {
                    string k = "";
                }
            }

            
        }
            
    }
}
