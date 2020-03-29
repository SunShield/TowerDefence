using TowerDefence.Unity.Interaction.Selection;
using TowerDefence.Unity.Monster;
using TowerDefence.Unity.Tower;
using TowerDefence.Unity.TowerSpot;

namespace TowerDefence.Unity.GlobalEvents
{
	public class GlobalSelectionEvents
	{
		private static GlobalSelectionEvents _instance;
		private GlobalSelectionEvents() { }
		public static GlobalSelectionEvents Instance => _instance ?? (_instance = new GlobalSelectionEvents());

		public void InvokeOnTowerSpotSelectionChanged(TowerSpotController newSelection)
		{
			OnTowerSpotSelectionChanged?.Invoke(newSelection);
		}

		public void InvokeOnTowerSelectionChanged(TowerController newSelection)
		{
			OnTowerSelectionChanged?.Invoke(newSelection);
		}

		public void InvokeOnMonsterSelectionChanged(MonsterController newSelection)
		{
			OnMonsterSelectionChanged?.Invoke(newSelection);
		}

		public delegate void TowerSpotSelectionArgs(TowerSpotController newSelection);
		public event TowerSpotSelectionArgs OnTowerSpotSelectionChanged;

		public delegate void TowerSelectionArgs(TowerController newSelection);
		public event TowerSelectionArgs OnTowerSelectionChanged;

		public delegate void MonsterSelectionArgs(MonsterController newSelection);
		public event MonsterSelectionArgs OnMonsterSelectionChanged;
	}
}
