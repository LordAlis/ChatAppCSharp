using ChatApp.Common.Models;

namespace ChatApp.Common.Interfaces
{
    public interface IMessageHandler
    {
        void HandleMessage(ChatMessage message);
    }
}
