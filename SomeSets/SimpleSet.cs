using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SomeSets {
    public class SimpleSet : MySet {
        private readonly bool[] _array;

        public SimpleSet(ulong size) {
            Contract.Assert(size < ulong.MaxValue, "size < ulong.MaxValue");

            _array = new bool[size + 1];
        }

        protected override object[] GetArray() {
            return _array.Cast<object>().ToArray();
        }

        public override void Add(ulong value) {
            _array[value] = true;
        }

        public override void Delete(ulong value) {
            _array[value] = false;
        }

        public override bool Exists(ulong value) {
            return _array[value];
        }

        public static SimpleSet operator +(SimpleSet lValue, SimpleSet rValue) {
            return (SimpleSet) OperatorBase((left, right) => (bool) left && (bool) right, lValue, rValue);
        }

        public static SimpleSet operator *(SimpleSet lValue, SimpleSet rValue) {
            return (SimpleSet) OperatorBase((left, right) => (bool) left || (bool) right, lValue, rValue);
        }

        public override string ToString() {
            return base.ToString((ulong) _array.Length);
        }
    }
}