using System.IO;
using MassTransit;

namespace MassTransitMessageDataTest.Commands
{
    public class BigMessage
    {
        public BigMessage(string id, MessageData<Stream> bigPayload)
        {
            Id = id;
            BigPayload = bigPayload;
        }

        public string Id { get; set; }

        public MessageData<Stream> BigPayload { get; set; }
    }
}