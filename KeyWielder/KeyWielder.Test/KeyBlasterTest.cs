using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyWielder.Test
{
    [TestClass]
    public class KeyBlasterTest
    {
        private readonly String firstSimpleKey = KeyBlaster.BuildSimpleKey();
        private readonly String secondSimpleKey = KeyBlaster.BuildSimpleKey();

        private readonly String firstComplexKey = KeyBlaster.BuildComplexKey();
        private readonly String secondComplexKey = KeyBlaster.BuildComplexKey();

        [TestMethod]
        public void SimpleKeyTest()
        {
            Boolean isFirstKeyContainsAlphanumeric = Regex.IsMatch(firstSimpleKey, @"^[a-zA-Z0-9]+$");
            Boolean isSecondKeyContainsAlphanumeric = Regex.IsMatch(secondSimpleKey, @"^[a-zA-Z0-9]+$");
            Assert.IsTrue(isFirstKeyContainsAlphanumeric);
            Assert.IsTrue(isSecondKeyContainsAlphanumeric);
        }

        [TestMethod]
        public void DefaultSimpleKeyLengthTest()
        {
            Boolean isFirstKeyDefaultLengthSet = firstSimpleKey.Length == 8;
            Boolean isSecondKeyDefaultLengthSet = secondSimpleKey.Length == 8;
            Assert.IsTrue(isFirstKeyDefaultLengthSet);
            Assert.IsTrue(isSecondKeyDefaultLengthSet);
        }

        [TestMethod]
        public void CustomSimpleKeyTest()
        {
            String customKey = KeyBlaster.BuildSimpleKey(6, KeyBlaster.SimpleKeyType.NUMBER);
            Boolean isLengthEqualsToSix = customKey.Length == 6;
            Boolean isKeyConstainsNumber = Regex.IsMatch(customKey, @"^[0-9]+$");
            Assert.IsTrue(isLengthEqualsToSix);
            Assert.IsTrue(isKeyConstainsNumber);

            String secondCustomKey = KeyBlaster.BuildSimpleKey(12, KeyBlaster.SimpleKeyType.TEXT);
            Boolean isLengthEqualsToTwelve = secondCustomKey.Length == 12;
            Boolean isKeyConstainsText = Regex.IsMatch(secondCustomKey, @"^[a-zA-Z]+$");
            Assert.IsTrue(isLengthEqualsToTwelve);
            Assert.IsTrue(isKeyConstainsText);
        }

        [TestMethod]
        public void SimpleKeyUniquenessTest()
        {
            Assert.AreNotEqual(firstSimpleKey, secondSimpleKey);
        }

        [TestMethod]
        public void ComplexKeyUniquessTest()
        {
            Assert.AreNotEqual(firstComplexKey, secondComplexKey);
        }
    }
}