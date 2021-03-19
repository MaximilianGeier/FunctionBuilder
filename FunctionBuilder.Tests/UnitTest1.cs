using FunctionBuilder.Logic;
using NUnit.Framework;

namespace FunctionBuilder.Tests
{
    public class FunctionBuilderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase]
        public void RpnTest()
        {
            string expression = "12+123";
            Assert.AreEqual("12 123+", Rpn.ParseExpression(expression));
        }

        [TestCase("214*12", ExpectedResult = "214 12*")]
        [TestCase("214-12", ExpectedResult = "214 12-")]
        [TestCase("214*12+1", ExpectedResult = "214 12 *1 +")]
        public string Test1(string expression)
        {
            return Rpn.ParseExpression(expression);
        }

        [TestCase('+')]
        public void AssertTest(char c)
        {
            Assert.IsTrue(Rpn.IsSign(c));
        }
    }
}