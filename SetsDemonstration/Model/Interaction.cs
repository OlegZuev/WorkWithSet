using SomeSets;

namespace SetsDemonstration.Model {
    public class Interaction {
        public SimpleSet InitializeSet(string text) {
            var temp = new SimpleSet(200);
            temp.AddRange(text);

            var temp2 = new SimpleSet(200);
            temp2.AddRange("7 9 12 13");
            return temp + temp2;
        }
    }
}