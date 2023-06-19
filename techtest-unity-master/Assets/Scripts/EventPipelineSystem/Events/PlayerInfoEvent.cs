using EventPipelineSystem.Interfaces;
using GameItems;

namespace EventPipelineSystem.Events
{
    public class PlayerInfoEvent : IPlayerInfoEvent
    {
        public PlayerData PlayerInfo { get; } 

        public PlayerInfoEvent(PlayerData playerInfo)
        {
            PlayerInfo = playerInfo;
        }

        public PlayerInfoEvent(int userId, string name, int coins)
        {
            PlayerInfo = new PlayerData
            {
                UserId = userId,
                Name = name,
                Coins = coins
            };
        }
    
        public void Visited(IEventHandler handler)
        {
            handler.Handle(this);
        }
    }
}
