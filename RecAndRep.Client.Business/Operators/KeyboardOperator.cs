using RecAndRep.Client.Business.ActionResolver.Model;
using RecAndRep.Client.Business.Attributes;
using RecAndRep.Client.Business.Operations;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace RecAndRep.Client.Business.Operators
{
    [Operator("keyboard")]
    class KeyboardOperator
    {
        [Action("sendkeys")]
        public ActionResponse Send(string text)
        {
            SendKeys.SendWait(text);
            return new ActionResponse()
            {
                Succeeded = true
            };
        }

    }
}
