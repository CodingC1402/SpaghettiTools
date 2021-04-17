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
        protected bool isSelected = false;
        protected bool isStarted = false;
        public BaseTool()
        {
            editor = SpriteEditor.GetInstance();
        }

        //Do stuff when the tool is selected
        public virtual void Select() 
        {
            isSelected = true;
        }

        //MouseDown
        public virtual void StartJob(MouseButtonEventArgs e)
        {
            isStarted = true;
        }

        //Mouse Move
        public abstract void DoJob(MouseEventArgs e);

        //Mouse up
        public virtual void EndJob(MouseButtonEventArgs e)
        {
            isStarted = false;
        }

        //Do stuff when the tool is unselected
        public virtual void Unselect() 
        {
            isSelected = false; 
        }
    }
}
