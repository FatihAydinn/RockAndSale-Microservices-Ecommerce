using RAS.MessageBus;

namespace RAS.Services.BagAPI.RabbitMQSender
{
    public interface IRabbitMQBagMessageSender
    {
        void SendMessage(BaseMessage baseMessage, String queueName);
    }
}
