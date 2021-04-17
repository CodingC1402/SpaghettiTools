using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class EditTool : BaseTool
    {
        public override void StartJob(MouseButtonEventArgs e)
        {
            base.StartJob(e);
        }
        public override void DoJob(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        public override void EndJob(MouseButtonEventArgs e)
        {
            base.EndJob(e);
        }
    }
}
