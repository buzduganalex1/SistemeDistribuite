using System;
using System.Collections.Generic;
using System.Text;

namespace CoinProcessor.Configuration
{
    public class Wrapper<T>
    {
        public Wrapper(T objectMessage)
        {
            this.Message = objectMessage;
        }

        public T Message { get; set; }

        public DateTime sendingDateTime;
    }
}
