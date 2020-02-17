using System;
using System.Diagnostics.Contracts;

namespace SomeSets {
    public class BitSet : MySet {
        private readonly ulong[] _array;
        private const long Base64 = 64;

        public BitSet(long size) {
            _array = new ulong[(size + Base64) / Base64];
        }

        public override void Add(ulong value) {
            _array[value / Base64] |= (uint) (1 << (int) (value % Base64));
        }

        public override void Delete(ulong value) {
            _array[value / Base64] &= ~(uint) (1 << (int) (value % Base64));
        }

        public override bool Exists(ulong value) {
            return (_array[value / Base64] & (uint) (1 << (int) (value % Base64))) != 0;
        }

        public static BitSet operator +(BitSet lValue, BitSet rValue) {
            Contract.Assert(lValue != null);
            Contract.Assert(rValue != null);

            var (minArray, maxArray) = MinAndMax(lValue._array, rValue._array);
            var result = new BitSet(maxArray.Length);
            long i = 0;
            for (; i < minArray.Length; i++) {
                result._array[i] = minArray[i] | maxArray[i];
            }

            for (; i < maxArray.Length; i++) {
                result._array[i] = maxArray[i];
            }

            return result;
        }

        public static BitSet operator *(BitSet lValue, BitSet rValue) {
            Contract.Assert(lValue != null);
            Contract.Assert(rValue != null);

            var (minArray, maxArray) = MinAndMax(lValue._array, rValue._array);
            var result = new BitSet(maxArray.Length);
            long i = 0;
            for (; i < minArray.Length; i++) {
                result._array[i] = minArray[i] & maxArray[i];
            }

            for (; i < maxArray.Length; i++) {
                result._array[i] = maxArray[i];
            }

            return result;
        }

        public override string ToString() {
            string result = string.Empty;
            for (long i = 0; i < _array.Length * Base64; i++) {
                if (Exists((ulong) i)) {
                    result += i + " ";
                }
            }

            return string.IsNullOrEmpty(result) ? string.Empty : result.Substring(0, result.Length - 1);
        }
    }
}