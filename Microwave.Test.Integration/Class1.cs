using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void test()
        {
            Assert.That(true, Is.True);
        }
    }
}
