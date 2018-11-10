//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//
//namespace ElGamalAlgorithm
//{
//    public struct BigIntegerV2
//    {
//        private uint[] _bits;
//
//        private static readonly uint BASE = UInt32.MaxValue;
//        private static readonly uint MAX = UInt32.MaxValue;
//
//
//
//        private static readonly BigIntegerV2 _min = new BigIntegerV2(0);
//        private static readonly BigIntegerV2 _one = new BigIntegerV2(1);
//
//        public BigIntegerV2(uint number)
//        {
//            _bits = new[] {number};
//        }
//
//        public BigIntegerV2(uint[] dwords)
//        {
//            _bits = dwords;
//        }
//
//        public BigIntegerV2(byte[] value)
//        {
//            if (value == null) throw new ArgumentNullException("value");
//
//            int byteCount = value.Length;
//
//            while (byteCount > 0 && value[byteCount - 1] == 0) byteCount--;
//
//            if (byteCount == 0)
//            {
//                _bits = new uint[] { 0 };
//                return;
//            }
//
//            int unalignedBytes = byteCount % 4;
//            int dwordCount = byteCount / 4 + (unalignedBytes == 0 ? 0 : 1);
//            _bits = new uint[dwordCount];
//
//            int curDword, curByte = 3;
//            
//            for (curDword = 0; curDword < dwordCount - (unalignedBytes == 0 ? 0 : 1); curDword++)
//            {
//                int byteInDword = 0;
//                while (byteInDword < 4)
//                {
//                    _bits[curDword] <<= 8;
//                    _bits[curDword] |= value[curByte];
//                    curByte--;
//                    byteInDword++;
//                }
//
//                curByte += 8;
//            }
//
//            if (unalignedBytes != 0)
//            {
//                for (curByte = byteCount - 1; curByte >= byteCount - unalignedBytes; curByte--)
//                {
//                    _bits[curDword] <<= 8;
//                    _bits[curDword] |= value[curByte];
//                }
//            }
//        }
//
//        public static BigIntegerV2 operator +(BigIntegerV2 a, BigIntegerV2 b)
//        {
//            if (a._bits.Length > b._bits.Length)
//            {
//
//            }
//
//            List<uint> tmp = new List<uint>();
//           
//
//
//            bool carry = false;
//            for (int i = 0; i < b._bits.Length; i++)
//            {
//                if (MAX - b._bits[i] - (carry ? 1 : 0) < a._bits[i])
//                {
//                    tmp.Add(unchecked( (uint)(a._bits[i] + b._bits[i] + (carry ? 1 : 0))) );
//                    carry = true;
//                }
//                else
//                {
//                    tmp.Add(unchecked((uint)(a._bits[i] + b._bits[i] + (carry ? 1 : 0))));
//                }
//            }
//
//            return new BigIntegerV2(tmp.ToArray());
//
//        }
//
//    }
//}
