using EventPipelineSystem.Interfaces;

namespace EventPipelineSystem.Events
{
    public class GameOverEvent : IGameOverEvent
    {
        public bool Win { get; }

        public GameOverEvent(bool win)
        {
            Win = win;
        }

        public void Visited(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
}
