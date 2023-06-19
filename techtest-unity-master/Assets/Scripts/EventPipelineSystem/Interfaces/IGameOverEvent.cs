namespace EventPipelineSystem.Interfaces
{
    public interface IGameOverEvent : IGameEvent
    {
        bool Win { get; }
    }
}
