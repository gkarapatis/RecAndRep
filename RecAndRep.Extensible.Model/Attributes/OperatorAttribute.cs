using System;

namespace RecAndRep.Extensible.Model.Attributes
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
