using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeSets {
    public abstract class MySet {
        public abstract void Add(ulong value);

        public abstract void Delete(ulong value);

        public abstract bool Exists(ulong value);

        public void AddRange(string array) {
            Contract.Assert(array != null);

            array.Split(' ').ToList().ForEach(x => Add(ulong.Parse(x, CultureInfo.InvariantCulture)));
        }

        public void AddRange(ulong[] array) {
            array.ToList().ForEach(Add);
        }

        public void Print(Action<MySet> printAction) {
            Contract.Assert(printAction != null);

            printAction(this);
        }

        public static (T[], T[]) MinAndMax<T>(T[] lValue, T[] rValue) {
            Contract.Assert(lValue != null);
            Contract.Assert(rValue != null);

            return lValue.Length < rValue.Length ? (lValue, rValue) : (rValue, lValue);
        }
    }
}