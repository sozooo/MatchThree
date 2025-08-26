using UniRx;

namespace Project.Scripts.MessageBrokers
{
    public static class MessageBrokerHolder
    {
        public static readonly IMessageBroker Ball = new MessageBroker();
        public static readonly IMessageBroker Line = new MessageBroker();
    }
}