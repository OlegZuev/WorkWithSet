﻿using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SomeSets {
    public class MultiSet : MySet<ulong> {
        private readonly ulong[] _array;

        public MultiSet(ulong size) {
            if (!(size < ulong.MaxValue))
                throw new IndexOutOfMySetRangeException("size < ulong.MaxValue");

            _array = new ulong[size + 1];
            Empty = 0;
            MaxAllowedNumber = size;
        }

        protected override ulong Empty { get; }

        protected override ulong[] GetArray() {
            return _array;
        }

        protected override ulong MaxAllowedNumber { get; }

        public override void Add(ulong value) {
            _array[value]++;
        }

        public override void Delete(ulong value) {
            if (!(value > 0))
                throw new IndexOutOfMySetRangeException("value > 0");

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
    }
}