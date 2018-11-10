using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ElGamalAlgorithm.Exceptions;
using ElGamalAlgorithm.Extensions;

namespace ElGamalAlgorithm
{
    public class BigInteger
    {

        private readonly uint[] _digits;
        public uint this[int index] => _digits[index];
        public int DigitsCount => _digits.Length;
        public List<uint> Digits => _digits.ToList();
        public bool IsZero => _digits.Length == 1 && _digits[0] == 0;

        #region constructors

        public BigInteger(string stringNumber)
        {
            if(!stringNumber.IsDigitsOnly()) throw new InvalidArgumentException($"Given string {stringNumber} contains non numerical characters");
            _digits = new uint[stringNumber.Length];
            for (int i = 0; i < stringNumber.Length; i++)
            {
                _digits[i] = stringNumber[stringNumber.Length - 1 - i].GetDigit();
            }
        }

        private BigInteger(uint[] digits)
        {
//            if (!digits.All(digit => digit <= 9)) throw new InvalidArgumentException($"Given array {digits} contains number greater than 9");
            _digits = new uint[digits.Length];
            Array.Copy(digits, _digits, _digits.Length);
        }

        #endregion

        #region comparisonOperators

        public static bool operator <=(BigInteger a, BigInteger b)
        {
            if (a.DigitsCount < b.DigitsCount) return true;
            if (a.DigitsCount > b.DigitsCount) return false;

            for (int i = a.DigitsCount - 1; i >= 0; i--)
            {
                if (a[i] > b[i]) return false;
                if (a[i] < b[i]) return true;
            }

            return true;
        }

        public static bool operator >=(BigInteger a, BigInteger b)
        {
            if (a.DigitsCount > b.DigitsCount) return true;
            if (a.DigitsCount < b.DigitsCount) return false;

            for (int i = a.DigitsCount - 1; i>=0; i--)
            {
                if (a[i] < b[i]) return false;
                if (a[i] > b[i]) return true;
            }

            return true;
        }

        public static bool operator <(BigInteger a, BigInteger b)
        {
            return !(a >= b);
        }

        public static bool operator >(BigInteger a, BigInteger b)
        {
            return !(a <= b);
        }

        public static bool operator ==(BigInteger a, BigInteger b)
        {
            if (a.DigitsCount != b.DigitsCount) return false;
            for (int i = 0; i < a.DigitsCount; i++)
            {
                if (a[i] != b[i]) return false;
            }

            return true;
        }

        public static bool operator !=(BigInteger a, BigInteger b)
        {
            return !(a == b);
        }

        #endregion

        #region mathematicalOperations

        public static BigInteger Division(BigInteger dividend, BigInteger divisor, out BigInteger remainder)
        {
            if (dividend < divisor)
            {
                remainder = new BigInteger(dividend.Digits.ToArray());
                return new BigInteger(new uint[] {0});
            }

            BigInteger tmpBig;
            int i = dividend.DigitsCount - divisor.DigitsCount;
            List<uint> divisorDigits = divisor.Digits;
            List<uint> tmp = dividend.Digits.Skip(i).ToList();
            i--;

            if (!tmp.IsGreaterOrEqualThan(divisorDigits))
            {
                tmp.Insert(0, dividend[i]);
                i--;
            }

            tmpBig = new BigInteger(tmp.ToArray());
            
            
            List<uint> result = new List<uint>();
            for (; i >= 0 || tmpBig > divisor; )
            {
                uint counter = 0;
                while (tmpBig >= divisor)
                {
                    tmpBig -= divisor;
                    counter++;
                }
                result.Insert(0, counter);
                

                tmp = tmpBig.Digits;
                if (i >= 0)
                {
                    tmp.Insert(0, dividend[i]);
                    i--;
                }
                
                while (i >= 0 && !tmp.IsGreaterOrEqualThan(divisorDigits))
                {
                    tmp.Insert(0, dividend[i]);
                    result.Insert(0, 0);
                    i--;
                }

                tmpBig = new BigInteger(tmp.ToArray());
                if (!tmp.IsGreaterOrEqualThan(divisorDigits)) break;                
            }

            remainder = tmpBig;
            return new BigInteger(result.ToArray());

        } 

//        public static BigInteger PowMod(BigInteger a, BigInteger pow, BigInteger mod)
//        {
//
//        }
//
//        public static BigInteger Mod(BigInteger a, BigInteger mod)
//        {
//            if(a < mod) return new BigInteger(a.Digits.ToArray());
//        }

        public static BigInteger operator *(BigInteger a, BigInteger b)
        {
            if(a.IsZero || b.IsZero) return new BigInteger(new uint[] {0});
            return a > b ? Multiplication(a, b) : Multiplication(b, a);
        }

        private static BigInteger Multiplication(BigInteger greaterNumber, BigInteger anotherNumber)
        {
            List<uint> result = new List<uint>(new uint[greaterNumber.DigitsCount + anotherNumber.DigitsCount - 1]);

            int i;
            uint carry = 0;
            for (i = 0; i < anotherNumber.DigitsCount; i++)
            {
                for (int j = 0; j < greaterNumber.DigitsCount; j++)
                {
                    result[i + j] += anotherNumber[i] * greaterNumber[j];
                }

                result[i] += carry;
                if (result[i] > 9)
                {
                    carry = result[i] / 10;
                    result[i] = result[i] % 10;
                }
                else
                {
                    carry = 0;
                }
            }

            for ( ; i < result.Count; i++)
            {
                result[i] += carry;
                if (result[i] > 9)
                {
                    carry = result[i] / 10;
                    result[i] = result[i] % 10;
                }
                else
                {
                    carry = 0;
                }
            }

            while (carry > 0)
            {
                result.Add(carry % 10);
                carry = carry / 10;
            }
            
            return new BigInteger(result.ToArray());
        }

        public static BigInteger operator -(BigInteger a, BigInteger b)
        {
            return a > b ? Subtraction(a, b) : new BigInteger(new uint[] {0});
        }

        private static BigInteger Subtraction(BigInteger greaterNumber, BigInteger anotherNumber)
        {
            List<uint> result = greaterNumber.Digits;
            int i;
            uint borrow = 0;
            for (i = 0; i < anotherNumber.DigitsCount; i++)
            {
                uint toSubtract = anotherNumber[i] + borrow;
                if (toSubtract > result[i])
                {
                    borrow = 1;
                    result[i] += 10;
                }
                else
                {
                    borrow = 0;
                }

                result[i] -= toSubtract;
            }

            while (borrow != 0)
            {
                if (result[i] == 0)
                {
                    result[i] = 9; //result[i] = 0; borrow = 1; result[i] + 10 - borrow = 9
                    i++;
                }
                else
                {
                    result[i] = result[i] - 1;
                    borrow = 0;
                }
            }

            while (result[result.Count - 1] == 0)
            {
                result.RemoveAt(result.Count - 1);
            }

            return new BigInteger(result.ToArray());
        }

        public static BigInteger operator +(BigInteger a, BigInteger b)
        {
            return a > b ? Add(a, b) : Add(b, a);
        }

        private static BigInteger Add(BigInteger greaterNumber, BigInteger anotherNumber)
        {
            List<uint> result = greaterNumber.Digits;

            int i;
            uint carry = 0;
            for (i = 0; i < anotherNumber.DigitsCount; i++)
            {
                result[i] += (anotherNumber[i] + carry);

                if (result[i] > 9)
                {
                    result[i] -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
            }

            for (; i < result.Count; i++)
            {
                result[i] += carry;
                if (result[i] > 9)
                {
                    result[i] -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }
            }

            if (carry == 1) result.Add(1);

            return new BigInteger(result.ToArray());
        }

        #endregion
    }
}
