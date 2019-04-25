using System;

namespace RecAndRep.Client.Business.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OperatorAttribute : Attribute
    {
        public string Name { get; set; }

        public OperatorAttribute(string name)
        {
            this.Name = name;
        }
    }
}
