using RecAndRep.Client.Business.ActionResolver.Model;
using RecAndRep.Client.Business.Attributes;
using RecAndRep.Client.Business.Operations;

namespace RecAndRep.Client.Business.Operators
{
    [Operator("window")]
    class WindowOperator
    {
        [Action("select")]
        public ActionResponse Select(string name)
        {
            var result = WindowOperations.SetWindowByProcessName(name);
            return new ActionResponse()
            {
                Succeeded = result,
                ErrorMessage = !result ? "Process Not Found" : ""

            };
        }
    }
}
