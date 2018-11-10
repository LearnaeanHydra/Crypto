using System.Collections.Generic;
using ElGamalAlgorithm;
using FluentAssertions;
using NUnit.Framework;

namespace ElGamalAlgorithmTests.BigIntegerTests
{
    public class DivisionTests
    {
        public static IEnumerable<TestCaseData> BigIntegerAdditionTestData
        {
            get
            {
                yield return new TestCaseData(new BigInteger("82312648127348971289312"), 
                    new BigInteger("732183812"), 
                    new BigInteger("112420742958667"));
            }
        }

        [TestCaseSource("BigIntegerAdditionTestData")]
        public void DivisionTest(BigInteger a, BigInteger b, BigInteger expectedResult)
        {
            BigInteger tmp;
            BigInteger.Division(a,b, out tmp).Should().BeEquivalentTo(expectedResult);
        }
    }
}
