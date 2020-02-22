using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeSets {
    public abstract class MySet<T> : MySetBase {

        protected abstract T Empty { get; }

        protected abstract T[] GetArray();

        protected abstract ulong MaxAllowedNumber { get; }

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
            for (ulong i = 0; i <= MaxAllowedNumber; i++) {
                if (Exists(i)) {
                    result += i + " ";
                }
            }

            return string.IsNullOrEmpty(result) ? string.Empty : result.Substring(0, result.Length - 1);
        }
    }
}