using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SomeSets {
    public class MultiSet : MySet<ulong> {
        private readonly ulong[] _array;

        public MultiSet(ulong size) {
            Contract.Requires<IndexOutOfMySetRangeException>(size < ulong.MaxValue, "size < ulong.MaxValue");

            _array = new ulong[size + 1];
            Empty = 0;
        }

        protected override ulong Empty { get; }

        protected override ulong[] GetArray() {
            return _array;
        }

        public override void Add(ulong value) {
            _array[value]++;
        }

        public override void Delete(ulong value) {
            Contract.Requires<IndexOutOfMySetRangeException>(value > 0, "value > 0");

            _array[value]--;
        }

        public override bool Exists(ulong value) {
            return _array[value] > 0;
        }

        public static MultiSet operator +(MultiSet lValue, MultiSet rValue) {
            return (MultiSet) OperatorBase((left, right) => left + right, lValue, rValue);
        }

        public static MultiSet operator *(MultiSet lValue, MultiSet rValue) {
            return (MultiSet) OperatorBase(Math.Min, lValue, rValue);
        }

        public override string ToString() {
            return base.ToString((ulong) _array.Length);
        }
    }
}