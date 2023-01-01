using System;

namespace _core.Script.UI
{
    //to show the result of operation panel
    public interface IOperationPanel
    {
        Action<IVariable> Result { get; set; }
    }
}