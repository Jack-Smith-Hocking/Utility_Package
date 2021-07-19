using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Jack.Utility;
using System.Linq;

namespace Tests
{
    public class Tests_GenUtil
    {
        [Test]
        public void Test_Is_Null()
        {
            int[] _testArr = null;
            Assert.IsTrue(_testArr.IsNull());

            _testArr = new int[2];
            Assert.IsFalse(_testArr.IsNull());
        }

        [Test]
        public void Test_Is_Not_Null()
        {
            int[] _testArr = null;
            Assert.IsFalse(_testArr.IsNotNull());

            _testArr = new int[2];
            Assert.IsTrue(_testArr.IsNotNull());
        }

        [Test]
        public void Test_Flatten_Dictionary_To_List()
        {
            Dictionary<string, int> _dict = new Dictionary<string, int>
            {
                { "test1", 1 },
                { "test2", 2 },
                { "test3", 3 }
            };

            List<KeyValuePair<string, int>> _flatDict = _dict.Flatten().ToList();

            Assert.AreEqual("test1", _flatDict[0].Key);
            Assert.AreEqual("test2", _flatDict[1].Key);
            Assert.AreEqual("test3", _flatDict[2].Key);

            Assert.AreEqual(1, _flatDict[0].Value);
            Assert.AreEqual(2, _flatDict[1].Value);
            Assert.AreEqual(3, _flatDict[2].Value);
        }

        [Test]
        public void Test_Sort_Array()
        {
            int[] _intArr = new int[] { 0, 2, -1, 7, -5 };

            List<int> _intList = _intArr.Sort((a, b) => a.CompareTo(b)).ToList();

            Assert.AreEqual(_intList[0], -5);
            Assert.AreEqual(_intList[1], -1);
            Assert.AreEqual(_intList[2], 0);
            Assert.AreEqual(_intList[3], 2);
            Assert.AreEqual(_intList[4], 7);
        }

        [Test]
        public void Test_Is_Collection_Empty()
        {
            List<int> _list = new List<int> { 0, 2, 3, 4, 5, 6 };
            Assert.IsFalse(_list.IsEmpty());

            _list.Clear();
            Assert.IsTrue(_list.IsEmpty());

            int[] _arr = new int[] { 0, 1, 2, 3, 4 };
            Assert.IsFalse(_arr.IsEmpty());

            _arr = new int[0];
            Assert.IsTrue(_arr.IsEmpty());
        }

        [Test]
        public void Test_Is_Collection_Not_Empty()
        {
            List<int> _list = new List<int> { 0, 2, 3, 4, 5, 6 };
            Assert.IsTrue(_list.IsNotEmpty());

            _list.Clear();
            Assert.IsFalse(_list.IsNotEmpty());

            int[] _arr = new int[] { 0, 1, 2, 3, 4 };
            Assert.IsTrue(_arr.IsNotEmpty());

            _arr = new int[0];
            Assert.IsFalse(_arr.IsNotEmpty());
        }

        [Test]
        public void Test_Operate_On_All_Elements()
        {
            List<int> _list = new List<int>() { 0, 1, 2, 3, 4 };

            int _val = 0;
            _list.OnAll((i) => _val += i);

            Assert.AreEqual(10, _val);
        }

        [Test]
        public void Test_Dictionary_Does_Not_Contain_Key()
        {
            Dictionary<string, int> _dict = new Dictionary<string, int>
            {
                { "test1", 1 },
                { "test2", 2 },
                { "test3", 3 }
            };

            Assert.IsTrue(_dict.DoesNotContainKey("test4"));
            Assert.IsTrue(_dict.DoesNotContainKey("test5"));

            Assert.IsFalse(_dict.DoesNotContainKey("test2"));
            Assert.IsFalse(_dict.DoesNotContainKey("test3"));
        }

        [Test]
        public void Test_Dictionary_Does_Not_Contain_Value()
        {
            Dictionary<string, int> _dict = new Dictionary<string, int>
            {
                { "test1", 1 },
                { "test2", 2 },
                { "test3", 3 }
            };

            Assert.IsTrue(_dict.DoesNotContainValue(4));
            Assert.IsTrue(_dict.DoesNotContainValue(5));

            Assert.IsFalse(_dict.DoesNotContainValue(2));
            Assert.IsFalse(_dict.DoesNotContainValue(3));
        }
    }
}