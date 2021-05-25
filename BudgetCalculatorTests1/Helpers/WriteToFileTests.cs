﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BudgetCalculator.Helpers.Tests
{
    [TestClass()]
    public class WriteToFileTests
    {

        [TestMethod()]
        public void WriteStringToFileTest_ShouldWriteToFile()
        {
            var fileTxt = new WriteToFile();
            string test = "Hello";
            fileTxt.WriteStringToFile("error", test);

            var expected = File.ReadAllLines(fileTxt.PathAndFileName);
            Assert.AreEqual(expected[0], test);
        }

        [TestMethod()]
        public void WriteStringToFileTest_ShouldNotWriteToFile()
        {
            var fileTxt = new WriteToFile();
            string test = string.Empty;

            fileTxt.WriteStringToFile(fileTxt.PathAndFileName, test);
            Assert.AreEqual(false, File.Exists(fileTxt.PathAndFileName));
        }
    }
}