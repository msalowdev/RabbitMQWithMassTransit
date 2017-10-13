

namespace Common
{
    public interface IRequestFactory
    {
        IMessageRequest CreateMessageRequest<TRequestMessage, TResponseMessage>();
    }
}
