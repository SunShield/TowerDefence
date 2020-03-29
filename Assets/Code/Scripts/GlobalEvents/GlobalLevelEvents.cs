using System;
using TowerDefence.Unity.Monster;

namespace TowerDefence.Unity.GlobalEvents
{
	/// <summary>
	/// I think, using Global...Events classes as I am going to use them is not the best soultion possible.
	/// More proper way would be adding an Init method into them and Itit them with all possible classes containing events we need to globalize.
	/// But for now just using Global...Events classes to contain all need-to-globalize envets directly is ok, 
	/// disregarding this way's huge coupling increasement
	/// </summary>
	public class GlobalLevelEvents
	{
		private static GlobalLevelEvents _instance;
		private GlobalLevelEvents() { }
		public static GlobalLevelEvents Instance => _instance ?? (_instance = new GlobalLevelEvents());
		
		#region Global Level Events
		
		public void InvokeOnNewWave(int waveNumber)
		{
			OnNewWave?.Invoke(waveNumber);
		}

		public delegate void NewWaveArgs(int waveNumber);
		public event NewWaveArgs OnNewWave;

		#endregion

		#region Player Events

		public void InvokeOnPlayerLifeChange(int value, int newLifeAmount)
		{
			OnPlayerLifeChange?.Invoke(value, newLifeAmount);
		}

		public void InvokeOnPlayerGoldChange(int value, int newGoldAmount)
		{
			OnPlayerGoldChange?.Invoke(value, newGoldAmount);
		}

		public delegate void PlayerLifeAmountChanged(int value, int newLifeAmount);
		public event PlayerLifeAmountChanged OnPlayerLifeChange;

		public delegate void PlayerGoldAmountChanged(int value, int newGoldAmount);
		public event PlayerGoldAmountChanged OnPlayerGoldChange;

		#endregion

		#region MonsterEvents

		public void InvokeOnMonsterEntersCastle(MonsterController monster)
		{
			OnMonsterEntersCastle?.Invoke(monster);
		}

		public void InvokeOnMonsterDies(MonsterController monster)
		{
			OnMonsterDies?.Invoke(monster);
		}

		public event Action<MonsterController> OnMonsterEntersCastle;
		public event Action<MonsterController> OnMonsterDies;

		#endregion
	}
}
