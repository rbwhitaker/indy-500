using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indy500
{

    [Serializable]
    public class LevelParseException : Exception
    {
        public LevelParseException()
        {
        }

        public LevelParseException(string message) : base(message) { }

        public LevelParseException(string message, Exception inner) : base(message, inner) { }

        protected LevelParseException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
