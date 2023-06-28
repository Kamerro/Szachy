using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Szachy;

namespace SzachyTests
{
    [TestClass]
    public class Form1Tests : Chess
    {
        [TestMethod]
        public void ChessCanBeCreated()
        {
            var chess = new Chess();
            Assert.IsNotNull(chess);
        }

        [TestMethod]
        public void ZainicjujSiatkeTest()
        {
            var chess = new Chess();
            ZainicjujSiatke();
        }
    }
}
