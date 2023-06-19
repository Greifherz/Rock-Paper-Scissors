using GameItems;

namespace EventPipelineSystem.Interfaces
{
    public interface IPlayerInfoEvent : IGameEvent
    {
        PlayerData PlayerInfo { get; }
    }
}
