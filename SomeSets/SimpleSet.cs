﻿using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SomeSets {
    public class SimpleSet : MySet {
        private readonly bool[] _array;

        public SimpleSet(long size) {
            _array = new bool[size + 1];
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
            Contract.Assert(lValue != null);
            Contract.Assert(rValue != null);

            var (minArray, maxArray) = MinAndMax(lValue._array, rValue._array);
            var result = new SimpleSet(maxArray.Length);
            long i = 0;
            for (; i < minArray.Length; i++) {
                result._array[i] = minArray[i] || maxArray[i];
            }

            for (; i < maxArray.Length; i++) {
                result._array[i] = maxArray[i];
            }

            return result;
        }

        public static SimpleSet operator *(SimpleSet lValue, SimpleSet rValue) {
            Contract.Assert(lValue != null);
            Contract.Assert(rValue != null);

            var (minArray, maxArray) = MinAndMax(lValue._array, rValue._array);
            var result = new SimpleSet(maxArray.Length);
            long i = 0;
            for (; i < minArray.Length; i++) {
                result._array[i] = minArray[i] && maxArray[i];
            }

            for (; i < maxArray.Length; i++) {
                result._array[i] = maxArray[i];
            }

            return result;
        }

        public override string ToString() {
            string result = string.Empty;
            for (int i = 0; i < _array.Length; i++) {
                if (_array[i]) {
                    result += i + " ";
                }
            }

            return string.IsNullOrEmpty(result) ? string.Empty : result.Substring(0, result.Length - 1);
        }
    }
}