using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaghettiSpriteEditor.ViewModel
{
    public abstract class BaseTool // Base class for tools
    {
        SpriteEditor.Tools toolType;

        public abstract void StartJob(MouseButtonEventArgs e);
        public abstract void EndJob(MouseButtonEventArgs e);
        public abstract void DoJob(MouseEventArgs e);
    }
}
