using RecAndRep.Extensible.Model.Model;
using RecAndRep.Extensible.Model.Attributes;
using RecAndRep.Client.Business.Operations;
using System;

namespace RecAndRep.Client.Business.Operators
{
    [Operator("mouse")]
    public class MouseClick
    {
        public enum ClickTypeEnum
        {
            Single,
            Double
        }

        [Action("click")]
        public ActionResponse Click(string clickType)
        {
            switch (clickType)
            {
                case "Single":
                    MouseOperations.MouseClick();
                    return new ActionResponse()
                    {
                        Succeeded = true
                    };                    
                case "Double":
                    break;

            }

            return new ActionResponse()
            {
                Succeeded = false,
                ErrorMessage = "Type not found."
            };
        }


        [Action("move")]
        public ActionResponse Move(int x, int y)
        {
            MouseOperations.SetCursorPosition(x,y);
            return new ActionResponse()
            {
                Succeeded = true,
            };
        }
    }
}
