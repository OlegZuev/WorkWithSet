using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeSets {
    public abstract class MySet<T> {

        protected abstract T Empty { get; }

        protected abstract T[] GetArray();

        public abstract void Add(ulong value);

        public abstract void Delete(ulong value);

        public abstract bool Exists(ulong value);

        public void AddRange(string array) {
            Contract.Assert(array != null, "array != null");

            array.Split(' ').ToList().ForEach(x => Add(ulong.Parse(x, CultureInfo.InvariantCulture)));
        }

        public void AddRange(ulong[] array) {
            array.ToList().ForEach(Add);
        }

        public void Print(Action<MySet<T>> printAction) {
            Contract.Assert(printAction != null, "printAction != null");

            printAction(this);
        }

        protected static MySet<T> OperatorBase<TMySet>(Func<T, T, T> operation, TMySet lValue, TMySet rValue)
            where TMySet : MySet<T> {
            Contract.Assert(lValue != null, "lValue != null");
            Contract.Assert(rValue != null, "rValue != null");

            var lArray = lValue.GetArray();
            var rArray = rValue.GetArray();
            var (minArray, maxArray) = lArray.Length < rArray.Length ? (lArray, rArray) : (rArray, lArray);
            var result = (MySet<T>) Activator.CreateInstance(typeof(TMySet), (ulong) maxArray.Length - 1);

            ulong i = 0;
            for (; i < (ulong) minArray.Length; i++) {
                result.GetArray()[i] = operation(minArray[i], maxArray[i]);
            }

            for (; i < (ulong) maxArray.Length; i++) {
                result.GetArray()[i] = operation(lValue.Empty, maxArray[i]);
            }

            return result;
        }

        public string ToString(ulong size) {
            string result = string.Empty;
            for (ulong i = 0; i < size; i++) {
                if (Exists(i)) {
                    result += i + " ";
                }
            }

            return string.IsNullOrEmpty(result) ? string.Empty : result.Substring(0, result.Length - 1);
        }
    }
}