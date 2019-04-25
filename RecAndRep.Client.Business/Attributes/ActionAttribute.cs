using System;

namespace RecAndRep.Client.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionAttribute : Attribute
    {
        public string Name { get; set; }

        public ActionAttribute(string name)
        {
            this.Name = name;
        }
    }
}
