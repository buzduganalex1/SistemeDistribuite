using System;
using System.Collections.Generic;
using System.Text;

namespace Publisher.Domain
{
    public class MessageWrapper<T>
    {
        public string MessageType { get { return typeof(T).FullName; } }

        public T Message { get; set; }
    }
}
