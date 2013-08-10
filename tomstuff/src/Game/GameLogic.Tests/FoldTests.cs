using GameClient.SExpressionTree;
using NUnit.Framework;

namespace GameLogic.Tests
{
    [TestFixture]
    public class FoldTests : TestBase
    {
        [Test]
        public void TestFold1()
        {
            var program = SProgramParser.Parse("(lambda (x) (fold x 0 (lambda (y z) (or y z))))");

            var val1 = program.Eval(0x1122334455667788);

            Assert.That(val1, Is.EqualTo(0x00000000000000FF));
        }

        [Test]
        public void TestFold2()
        {
            var program = SProgramParser.Parse("(lambda (x) (fold x 1 (lambda (y z) (xor y z))))");

            var val1 = program.Eval(0x1122334455667788);

            Assert.That(val1, Is.EqualTo(0x0000000000000089));
        }
    }
}
