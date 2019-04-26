using RecAndRep.Extensible.Model.Model;
using RecAndRep.Extensible.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.ExternalAssembly
{
    [Operator("Operator")]
    public class ExternalTestOrepator
    {
        [Action("Add")]
        public ActionResponse Send(string text)
        {
            if (text.Equals("Text_2"))
            {
                return new ActionResponse()
                {
                    Succeeded = false,
                };
            }
            return new ActionResponse()
            {
                Succeeded = true,
            };
        }
    }
}
