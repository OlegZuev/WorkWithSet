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

        protected abstract ulong MaxAllowedNumber { get; }

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

            
            (TMySet minValue, TMySet maxValue) = lValue.MaxAllowedNumber < rValue.MaxAllowedNumber ? (lValue, rValue) : (rValue, lValue);
            var minArray = minValue.GetArray();
            var maxArray = maxValue.GetArray();
            var result = (MySet<T>) Activator.CreateInstance(typeof(TMySet), maxValue.MaxAllowedNumber);

            ulong i = 0;
            for (; i < (ulong) minArray.Length; i++) {
                result.GetArray()[i] = operation(minArray[i], maxArray[i]);
            }

            for (; i < (ulong) maxArray.Length; i++) {
                result.GetArray()[i] = operation(lValue.Empty, maxArray[i]);
            }

            return result;
        }

        public override string ToString() {
            string result = string.Empty;
            for (ulong i = 0; i < MaxAllowedNumber; i++) {
                if (Exists(i)) {
                    result += i + " ";
                }
            }

            return string.IsNullOrEmpty(result) ? string.Empty : result.Substring(0, result.Length - 1);
        }
    }
}