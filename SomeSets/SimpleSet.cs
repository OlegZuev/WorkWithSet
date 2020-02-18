using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace SomeSets {
    public class SimpleSet : MySet<bool> {
        private readonly bool[] _array;

        public SimpleSet(ulong size) {
            Contract.Requires<IndexOutOfMySetRangeException>(size < ulong.MaxValue, "size < ulong.MaxValue");

            _array = new bool[size + 1];
            Empty = false;
        }

        protected override bool Empty { get; }

        protected override bool[] GetArray() {
            return _array;
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
            return (SimpleSet) OperatorBase((left, right) => left || right, lValue, rValue);
        }

        public static SimpleSet operator *(SimpleSet lValue, SimpleSet rValue) {
            return (SimpleSet) OperatorBase((left, right) => left && right, lValue, rValue);
        }

        public override string ToString() {
            return base.ToString((ulong) _array.Length);
        }
    }
}