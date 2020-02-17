using System;
using System.Linq;

namespace SomeSets {
    public class SimpleSet : Set {
        private readonly bool[] _array;

        public SimpleSet(int size) {
            _array = new bool[size + 1];
        }

        public override void Add(long value) {
            _array[value] = true;
        }

        public override void Delete(long value) {
            _array[value] = false;
        }

        public override bool Exists(long value) {
            return _array[value];
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