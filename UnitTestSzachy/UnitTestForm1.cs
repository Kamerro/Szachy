using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Szachy;

namespace UnitTestSzachy
{
    [TestClass]
    public class UnitTestForm1
    {
        [TestMethod]
        public void TestChessObjectCanBeCreated()
        {
            var chess = new Chess();
            Assert.IsNotNull(chess);
        }
    }
}
