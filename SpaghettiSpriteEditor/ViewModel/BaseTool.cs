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

        protected bool isStarted = false;
        public BaseTool()
        {
            editor = SpriteEditor.GetInstance();
        }

        public virtual void StartJob(MouseButtonEventArgs e)
        {
            isStarted = true;
        }
        public abstract void DoJob(MouseEventArgs e);
        public virtual void EndJob(MouseButtonEventArgs e)
        {
            isStarted = false;
        }
    }
}
