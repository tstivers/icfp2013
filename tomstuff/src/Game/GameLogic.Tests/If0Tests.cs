using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameClient.SExpressionTree;
using GameClient.ViewModels;
using NUnit.Framework;

namespace GameLogic.Tests
{
    [TestFixture]
    public class If0Tests
    {
        [Test]
        public void ThisShouldWork()
        {            
            var problem = SProgramParser.Parse("(lambda (x_9646) (if0 (and (shr1 (shr4 (shr16 x_9646))) 1) x_9646 0))");
            var solution = SProgramParser.Parse("(lambda (y) (if0 (and (shr16 (shr16 (shr4 y))) y) y 0))");

            var p1 = problem.Eval(0x000000000F73D79A);
            var s1 = solution.Eval(0x000000000F73D79A);

            Assert.That(p1, Is.EqualTo(s1));
        }
    }
}
