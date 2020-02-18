using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeSets {
    public abstract class MySet {
        protected abstract object[] GetArray();

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

        public void Print(Action<MySet> printAction) {
            Contract.Assert(printAction != null, "printAction != null");

            printAction(this);
        }

        public static (T[], T[]) MinAndMax<T>(T[] lValue, T[] rValue) {
            Contract.Assert(lValue != null, "lValue != null");
            Contract.Assert(rValue != null, "rValue != null");

            return lValue.Length < rValue.Length ? (lValue, rValue) : (rValue, lValue);
        }

        protected static MySet OperatorBase<T>(Func<object, object, object> operation, T lValue, T rValue)
            where T : MySet {
            Contract.Assert(lValue != null, "lValue != null");
            Contract.Assert(rValue != null, "rValue != null");

            var (minArray, maxArray) = MinAndMax(lValue.GetArray(), rValue.GetArray());
            MySet result = GenerateNew(typeof(T), maxArray.Length);
            long i = 0;
            for (; i < minArray.Length; i++) {
                result.GetArray()[i] = operation(minArray[i], minArray[i]);
            }

            for (; i < maxArray.Length; i++) {
                result.GetArray()[i] = maxArray[i];
            }

            return result;
        }

        public static MySet GenerateNew(Type T, long size) {
            return (MySet) Activator.CreateInstance(T, size);
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