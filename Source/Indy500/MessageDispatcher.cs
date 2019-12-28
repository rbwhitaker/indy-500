using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indy500
{
    public delegate void MessageHandler(object sender, MessageArgs e);
    public class MessageDispatcher : IDisposable
    {
        private Dictionary<MessageType, List<MessageHandler>> _handlers;

        public MessageDispatcher()
        {
            _handlers = new Dictionary<MessageType, List<MessageHandler>>();
        }

        public void RegisterMessage(MessageType messageType, MessageHandler address)
        {
            if (!_handlers.ContainsKey(messageType))
            {
                _handlers.Add(messageType, new List<MessageHandler>());
            }
            _handlers[messageType].Add(address);
        }

        public void InvokeMessage(MessageType messageType, object sender, MessageArgs e)
        {
            if (_handlers.Count > 0)
                foreach (var handler in _handlers[messageType])
                {
                    handler(sender, e);
                }
        }

        public void Dispose()
        {
            _handlers = null;
        }
    }

    public enum MessageType
    {
        Collision
    }
}
