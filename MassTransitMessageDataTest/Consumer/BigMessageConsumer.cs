using System.Threading.Tasks;
using MassTransit;
using MassTransitMessageDataTest.Commands;

namespace MassTransitMessageDataTest.Consumer
{
    public class BigMessageConsumer: IConsumer<BigMessage>
    {
        public async Task Consume(ConsumeContext<BigMessage> context)
        {
            var test = await context.Message.BigPayload.Value;
            throw new System.NotImplementedException();
        }
    }
}