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
        protected SpriteEditor.Tools toolType = SpriteEditor.Tools.Invalid;
        protected SpriteEditor editor;

        public BaseTool()
        {
            editor = SpriteEditor.GetInstance();
        }

        public abstract void StartJob(MouseButtonEventArgs e);
        public abstract void EndJob(MouseButtonEventArgs e);
        public abstract void DoJob(MouseEventArgs e);
    }
}
