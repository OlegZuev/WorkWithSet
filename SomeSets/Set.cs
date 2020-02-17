using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeSets {
    public abstract class Set {
        public abstract void Add(long value);

        public abstract void Delete(long value);

        public abstract bool Exists(long value);

        public void AddRange(string array) {
            array.Split(' ').ToList().ForEach(x => Add(x));
        }

        public void AddRange(int[] array) {
            array.ToList().ForEach(x => Add(x));
        }

        public void Print(Action<Set> printAction) {
            printAction(this);
        }
    }
}