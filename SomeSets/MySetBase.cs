using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;

namespace SomeSets {
    public abstract class MySetBase {
        public abstract void Add(ulong value);

        public abstract void Delete(ulong value);

        public abstract bool Exists(ulong value);

        public void AddRange(string array) {
            Contract.Assert(array != null, "array != null");

            array.Split().ToList().ForEach(x => {
                if (!ulong.TryParse(x, out ulong result))
                    throw new IndexOutOfMySetRangeException("Недопустимое значение. Принимаются только натуральные числа");
                Add(result);
            });
        }

        public void AddRange(ulong[] array) {
            array.ToList().ForEach(Add);
        }
    }
}