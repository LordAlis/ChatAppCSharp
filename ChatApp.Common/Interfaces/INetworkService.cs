namespace ChatApp.Common.Interfaces
{
    public interface INetworkService
    {
        bool IsRunning { get; }
        Task StartAsync();
        void Stop();
    }
}
