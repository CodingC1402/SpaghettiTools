using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

using SpaghettiTools.Utilities;

namespace SpaghettiTools.Components
{
    public class IntTextBox : TextBox
    {
        public IntTextBox() : base()
        {
            TextBoxConstraint.AddIntOnlyConstraint(this);
        }
    }
}
