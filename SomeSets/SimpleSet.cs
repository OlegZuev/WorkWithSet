using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace SomeSets {
    public class SimpleSet : MySet<bool> {
        private readonly bool[] _array;

        public SimpleSet(ulong size) {
            if (!(size < ulong.MaxValue))
                throw new IndexOutOfMySetRangeException("size < ulong.MaxValue");

            _array = new bool[size + 1];
            Empty = false;
            MaxAllowedNumber = size;
        }

        protected override bool Empty { get; }

        protected override bool[] GetArray() {
            return _array;
        }

        protected override ulong MaxAllowedNumber { get; }

        public override void Add(ulong value) {
            if (value > MaxAllowedNumber)
                throw new IndexOutOfMySetRangeException($"Недопустимое значение. Принимаются только натуральные числа до {MaxAllowedNumber}");

            _array[value] = true;
        }

        public override void Delete(ulong value) {
            if (value > MaxAllowedNumber)
                throw new IndexOutOfMySetRangeException($"Недопустимое значение. Принимаются только натуральные числа до {MaxAllowedNumber}");

            _array[value] = false;
        }

        public override bool Exists(ulong value) {
            if (value > MaxAllowedNumber)
                throw new IndexOutOfMySetRangeException($"Недопустимое значение. Принимаются только натуральные числа до {MaxAllowedNumber}");

            return _array[value];
        }

        public static SimpleSet operator +(SimpleSet lValue, SimpleSet rValue) {
            return (SimpleSet) OperatorBase((left, right) => left || right, lValue, rValue);
        }

        public static SimpleSet operator *(SimpleSet lValue, SimpleSet rValue) {
            return (SimpleSet) OperatorBase((left, right) => left && right, lValue, rValue);
        }
    }
}