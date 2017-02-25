using MaximStartsev.SmallUtilities.Common.Errors;
using NUnit.Framework;
using System;

namespace MaximStartsev.SmallUtilities.Common.UnitTests.Errors
{
    [TestFixture]
    public class ErrorTests
    {
        [Test]
        public void Ctor_NullExceptionParameter_Null()
        {
            Error error = null;
            try
            {
                error = new Error(null);
            }
            catch {}
            Assert.IsNull(error);
        }
        [Test]
        public void Ctor_SomeExceptionParameter_Object()
        {
            var parameter = new Exception(NSubstitute.Arg.Any<string>());
            Error error = null;
            try
            {
                error = new Error(parameter);
            }
            catch {}
            Assert.IsNotNull(error);
        }
    }
}
