using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SomeSets;

namespace SetsDemonstration.Model {
    public static class Interaction {
        public static void AddNewSet(string type, ulong size, ObservableCollection<MySetBaseWrapper> wrappers) {
            var mySet = (MySetBase) Activator.CreateInstance(MySetBaseWrapper.GetMySetType(type), size);
            int counter = wrappers.Count(elem => elem.Name.Contains(type)) + 1;
            wrappers.Add(new MySetBaseWrapper(mySet, type + " " + counter));
        }

        public static void AddValueInSet(string array, MySetBase mySet) {
            mySet.AddRange(array);
        }

        public static void RemoveValueFromSet(string array, MySetBase mySet) {
            array.Split().ToList().ForEach(x => mySet.Delete(ulong.Parse(x, CultureInfo.InvariantCulture)));
        }

        public static void ExistsValueInSet(string array, MySetBase mySet, out int result) {
            result = -1;

            if (string.IsNullOrEmpty(array)) {
                result = 2;
                return;
            }

            foreach (string x in array.Split().ToList()) {
                bool flagExists = mySet.Exists(ulong.Parse(x, CultureInfo.InvariantCulture));
                switch (result) {
                    case -1 when flagExists:
                        result = 2;
                        break;
                    case -1 when !flagExists:
                        result = 0;
                        break;
                    case 0 when flagExists:
                    case 2 when !flagExists:
                        result = 1;
                        break;
                    case 1:
                        return;
                }
            }
        }

        public static void ComputeOperation(string operation, MySetBase lValue, MySetBase rValue, out MySetBase result) {
            Contract.Assert(operation == "+" || operation == "*", $"Операция {operation} не поддерживается");
            Contract.Assert(lValue.GetType() == rValue.GetType(), "Операция для разных типов не поддерживается");

            switch (lValue) {
                case SimpleSet lSet when rValue is SimpleSet rSet:
                    switch (operation) {
                        case "+":
                            result = lSet + rSet;
                            return;
                        case "*":
                            result = lSet * rSet;
                            return;
                    }

                    break;

                case BitSet lSet when rValue is BitSet rSet:
                    switch (operation) {
                        case "+":
                            result = lSet + rSet;
                            return;
                        case "*":
                            result = lSet * rSet;
                            return;
                    }

                    break;

                case MultiSet lMultiSet when rValue is MultiSet rMultiSet:
                    switch (operation) {
                        case "+":
                            result = lMultiSet + lMultiSet;
                            return;
                        case "*":
                            result = lMultiSet * lMultiSet;
                            return;
                    }

                    break;
            }

            throw new ApplicationException($"Операции для типа {lValue.GetType()} не поддерживаются");
        }
    }
}