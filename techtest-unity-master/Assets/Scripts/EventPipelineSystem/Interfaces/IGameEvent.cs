namespace EventPipelineSystem.Interfaces
{
    public interface IGameEvent 
    {
        void Visited(IEventHandler handler);
    }
}
