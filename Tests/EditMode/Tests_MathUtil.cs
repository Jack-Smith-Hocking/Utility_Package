using NUnit.Framework;
using UnityEngine;
using Jack.Utility;
using NUnit.Framework.Constraints;

namespace Tests
{
    public class Tests_MathUtil
    {
        public static float TestFloatDelta => 0.00001f;

        #region ApproxEqual
        private bool AreApproxEqual(float expected, float actual)
        {
            return MathUtil.InRange(expected, actual - TestFloatDelta, actual + TestFloatDelta);
        }
        private bool AreApproxEqual(Vector3 expected, Vector3 actual)
        {
            bool _areEqual = AreApproxEqual(expected.x, actual.x);
            _areEqual = _areEqual && AreApproxEqual(expected.y, actual.y);
            _areEqual = _areEqual && AreApproxEqual(expected.z, actual.z);

            return _areEqual;
        }
        #endregion

        [Test]
        public void Test_Rotate_Vector_By_Degrees()
        {
            void TestRotationByDegrees(float angle, Vector3 dir, Vector3 expected)
            {
                Vector3 _rot = MathUtil.RotateBy(angle, Vector3.up, dir);
                bool _rotPassed = AreApproxEqual(expected, _rot);

                Assert.IsTrue(_rotPassed);
            }

            TestRotationByDegrees(0, Vector3.left, Vector3.left);
            TestRotationByDegrees(90, Vector3.left, Vector3.forward);
            TestRotationByDegrees(180, Vector3.left, Vector3.right);
            TestRotationByDegrees(270, Vector3.left, Vector3.back);
            TestRotationByDegrees(360, Vector3.left, Vector3.left);
        }

        [Test]
        public void Test_Is_Layer_In_Layer_Mask()
        {
            void TestIsInLayerMask(int layer, int mask, bool expected)
            {
                Assert.AreEqual(expected, MathUtil.InLayerMask(layer, mask));
            }

            // 1 << 0 -> 0001
            // 5      -> 0111
            TestIsInLayerMask(0, 5, true);

            // 1 << 1 -> 0010
            // 3      -> 0011
            // Expected: True
            TestIsInLayerMask(1, 3, true);

            // 1 << 2 -> 0100
            // 9      -> 1001
            // Expected: False
            TestIsInLayerMask(2, 9, false);

            // 1 << 8 ->  10000000
            // 12     ->  00001100
            // Expected: True
            TestIsInLayerMask(8, 12, false);

            // 1 << 5 -> 0100000
            // 87     -> 1010111
            // Expected: False
            TestIsInLayerMask(5, 87, false);
        }

        [Test]
        public void Test_In_Range()
        {
            // Int
            Assert.IsTrue(MathUtil.InRange(10, 10, 20));
            Assert.IsTrue(MathUtil.InRange(15, 10, 20));
            Assert.IsTrue(MathUtil.InRange(20, 10, 20));

            Assert.IsFalse(MathUtil.InRange(9, 10, 20));
            Assert.IsFalse(MathUtil.InRange(21, 10, 20));

            // Float
            Assert.IsTrue(MathUtil.InRange(10f, 10f, 20f));
            Assert.IsTrue(MathUtil.InRange(15f, 10f, 20f));
            Assert.IsTrue(MathUtil.InRange(20f, 10f, 20f));

            Assert.IsFalse(MathUtil.InRange(9f, 10f, 20f));
            Assert.IsFalse(MathUtil.InRange(21f, 10f, 20f));
        }

        [Test]
        public void Test_Calculate_Direction()
        {
            void TestCalculateDirection(Vector3 start, Vector3 end)
            {
                Vector3 _dir = end - start;
                Vector3 _calcDir = MathUtil.Direction(start, end);

                Assert.IsTrue(AreApproxEqual(_dir, _calcDir));
            }

            TestCalculateDirection(Vector3.zero, Vector3.forward);
            TestCalculateDirection(Vector3.zero, Vector3.back);
            TestCalculateDirection(Vector3.zero, Vector3.left);
            TestCalculateDirection(Vector3.zero, Vector3.right);
        }
    }
}
