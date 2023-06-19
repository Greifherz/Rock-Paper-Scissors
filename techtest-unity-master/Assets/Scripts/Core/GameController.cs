using System;
using System.Collections;
using EventPipelineSystem;
using EventPipelineSystem.Events;
using EventPipelineSystem.Interfaces;
using Extensions;
using GameItems;
using Loaders;
using Persistence;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
	public class GameController : MonoEventHandlerAdapter
	{
		[SerializeField] private Text playerHand;
		[SerializeField] private Text enemyHand;
		[SerializeField] private Text nameLabel;
		[SerializeField] private Text moneyLabel;
		[SerializeField] private Text matchResultLabel;

		[SerializeField] private GameObject gameOutcomePanel;
		[SerializeField] private Text gameOutcomeLabel;
	
		private Player _player;
		private bool _inputAllowed = true;
		private IPersistenceService _persistenceService;

		void Start()
		{
			EventPipelineSystem.EventPipelineSystem.Instance.RegisterListener(Visit);
			_persistenceService = new PlayerPrefsPersistenceService();
			
			var InfoLoader = new PlayerInfoLoader(_persistenceService);
			InfoLoader.OnLoaded += (playerData) =>
			{
				EventPipelineSystem.EventPipelineSystem.Instance.Raise(new PlayerInfoEvent(playerData));
			};
			InfoLoader.Load();
			
			// EventPipelineSystem.EventPipelineSystem.Instance.Raise(new PlayerInfoEvent(1,StringUtilityExtensions.GenerateRandomName(),50)); //Here for testing purposes
			
			UpdateGameLoader updateGameLoader = UpdateGameLoader.GetUpdateGameLoaderInstance();
			updateGameLoader.OnResult += OnGameUpdated;
		}

		void Update()
		{
			UpdateHud();
		}

		public void OnPlayerInfoLoaded(PlayerData playerData)
		{
			_player = new Player(playerData);
		}

		public void UpdateHud()
		{
			nameLabel.text = "Name: " + _player.Name;
			moneyLabel.text = "Money: $" + _player.Coins.ToString();
		}

		public void HandlePlayerInput(int item)
		{
			if(!_inputAllowed) return; //Reactive programming could remove this flag, I really don't like flagging.
		
			UseableItem playerChoice = UseableItem.None;

			switch (item)
			{
				case 1:
					playerChoice = UseableItem.Rock;
					break;
				case 2:
					playerChoice = UseableItem.Paper;
					break;
				case 3:
					playerChoice = UseableItem.Scissors;
					break;
			}

			UpdateGame(playerChoice);
		
			// Cooldown
			_inputAllowed = false;
			StartCoroutine(CoroutineWait(0.5f, () =>
			{
				_inputAllowed = true;
			}));
		}

		private void UpdateGame(UseableItem playerChoice)
		{
			UpdateGameLoader.GetUpdateGameLoaderInstance().GenerateResult(playerChoice);
		}

		public void OnGameUpdated(Hashtable gameUpdateData)
		{
			playerHand.text = DisplayResultAsText((UseableItem)gameUpdateData["resultPlayer"]);
			enemyHand.text = DisplayResultAsText((UseableItem)gameUpdateData["resultOpponent"]);

			var coinsAmountChange = (int) gameUpdateData["coinsAmountChange"];
			_player.ChangeCoinAmount(coinsAmountChange);

			matchResultLabel.text = ((Result) gameUpdateData["matchResult"]).ToString() + "! " + (coinsAmountChange > 0 ? "+$" : "-$") + Math.Abs(coinsAmountChange);
		
			//Win/Lose condition
			int playerCoins = _player.Coins;
			if (playerCoins < 1 || playerCoins >= 150)
			{
				//Deprecated the commented part below because it's not like we're sending something to the server now and there are other systems that would need to be triggered by this event
				//Since only "View" events are concerned with this and the GameController is the view controller responsible for such things, it's unwieldy to use the EventPipeline only for this
			
				// EventPipelineSystem.Instance.Raise(new GameOverEvent(playerCoins >= 150));

				GameOver(playerCoins >= 150);
				_persistenceService.ClearPlayerData();
			}
			else
			{
				_persistenceService.SavePlayerData(_player.GetPlayerData());
			}
		}

		private string DisplayResultAsText (UseableItem result)
		{
			switch (result)
			{
				case UseableItem.Rock:
					return "Rock";
				case UseableItem.Paper:
					return "Paper";
				case UseableItem.Scissors:
					return "Scissors";
			}

			return "Nothing";
		}

		private IEnumerator CoroutineWait(float waitDuration,Action callback)
		{
			yield return new WaitForSeconds(waitDuration);
			callback();
		}

		private void GameOver(bool overStatus)
		{
			var gameOverText = "Game over, you " + (overStatus ? "Won!" : "Lose!") ;
			Debug.Log(gameOverText);
		
			gameOutcomePanel.SetActive(true);
			gameOutcomeLabel.text = gameOverText;
		}

		public override void Visit(IGameEvent gameEvent)
		{
			gameEvent.Visited(this);
		}

		public override void Handle(IPlayerInfoEvent playerInfoEvent)
		{
			OnPlayerInfoLoaded(playerInfoEvent.PlayerInfo);
		}
	}
}