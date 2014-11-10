namespace InterfaceTests.Net
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public abstract class IDictionaryTest_MSTest
    {
        #region Derived Classes
		// Derive from the abstract class for every Testee:
		
        [TestClass]
        public class A_DictionaryTests : IDictionaryTest_MSTest
        {
            protected override IDictionary<string, object> CreateTestee()
            {
                return new A_Dictionary<string, object>();
            }
        }

        [TestClass]
        public class B_DictionaryTests : IDictionaryTest_MSTest
        {
            protected override IDictionary<string, object> CreateTestee()
            {
                return new B_Dictionary<string, object>();
            }
        }

        #endregion

        #region Test Implementation

        protected abstract IDictionary<string, object> CreateTestee();

        protected KeyValuePair<string, object> CreateEntry(int seed)
        {
            return new KeyValuePair<string, object>("kvp" + seed, seed);
        }

        [TestMethod]
        public void AddKeyValuePair()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);

            target.Add(entry);

            Assert.AreEqual(1, target.Count);
            Assert.IsTrue(target.ContainsKey(entry.Key));
            Assert.AreEqual(entry.Value, target[entry.Key]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDuplicateKeyValuePairShouldThrow()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);

            target.Add(entry);
            target.Add(entry);
        }

        [TestMethod]
        public void AddKeyAndValue()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);

            target.Add(entry.Key, entry.Value);

            Assert.AreEqual(1, target.Count);
            Assert.IsTrue(target.ContainsKey(entry.Key));
            Assert.AreEqual(entry.Value, target[entry.Key]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDuplicateKeyAndValueShouldThrow()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);

            target.Add(entry.Key, entry.Value);
            target.Add(entry.Key, entry.Value);
        }

        [TestMethod]
        public void AfterClearCountShouldBeZero()
        {
            var target = CreateTestee();
            target.Add(CreateEntry(0));
            target.Add(CreateEntry(1));
            target.Clear();

            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        public void ContainsShouldReturnTrueForValidEntry()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);
            Assert.IsTrue(target.Contains(entry));
        }

        [TestMethod]
        public void ContainsShouldReturnFalseForInvalidEntry()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            Assert.IsFalse(target.Contains(entry));
        }

        [TestMethod]
        public void ContainsKeyShouldReturnTrueForValidKey()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);
            Assert.IsTrue(target.ContainsKey(entry.Key));
        }

        [TestMethod]
        public void ContainsKeyShouldReturnFalseForInvalidKey()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            Assert.IsFalse(target.ContainsKey(entry.Key));
        }

        [TestMethod]
        public void CopyToZeroBasedShouldCopyAllElements()
        {
            var target = CreateTestee();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target.Add(entry1);

            var array = new KeyValuePair<string, object>[2];
            target.CopyTo(array, 0);

            Assert.AreEqual(entry0, array[0]);
            Assert.AreEqual(entry1, array[1]);
        }

        [TestMethod]
        public void CopyToNonZeroBasedShouldCopyElementsFromIndex()
        {
            var target = CreateTestee();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target.Add(entry1);

            var array = new KeyValuePair<string, object>[3];
            target.CopyTo(array, 1);

            Assert.AreEqual(entry0, array[1]);
            Assert.AreEqual(entry1, array[2]);
        }

        [TestMethod]
        public void CountOnNewInstanceShouldBeZero()
        {
            var target = CreateTestee();
            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        public void CountAfterAddShouldBeOne()
        {
            var target = CreateTestee();
            target.Add(CreateEntry(0));
            Assert.AreEqual(1, target.Count);
        }

        [TestMethod]
        public void EnumeratorTest()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);

            var enumerator = target.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(entry, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void KeysShouldContainKey()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);
            Assert.AreEqual(1, target.Keys.Count);
            Assert.AreEqual(entry.Key, target.Keys.Single());
        }

        [TestMethod]
        public void KeysShouldContainKeys()
        {
            var target = CreateTestee();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target.Add(entry1);
            Assert.AreEqual(2, target.Keys.Count);
            Assert.AreEqual(entry0.Key, target.Keys.First());
            Assert.AreEqual(entry1.Key, target.Keys.Last());
        }

        [TestMethod]
        public void RemoveKeyValuePairShouldRemoveIt()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);
            target.Remove(entry);
            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        public void RemoveKeyValuePairShouldOnlyRemoveIt()
        {
            var target = CreateTestee();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target.Add(entry1);
            target.Remove(entry0);
            Assert.AreEqual(1, target.Count);
            Assert.IsTrue(target.Contains(entry1));
        }

        [TestMethod]
        public void RemoveKeyShouldRemoveIt()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);
            target.Remove(entry.Key);
            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        public void RemoveKeyShouldOnlyRemoveIt()
        {
            var target = CreateTestee();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target.Add(entry1);
            target.Remove(entry0.Key);
            Assert.AreEqual(1, target.Count);
            Assert.IsTrue(target.Contains(entry1));
        }

        [TestMethod]
        public void TryGetValueShouldReturnTrueAndGetValue()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);

            object value;
            Assert.IsTrue(target.TryGetValue(entry.Key, out value));
            Assert.AreEqual(entry.Value, value);
        }

        [TestMethod]
        public void TryGetValueShouldReturnFalseAndGetDefaultValue()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);

            object value;
            Assert.IsFalse(target.TryGetValue(entry.Key, out value));
            Assert.AreEqual(default(object), value);
        }

        [TestMethod]
        public void IndexerShouldReturnCorrectValue()
        {
            var target = CreateTestee();
            var entry = CreateEntry(0);
            target.Add(entry);

            Assert.AreEqual(entry.Value, target[entry.Key]);
        }


        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void IndexerShouldThrowWithInvalidKey()
        {
            var target = CreateTestee();
            var x = target["INVALIDKEY"];
        }

        #endregion
    }
}
