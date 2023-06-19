using System;

namespace GameItems
{
	[Serializable]
	public class Player
	{
		public int UserId { get; private set; }
		public string Name { get; private set; }
		public int Coins { get; private set; }

		public Player(PlayerData playerData)
		{
			UserId = playerData.UserId;
			Name = playerData.Name; 
			Coins = playerData.Coins;
		}

		public Player(int userId, string name, int coins)
		{
			UserId = userId;
			Name = name;
			Coins = coins;
		}

		public PlayerData GetPlayerData()
		{
			return new PlayerData
			{
				UserId = this.UserId,
				Name = this.Name,
				Coins = this.Coins
			};
		}

		public void ChangeCoinAmount(int amount)
		{
			Coins += amount;
		}
	}
}
