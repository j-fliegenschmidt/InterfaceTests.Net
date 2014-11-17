namespace InterfaceTests.Net
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public abstract class IListTest
    {
        #region Derived Classes

        [TestClass]
        public class ACollectionTest : IListTest
        {
            protected override IList<object> CreateTarget()
            {
                return new ACollection<object>();
            }
        }

        [TestClass]
        public class BCollectionTest : IListTest
        {
            protected override IList<object> CreateTarget()
            {
                return new BCollection<object>();
            }
        }

        #endregion

        #region Test Implementation

        protected abstract IList<object> CreateTarget();

        protected object CreateEntry(int seed)
        {
            return new KeyValuePair<string, object>("kvp" + seed, seed);
        }

        [TestMethod]
        public void AddShouldAddItem()
        {
            var target = CreateTarget();
            var entry = CreateEntry(0);
            target.Add(entry);
            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(entry, target[0]);
        }

        [TestMethod]
        public void ClearShouldRemoveAllEntries()
        {
            var target = CreateTarget();
            var entry = CreateEntry(0);

            target.Add(entry);

            target.Clear();
            Assert.AreEqual(0, target.Count);
            Assert.IsFalse(target.Contains(entry));
        }

        [TestMethod]
        public void WhenListContainsItemContainsShouldBeTrue()
        {
            var target = CreateTarget();
            var entry = CreateEntry(0);

            target.Add(entry);

            Assert.IsTrue(target.Contains(entry));
        }

        [TestMethod]
        public void WhenListDoesNotContainItemContainsShouldBeFalse()
        {
            var target = CreateTarget();
            var entry = CreateEntry(0);

            Assert.IsFalse(target.Contains(entry));
        }

        [TestMethod]
        public void NewListShouldHaveCountEqualToZero()
        {
            var target = CreateTarget();

            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        public void ListWithOneItemShouldHaveCountEqualToOne()
        {
            var target = CreateTarget();
            target.Add(CreateEntry(0));

            Assert.AreEqual(1, target.Count);
        }

        [TestMethod]
        public void CopyToZeroBasedShouldCopyAllElements()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target.Add(entry1);

            var array = new object[2];
            target.CopyTo(array, 0);

            Assert.AreEqual(entry0, array[0]);
            Assert.AreEqual(entry1, array[1]);
        }

        [TestMethod]
        public void CopyToNonZeroBasedShouldCopyElementsFromIndex()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target.Add(entry1);

            var array = new object[3];
            target.CopyTo(array, 1);

            Assert.AreEqual(entry0, array[1]);
            Assert.AreEqual(entry1, array[2]);
        }

        [TestMethod]
        public void EnumeratorTest()
        {
            var target = CreateTarget();
            var entry = CreateEntry(0);
            target.Add(entry);

            var enumerator = target.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(entry, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [TestMethod]
        public void IndexOfShouldReturnCorrectValues()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);

            target.Add(entry0);
            target.Add(entry1);

            Assert.AreEqual(0, target.IndexOf(entry0));
            Assert.AreEqual(1, target.IndexOf(entry1));
        }

        [TestMethod]
        public void InsertShouldPutItemAtCorrectIndex()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);

            target.Add(entry1);
            Assert.AreEqual(0, target.IndexOf(entry1));

            target.Insert(0, entry0);

            Assert.AreEqual(0, target.IndexOf(entry0));
            Assert.AreEqual(1, target.IndexOf(entry1));
        }

        [TestMethod]
        public void RemoveShouldRemoveEntry()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);

            target.Add(entry0);
            target.Remove(entry0);
            Assert.IsFalse(target.Contains(entry0));
            Assert.AreEqual(0, target.Count);
        }

        [TestMethod]
        public void RemoveAtShouldRemoveEntryAtCorrectIndex()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);

            target.Add(entry0);
            target.Add(entry1);

            target.RemoveAt(0);
            Assert.AreEqual(1, target.Count);
            Assert.AreEqual(target[0], entry1);
        }

        [TestMethod]
        public void GetIndexerTest()
        {
            var target = CreateTarget();
            var entry = CreateEntry(0);
            target.Add(entry);

            Assert.AreEqual(entry, target[0]);
        }

        [TestMethod]
        public void SetIndexerTest()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);
            var entry1 = CreateEntry(1);
            target.Add(entry0);
            target[0] = entry1;

            Assert.AreEqual(entry1, target[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetIndexerBeyondSizeOfListShouldThrowException()
        {
            var target = CreateTarget();
            var entry0 = CreateEntry(0);

            target[0] = entry0;
        }

        #endregion
    }
}
