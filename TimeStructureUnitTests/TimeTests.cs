using TimeStructures;

namespace TimeStructureUnitTests
{
    [TestClass]
    public class UnitTestsTimeConstructors
    {
        private static byte defaultValue = 0;

        private void AssertTime(Time t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, t.Hours);
            Assert.AreEqual(expectedM, t.Minutes);
            Assert.AreEqual(expectedS, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Time t = new Time();

            AssertTime(t, defaultValue, defaultValue, defaultValue);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)23, (byte)0, (byte)59)]
        [DataRow((byte)0, (byte)59, (byte)59, (byte)0, (byte)59, (byte)59)]
        [DataRow((byte)0, (byte)0, (byte)59, (byte)0, (byte)0, (byte)59)]
        public void Constructor_3params(byte h, byte m, byte s,
                                        byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(h, m, s);

            AssertTime(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)23, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)23, (byte)0)]
        [DataRow((byte)0, (byte)59, (byte)0, (byte)59)]
        public void Constructor_2params(byte h, byte m, byte expectedH, byte expectedM)
        {
            Time t = new Time(h, m);

            AssertTime(t, expectedH, expectedM, defaultValue);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0)]
        [DataRow((byte)23, (byte)23)]
        public void Constructor_1param(byte h, byte expectedH)
        {
            Time t = new Time(h);

            AssertTime(t, expectedH, defaultValue, defaultValue);
        }

        public static IEnumerable<object[]> DataSet_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] { (byte)24, (byte)0, (byte)0 },
            new object[] { (byte)0, (byte)60, (byte)0 },
            new object[] { (byte)0, (byte)0, (byte)60 },
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            Time t = new Time(h, m, s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("00:00:01", (byte)0, (byte)0, (byte)1)]
        [DataRow("00:01:00", (byte)0, (byte)1, (byte)0)]
        [DataRow("01:00:00", (byte)1, (byte)0, (byte)0)]
        [DataRow("00:00:11", (byte)0, (byte)0, (byte)11)]
        [DataRow("00:11:00", (byte)0, (byte)11, (byte)0)]
        [DataRow("11:00:00", (byte)11, (byte)0, (byte)0)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        public void Constructor_1param_string(string s, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(s);

            AssertTime(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(null)]
        [DataRow("")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ArgumentNullException(string s)
        {
            Time t = new Time(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("abcdefg")]
        [DataRow("aa:aa:aa")]
        [DataRow("-1:00:00")]
        [DataRow("24:00:00")]
        [DataRow("00:60:00")]
        [DataRow("00:00:60")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_FormatException(string s)
        {
            Time t = new Time(s);
        }

    }

    [TestClass]
    public class UnitTestsTimeToString
    {
        [DataTestMethod, TestCategory("String representation")]
        [DataRow("00:00:00")]
        [DataRow("00:00:01")]
        [DataRow("00:01:00")]
        [DataRow("01:00:00")]
        [DataRow("00:00:11")]
        [DataRow("00:11:00")]
        [DataRow("11:00:00")]
        [DataRow("23:59:59")]
        public void ToString_Test(string s)
        {
            Time t = new Time(s);

            Assert.AreEqual(t.ToString(), s);
        }
    }

    [TestClass]
    public class UnitTestsTimeEquals
    {
        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59)]
        [DataRow((byte)0, (byte)59, (byte)59)]
        [DataRow((byte)0, (byte)0, (byte)59)]
        public void Equals_Test(byte h, byte m, byte s)
        {
            Time l = new Time(h, m, s);
            Time r = new Time(h, m, s);

            Assert.IsTrue(l.Equals(r));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)0, (byte)59, (byte)59)]
        public void Equals_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            Assert.IsFalse(l.Equals(r));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59)]
        [DataRow((byte)0, (byte)59, (byte)59)]
        [DataRow((byte)0, (byte)0, (byte)59)]
        public void Equals_Operator_Test(byte h, byte m, byte s)
        {
            Time l = new Time(h, m, s);
            Time r = new Time(h, m, s);

            Assert.IsTrue(l == r);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)0, (byte)59, (byte)59)]
        public void Equals_Operator_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            Assert.IsFalse(l == r);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)0, (byte)59, (byte)59)]
        public void Not_Equals_Operator_True_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            Assert.IsTrue(l != r);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59)]
        [DataRow((byte)0, (byte)59, (byte)59)]
        [DataRow((byte)0, (byte)0, (byte)59)]
        public void Not_Equals_Operator_False_Test(byte h, byte m, byte s)
        {
            Time l = new Time(h, m, s);
            Time r = new Time(h, m, s);

            Assert.IsFalse(l != r);
        }

    }

    [TestClass]
    public class UnitTestsTimeCompare
    {
        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        public void Smaller_Than_Operator_True_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            Assert.IsTrue(l < r);
        }

        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        public void Smaller_Than_Operator_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h2, m2, s2);
            Time r = new Time(h1, m1, s1);

            Assert.IsFalse(l < r);
        }

        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        public void Greater_Than_Operator_True_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h2, m2, s2);
            Time r = new Time(h1, m1, s1);

            Assert.IsTrue(l > r);
        }

        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        public void Greater_Than_Operator_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            Assert.IsFalse(l > r);
        }

        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)23)]
        public void Smaller_Than_Or_Equal_Operator_True_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            Assert.IsTrue(l <= r);
        }

        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        //[DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)23)]
        public void Smaller_Than_Or_Equal_Operator_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h2, m2, s2);
            Time r = new Time(h1, m1, s1);

            Assert.IsFalse(l <= r);
        }

        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)23)]
        public void Greater_Than_Or_Equal_Operator_True_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h2, m2, s2);
            Time r = new Time(h1, m1, s1);

            Assert.IsTrue(l >= r);
        }

        [DataTestMethod, TestCategory("Compare")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)0, (byte)0, (byte)23, (byte)1, (byte)0)]
        [DataRow((byte)23, (byte)23, (byte)0, (byte)23, (byte)23, (byte)23)]
        [DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)59)]
        //[DataRow((byte)23, (byte)23, (byte)23, (byte)23, (byte)23, (byte)23)]
        public void Greater_Than_Or_Equal_Operator_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            Assert.IsFalse(l >= r);
        }
    }

    [TestClass]
    public class UnitTestsTimeArithmetic 
    {
        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("00:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("01:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("01:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("23:59:58", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Plus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = l.Plus(r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("00:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("01:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("01:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("23:59:58", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Static_Plus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = Time.Plus(l, r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("23:59:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("23:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("22:58:59", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("00:00:01", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("00:00:01", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("00:00:00", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Minus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = l.Minus(r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("23:59:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("23:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("22:58:59", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("00:00:01", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("00:00:01", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("00:00:00", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Static_Minus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = Time.Minus(l, r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("00:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("01:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("01:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("23:59:58", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Plus_Operator_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            var check = l + r;

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("23:59:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("23:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("22:58:59", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("00:00:01", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("00:00:00", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Minus_Operator_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            Time l = new Time(h1, m1, s1);
            Time r = new Time(h2, m2, s2);

            var check = l - r;

            Assert.AreEqual(result, check.ToString());
        }
    }
}