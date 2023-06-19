namespace EventPipelineSystem.Interfaces
{
    public interface IEventHandler
    {
        void Visit(IGameEvent gameEvent);

        void Handle(IGameEvent gameEvent);

        void Handle(IPlayerInfoEvent playerInfoEvent);

        void Handle(IGameOverEvent gameOverEvent);
    }
}
