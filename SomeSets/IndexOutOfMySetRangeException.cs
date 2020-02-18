using System;
using System.Runtime.Serialization;

namespace SomeSets {
    [Serializable]
    public class IndexOutOfMySetRangeException : Exception {
        public IndexOutOfMySetRangeException() { }
        public IndexOutOfMySetRangeException(string message) : base(message) { }
        public IndexOutOfMySetRangeException(string message, Exception innerException) : base(message, innerException) { }
        protected IndexOutOfMySetRangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}