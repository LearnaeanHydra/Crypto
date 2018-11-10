using System.Collections.Generic;
using ElGamalAlgorithm;
using FluentAssertions;
using NUnit.Framework;

namespace ElGamalAlgorithmTests.BigIntegerTests
{
    public class AdditionTests
    {
        public static IEnumerable<TestCaseData> BigIntegerAdditionTestData
        {
            get
            {
                yield return new TestCaseData(new BigInteger("0"), 
                    new BigInteger("21938129830912802123"), 
                    new BigInteger("21938129830912802123"));

                yield return new TestCaseData(new BigInteger("9999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999"),
                    new BigInteger("999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999"),
                    new BigInteger("1009999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999999998"));
            }
        }

        [TestCaseSource("BigIntegerAdditionTestData")]
        public void AdditionTest(BigInteger a, BigInteger b, BigInteger expectedResult)
        {
            (a + b).Should().BeEquivalentTo(expectedResult);
        }

        [TestCaseSource("BigIntegerAdditionTestData")]
        public void AdditionCommutativityTest(BigInteger a, BigInteger b, BigInteger expectedResult)
        {
            (b + a).Should().BeEquivalentTo(expectedResult);
        }


    }
}
