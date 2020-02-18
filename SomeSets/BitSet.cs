using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SomeSets {
    public class BitSet : MySet {
        private readonly ulong[] _array;
        private const long Base64 = 64;

        public BitSet(long size) {
            _array = new ulong[(size + Base64) / Base64];
        }

        protected override object[] GetArray() {
            return _array.Cast<object>().ToArray();
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
            return (BitSet) OperatorBase((left, right) => (bool) left | (bool) right, lValue, rValue);
        }

        public static BitSet operator *(BitSet lValue, BitSet rValue) {
            return (BitSet) OperatorBase((left, right) => (bool) left & (bool) right, lValue, rValue);
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