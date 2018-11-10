using System.Linq;

namespace ElGamalAlgorithm.Extensions
{
    public static class StringExtensions
    {
        public static bool IsDigitsOnly(this string stringNumber)
        {
            return stringNumber.All(c => c >= '0' && c <= '9');
        }
    }
}
