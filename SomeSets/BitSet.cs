using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SomeSets {
    public class BitSet : MySet<ulong> {
        private readonly ulong[] _array;
        private const ulong Base64 = 64;

        public BitSet(ulong size) {
            if (size < ulong.MaxValue - Base64)
                throw new IndexOutOfMySetRangeException("size < ulong.MaxValue - Base64");

            _array = new ulong[(size + Base64) / Base64];
            Empty = 0;
            MaxAllowedNumber = size;
        }

        protected override ulong Empty { get; }

        protected override ulong[] GetArray() {
            return _array;
        }

        protected override ulong MaxAllowedNumber { get; }

        public override void Add(ulong value) {
            _array[value / Base64] |= 1UL << (int) (value % Base64);
        }

        public override void Delete(ulong value) {
            _array[value / Base64] &= ~1UL << (int) (value % Base64);
        }

        public override bool Exists(ulong value) {
            return (_array[value / Base64] & (1UL << (int) (value % Base64))) != 0;
        }

        public static BitSet operator +(BitSet lValue, BitSet rValue) {
            return (BitSet) OperatorBase((left, right) => left | right, lValue, rValue);
        }

        public static BitSet operator *(BitSet lValue, BitSet rValue) {
            return (BitSet) OperatorBase((left, right) => left & right, lValue, rValue);
        }
    }
}