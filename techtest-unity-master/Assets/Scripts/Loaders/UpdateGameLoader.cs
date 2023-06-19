using System;
using System.Collections;
using GameItems;

namespace Loaders
{
	public class UpdateGameLoader
	{
		private static UpdateGameLoader instance;
	
		public delegate void OnResultAction(Hashtable gameUpdateData);
		public event OnResultAction OnResult;

		private UpdateGameLoader() //Private constructor so its a singleton
		{
		
		}
	
		public static UpdateGameLoader GetUpdateGameLoaderInstance()
		{
			if (instance == null) instance = new UpdateGameLoader();
			return instance;
		}

		public void GenerateResult(UseableItem choice)
		{
			UseableItem opponentHand = (UseableItem)Enum.GetValues(typeof(UseableItem)).GetValue(UnityEngine.Random.Range(1, 4));

			var result = ResultAnalyzer.GetResultState(choice, opponentHand);
		
			Hashtable mockGameUpdate = new Hashtable();
			mockGameUpdate["resultPlayer"] = choice;
			mockGameUpdate["resultOpponent"] = opponentHand;
			mockGameUpdate["coinsAmountChange"] = GetCoinsAmount(result);
			mockGameUpdate["matchResult"] = result;
		
			OnResult(mockGameUpdate);
		}

		private int GetCoinsAmount (Result drawResult)
		{
			if (drawResult.Equals (Result.Won))
			{
				return 10;
			}
			else if (drawResult.Equals (Result.Lost))
			{
				return -10;
			}
			else
			{
				return 0;
			}

			return 0;
		}
	}
}