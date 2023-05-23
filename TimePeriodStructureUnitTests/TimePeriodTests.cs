using TimeStructures;

namespace TimePeriodStructureUnitTests
{
    [TestClass]
    public class UnitTestsTimePeriodConstructors
    {
        private static byte defaultValue = 0;

        private void AssertTimePeriod(TimePeriod t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, t.Hours);
            Assert.AreEqual(expectedM, t.Minutes);
            Assert.AreEqual(expectedS, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            TimePeriod t = new TimePeriod();

            AssertTimePeriod(t, defaultValue, defaultValue, defaultValue);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)23, (byte)0, (byte)59)]
        [DataRow((byte)0, (byte)59, (byte)59, (byte)0, (byte)59, (byte)59)]
        [DataRow((byte)0, (byte)0, (byte)59, (byte)0, (byte)0, (byte)59)]
        [DataRow((byte)48, (byte)0, (byte)59, (byte)48, (byte)0, (byte)59)]
        public void Constructor_3params(byte h, byte m, byte s,
                                        byte expectedH, byte expectedM, byte expectedS)
        {
            TimePeriod t = new TimePeriod(h, m, s);

            AssertTimePeriod(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)23, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)23, (byte)0)]
        [DataRow((byte)0, (byte)59, (byte)0, (byte)59)]
        [DataRow((byte)48, (byte)59, (byte)48, (byte)59)]
        public void Constructor_2params(byte h, byte m, byte expectedH, byte expectedM)
        {
            TimePeriod t = new TimePeriod(h, m);

            AssertTimePeriod(t, expectedH, expectedM, defaultValue);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, (byte)0, (byte)0, (byte)0)]
        [DataRow(60, (byte)0, (byte)1, (byte)0)]
        [DataRow(3600, (byte)1, (byte)0, (byte)0)]
        [DataRow(7200, (byte)2, (byte)0, (byte)0)]
        [DataRow(86400, (byte)24, (byte)0, (byte)0)]
        public void Constructor_1param(long s, byte expectedH, byte expectedM, byte expectedS)
        {
            TimePeriod t = new TimePeriod(s);

            AssertTimePeriod(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_ArgumentOutOfRangeException(long s)
        {
            TimePeriod t = new TimePeriod(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)60, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            TimePeriod t = new TimePeriod(h, m, s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("0:00:01", (byte)0, (byte)0, (byte)1)]
        [DataRow("0:01:00", (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", (byte)1, (byte)0, (byte)0)]
        [DataRow("0:00:11", (byte)0, (byte)0, (byte)11)]
        [DataRow("0:11:00", (byte)0, (byte)11, (byte)0)]
        [DataRow("11:00:00", (byte)11, (byte)0, (byte)0)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        [DataRow("48:59:59", (byte)48, (byte)59, (byte)59)]
        [DataRow("148:59:59", (byte)148, (byte)59, (byte)59)]
        public void Constructor_1param_string(string s, byte expectedH, byte expectedM, byte expectedS)
        {
            TimePeriod t = new TimePeriod(s);

            AssertTimePeriod(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(null)]
        [DataRow("")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ArgumentNullException(string s)
        {
            TimePeriod t = new TimePeriod(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("abcdefg")]
        [DataRow("aa:aa:aa")]
        [DataRow("-1:00:00")]
        [DataRow("00:60:00")]
        [DataRow("00:00:60")]
        [DataRow("00:00:00")]
        [DataRow("01:00:00")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_FormatException(string s)
        {
            TimePeriod t = new TimePeriod(s);
        }
    }

    [TestClass]
    public class UnitTestsTimePeriodToString
    {
        [DataTestMethod, TestCategory("String representation")]
        [DataRow("0:00:00")]
        [DataRow("0:00:01")]
        [DataRow("0:01:00")]
        [DataRow("1:00:00")]
        [DataRow("0:00:11")]
        [DataRow("0:11:00")]
        [DataRow("11:00:00")]
        [DataRow("23:59:59")]
        [DataRow("123:59:59")]
        public void ToString_Test(string s)
        {
            TimePeriod t = new TimePeriod(s);

            Assert.AreEqual(t.ToString(), s);
        }
    }

    [TestClass]
    public class UnitTestsTimePeriodEquals
    {
        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59)]
        [DataRow((byte)0, (byte)59, (byte)59)]
        [DataRow((byte)0, (byte)0, (byte)59)]
        public void Equals_Test(byte h, byte m, byte s)
        {
            TimePeriod l = new TimePeriod(h, m, s);
            TimePeriod r = new TimePeriod(h, m, s);

            Assert.IsTrue(l.Equals(r));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)0, (byte)59, (byte)59)]
        public void Equals_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

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
            TimePeriod l = new TimePeriod(h, m, s);
            TimePeriod r = new TimePeriod(h, m, s);

            Assert.IsTrue(l == r);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)0, (byte)59, (byte)59)]
        public void Equals_Operator_False_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            Assert.IsFalse(l == r);
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)0, (byte)59, (byte)0, (byte)59, (byte)59)]
        public void Not_Equals_Operator_True_Test(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

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
            TimePeriod l = new TimePeriod(h, m, s);
            TimePeriod r = new TimePeriod(h, m, s);

            Assert.IsFalse(l != r);
        }

    }

    [TestClass]
    public class UnitTestsTimePeriodCompare
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
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

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
            TimePeriod l = new TimePeriod(h2, m2, s2);
            TimePeriod r = new TimePeriod(h1, m1, s1);

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
            TimePeriod l = new TimePeriod(h2, m2, s2);
            TimePeriod r = new TimePeriod(h1, m1, s1);

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
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

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
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

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
            TimePeriod l = new TimePeriod(h2, m2, s2);
            TimePeriod r = new TimePeriod(h1, m1, s1);

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
            TimePeriod l = new TimePeriod(h2, m2, s2);
            TimePeriod r = new TimePeriod(h1, m1, s1);

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
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            Assert.IsFalse(l >= r);
        }
    }

    [TestClass]
    public class UnitTestsTimePeriodArithmetic
    {
        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("1:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("47:59:59", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("47:59:58", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Plus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = l.Plus(r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("1:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("47:59:59", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("47:59:58", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Static_Plus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = TimePeriod.Plus(l, r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("1:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("47:59:59", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("0:00:00", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Minus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = l.Minus(r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("1:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("47:59:59", (byte)0, (byte)0, (byte)0, (byte)47, (byte)59, (byte)59)]
        [DataRow("0:00:00", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Static_Minus_Method_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = TimePeriod.Minus(l, r);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", 0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", 1, (byte)0, (byte)1, (byte)0)]
        [DataRow("0:02:00", 2, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", 60, (byte)0, (byte)1, (byte)0)]
        public void Multiply_Method_Test(string result, long n, byte h, byte m, byte s)
        {
            TimePeriod t = new TimePeriod(h, m, s);

            var check = t.Multiply(n);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", 0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", 1, (byte)0, (byte)1, (byte)0)]
        [DataRow("0:02:00", 2, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", 60, (byte)0, (byte)1, (byte)0)]
        public void Static_Multiply_Method_Test(string result, long n, byte h, byte m, byte s)
        {
            TimePeriod t = new TimePeriod(h, m, s);

            var check = TimePeriod.Multiply(t, n);

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("1:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("47:59:58", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Plus_Operator_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = l + r;

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", (byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", (byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
        [DataRow("1:01:01", (byte)0, (byte)0, (byte)0, (byte)1, (byte)1, (byte)1)]
        [DataRow("23:59:59", (byte)0, (byte)0, (byte)0, (byte)23, (byte)59, (byte)59)]
        [DataRow("0:00:00", (byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Minus_Operator_Test(string result, byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            TimePeriod l = new TimePeriod(h1, m1, s1);
            TimePeriod r = new TimePeriod(h2, m2, s2);

            var check = l - r;

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", 0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", 1, (byte)0, (byte)1, (byte)0)]
        [DataRow("0:02:00", 2, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", 60, (byte)0, (byte)1, (byte)0)]
        public void Multiply_Operator_Left_Test(string result, long n, byte h, byte m, byte s)
        {
            TimePeriod t = new TimePeriod(h, m, s);

            var check = t * n;

            Assert.AreEqual(result, check.ToString());
        }

        [DataTestMethod, TestCategory("Arithmetic Operations")]
        [DataRow("0:00:00", 0, (byte)0, (byte)0, (byte)0)]
        [DataRow("0:01:00", 1, (byte)0, (byte)1, (byte)0)]
        [DataRow("0:02:00", 2, (byte)0, (byte)1, (byte)0)]
        [DataRow("1:00:00", 60, (byte)0, (byte)1, (byte)0)]
        public void Multiply_Operator_Right_Test(string result, long n, byte h, byte m, byte s)
        {
            TimePeriod t = new TimePeriod(h, m, s);

            var check = n * t;

            Assert.AreEqual(result, check.ToString());
        }
    }
}