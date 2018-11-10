using System;

namespace ElGamalAlgorithm.Extensions
{
    public static class CharExtensions
    {
        public static uint GetDigit(this char value)
        {
            uint i = (uint)(value - '0');
            if (i > 9) throw new ArgumentOutOfRangeException($"Can not create a digit representation from {value}");
            return i;
        }
     
    }
}
