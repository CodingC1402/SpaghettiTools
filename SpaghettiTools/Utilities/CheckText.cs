using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace SpaghettiTools.Utilities
{
    public class CheckText
    {
        static Regex _intRegex = new Regex("[^0-9]+"); //regex that matches disallowed text
        public static bool IsInt(string text)
        {
            
            return !_intRegex.IsMatch(text);
        }

        static Regex _floatRegex = new Regex("[^0-9.]+"); //regex that matches disallowed text
        public static bool IsFloat(string text)
        {
            return _floatRegex.IsMatch(text);
        }
    }

    public class TextBoxConstraint
    {
        public static void AddIntOnlyConstraint(TextBox textBox)
        {
            textBox.PreviewTextInput += CheckIntKeyDown;
            DataObject.AddPastingHandler(textBox, CheckIntPasting);
        }
        protected static void CheckIntKeyDown(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !CheckText.IsInt(e.Text);
        }
        protected static void CheckIntPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!CheckText.IsInt(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
