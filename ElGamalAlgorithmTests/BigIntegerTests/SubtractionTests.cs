using System.Collections.Generic;
using ElGamalAlgorithm;
using FluentAssertions;
using NUnit.Framework;

namespace ElGamalAlgorithmTests.BigIntegerTests
{
    public class SubtractionTests
    {
        public static IEnumerable<TestCaseData> BigIntegerSubtractionTestData
        {
            get
            {
                yield return new TestCaseData(new BigInteger("31283478216498127"),
                    new BigInteger("31283478216498127"),
                    new BigInteger("0"));

                yield return new TestCaseData(new BigInteger("31283478216498127"),
                    new BigInteger("31283478216498127371293890128390128"),
                    new BigInteger("0"));

                yield return new TestCaseData(new BigInteger("999999999999999999999"),
                    new BigInteger("999999999999999999900"),
                    new BigInteger("99"));

                yield return new TestCaseData(new BigInteger("100000000000000000000"),
                    new BigInteger("1111111111111111111"),
                    new BigInteger("98888888888888888889"));
            }
        }

        [TestCaseSource("BigIntegerSubtractionTestData")]
        public void SubtractionTest(BigInteger a, BigInteger b, BigInteger expectedResult)
        {
            (a - b).Should().BeEquivalentTo(expectedResult);
        }
    }
}
