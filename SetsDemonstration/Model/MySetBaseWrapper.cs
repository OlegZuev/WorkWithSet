using System;
using SomeSets;

namespace SetsDemonstration.Model {
    public class MySetBaseWrapper {
        public MySetBase MySet { get; }

        public string Name { get; }

        public MySetBaseWrapper(MySetBase mySet, string name) {
            MySet = mySet;
            Name = name;
        }

        public static Type GetMySetType(string type) {
            return type switch
                {
                "Логическое множество" => typeof(SimpleSet),
                "Битовое множество" => typeof(BitSet),
                "Мультимножество" => typeof(MultiSet),
                _ => throw new ApplicationException($"Типа {type} не существует")
                };
        }
    }
}