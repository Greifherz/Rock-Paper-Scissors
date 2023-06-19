using System.Collections;
using Extensions;
using GameItems;
using Persistence;

namespace Loaders
{
	public class PlayerInfoLoader
	{
		public delegate void OnLoadedAction(PlayerData playerData);
		public event OnLoadedAction OnLoaded;

		private IPersistenceService _persistenceService;
		
		public PlayerInfoLoader(IPersistenceService service)
		{
			_persistenceService = service;
		}
		
		public void Load()
		{
			if (_persistenceService.HasPlayerData())
			{
				OnLoaded(_persistenceService.LoadPlayerData());
			}
			else
			{
				var mockPlayer = new PlayerData
				{
					UserId = 1,
					Name = StringUtilityExtensions.GenerateRandomName(),
					Coins = 50
				};

				_persistenceService.SavePlayerData(mockPlayer);

				OnLoaded(mockPlayer);
			}
			
		}
	}
}