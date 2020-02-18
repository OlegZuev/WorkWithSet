using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SomeSets {
    public class MultiSet : MySet {
        private readonly ulong[] _array;

        public MultiSet(ulong size) {
            Contract.Assert(size < ulong.MaxValue, "size < ulong.MaxValue");

            _array = new ulong[size + 1];
        }

        protected override object[] GetArray() {
            return _array.Cast<object>().ToArray();
        }

        public override void Add(ulong value) {
            _array[value]++;
        }

        public override void Delete(ulong value) {
            Contract.Assert(value > 0, "value > 0");

            _array[value]--;
        }

        public override bool Exists(ulong value) {
            return _array[value] > 0;
        }

        public static MultiSet operator +(MultiSet lValue, MultiSet rValue) {
            return (MultiSet) OperatorBase((left, right) => (ulong) left + (ulong) right, lValue, rValue);
        }

        public static MultiSet operator *(MultiSet lValue, MultiSet rValue) {
            return (MultiSet)OperatorBase((left, right) => Math.Min((ulong)left, (ulong)right), lValue, rValue);
        }

        public override string ToString() {
            return base.ToString((ulong) _array.Length);
        }
    }
}