using NUnit.Framework;
using System.Collections.Generic;
using Organiser;

namespace BornToMove.Organiser.Test {
    
    [TestFixture]
    public class RotateSortTest {
        [Test]
        public void TestEmpty() {
            var list = new List<int>();
            var check = new List<int>(list);
            var sorter = new Sorter();
            sorter.Rotate(list, 0, list.Count-1, new Comparer());
            Assert.AreEqual(list, check, "List should not have changed.");
        }
        
        [Test]
        public void TestSingle() {
            var list = new List<int> {2};
            var check = new List<int>(list);
            var sorter = new Sorter();
            sorter.Rotate(list, 0, list.Count-1, new Comparer());
            Assert.AreEqual(list, check, "List should not have changed.");
        }

        [Test]
        public void TestDoubleSame() {
            var list = new List<int> {2, 2};
            var check = new List<int>(list);
            var sorter = new Sorter();
            sorter.Rotate(list, 0, list.Count-1, new Comparer());
            Assert.AreEqual(list, check, "List should not have changed.");
        }
        
        [Test]
        public void TestTripleSame() {
            var list = new List<int> {2, 2, 2};
            var check = new List<int>(list);
            var sorter = new Sorter();
            sorter.Rotate(list, 0, list.Count-1, new Comparer());
            Assert.AreEqual(list, check, "List should not have changed.");
        }
        
        [Test]
        public void TestSort() {
            var list = new List<int> {13, -5, 7};
            var sorter = new Sorter();
            sorter.Rotate(list, 0, list.Count-1, new Comparer());
            Assert.AreEqual(new List<int>{ -5, 7, 13 }, list, "List should have been sorted.");
        }
    }
}
