using System.Collections.Generic;

namespace ElGamalAlgorithm.Extensions
{
    public static class ListExtensions
    {
        public static bool IsGreaterOrEqualThan(this List<uint> a, List<uint> b)
        {
            if (a.Count > b.Count) return true;
            if (b.Count > a.Count) return false;

            for (int i = a.Count - 1; i >= 0; i--)
            {
                if (a[i] > b[i]) return true;
                if (b[i] > a[i]) return false;
            }

            return true;
        }
    }
}
