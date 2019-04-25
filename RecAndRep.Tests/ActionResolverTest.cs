using RecAndRep.Client.Business.ActionResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecAndRep.Tests
{
    public class ActionResolverTest

    {

        [Theory]
        [InlineData("Operator-Add text_1", true)]
        [InlineData("Operator-Add Text_2", false)]
        public void ActionResolverToExternalAssemblyTest(string action, bool expected)
        {
            var response = ActionResolver.Instance.Parse(action);
            Assert.Equal(response.Succeeded, expected);
        }

        [Theory]
        [InlineData("Operator-Add blablabla;hhjg", "Invalid number of parameters")]
        public void ActionResolverErrorMessagesTest(string action, string errorMessage)
        {
            var response = ActionResolver.Instance.Parse(action);
            Assert.Equal(response.ErrorMessage, errorMessage);
        }

    }
}
