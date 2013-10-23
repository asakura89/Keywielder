using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyWielder.Test
{
    [TestClass]
    public class KeyWielderTest
    {
        private String firstKey;
        private String secondKey;
        private String thirdKey;

        [TestInitialize]
        public void Initialize()
        {
            KeyWielder.KeyWielderConfig
                firstKeyPart1 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.STRING, value = "GRE", valueLength = 3 },
                firstKeyPart2 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.YEAR, valueLength = 4 },
                firstKeyPart3 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.MONTH, valueLength = 2 },
                firstKeyPart4 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.DATE, valueLength = 2 },
                firstKeyPart5 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.COUNTER, valueLength = 4, replacementChar = "0", currentCounterValue = 12 };

            var firstKeyConfigList = new List<KeyWielder.KeyWielderConfig> { firstKeyPart1, firstKeyPart2, firstKeyPart3, firstKeyPart4, firstKeyPart5 };
            firstKey = KeyWielder.BuildKey(firstKeyConfigList);
            Debug.WriteLine(firstKey);

            KeyWielder.KeyWielderConfig
                secondKeyPart1 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.STRING, value = "GRE", valueLength = 3, backSeparator = "/" },
                secondKeyPart2 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.YEAR, valueLength = 4, backSeparator = "-" },
                secondKeyPart3 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.MONTH, valueLength = 2, backSeparator = "-" },
                secondKeyPart4 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.DATE, valueLength = 2, backSeparator = "/" },
                secondKeyPart5 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.COUNTER, valueLength = 4, replacementChar = "0", currentCounterValue = 237 };

            var secondKeyConfigList = new List<KeyWielder.KeyWielderConfig> { secondKeyPart1, secondKeyPart2, secondKeyPart3, secondKeyPart4, secondKeyPart5 };
            secondKey = KeyWielder.BuildKey(secondKeyConfigList);
            Debug.WriteLine(secondKey);

            KeyWielder.KeyWielderConfig
                thirdKeyPart1 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.STRING, value = "ASAKURA", valueLength = 7 },
                thirdKeyPart2 = new KeyWielder.KeyWielderConfig { keyFormat = KeyWielder.KeyType.COUNTER, valueLength = 2, replacementChar = "0", currentCounterValue = 77, counterIncrement = 12 };

            var thirdKeyConfigList = new List<KeyWielder.KeyWielderConfig> { thirdKeyPart1, thirdKeyPart2 };
            thirdKey = KeyWielder.BuildKey(thirdKeyConfigList);
            Debug.WriteLine(thirdKey);
        }

        [TestMethod]
        public void BuildKeyTest()
        {
            Assert.AreEqual("GRE201310230013", firstKey); // change expectation to today's date
            Assert.AreEqual("GRE/2013-10-23/0238", secondKey); // change expectation to today's date
            Assert.AreEqual("ASAKURA89", thirdKey);
        }

        [TestMethod]
        public void KeyUniquenessTest()
        {
            Assert.AreNotEqual(firstKey, secondKey);
            Assert.AreNotEqual(secondKey, thirdKey);
            Assert.AreNotEqual(firstKey, thirdKey);
        }
    }
}