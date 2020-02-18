using SomeSets;

namespace SetsDemonstration.Model {
    public class Interaction {
        public MySet<ulong> InitializeSet(string text) {
            var temp = new MultiSet(200000);
            temp.AddRange(text);

            var temp2 = new MultiSet(200);
            temp2.AddRange("7 9 12 13");
            return temp + temp2;
        }
    }
}