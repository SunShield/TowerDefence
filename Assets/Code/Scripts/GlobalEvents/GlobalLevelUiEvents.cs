namespace TowerDefence.Unity.GlobalEvents
{
	public class GlobalLevelUiEvents
	{
		private static GlobalLevelUiEvents _instance;
		private GlobalLevelUiEvents() { }
		public static GlobalLevelUiEvents Instance => _instance ?? (_instance = new GlobalLevelUiEvents());

		public void InvokeOnTowerVariantChosenFromBuymenu(string towerVariant)
		{
			OnTowerVariantChosenFromBuymenu?.Invoke(towerVariant);
		}

		public delegate void TowerVariantChosen(string towerVariant);
		public event TowerVariantChosen OnTowerVariantChosenFromBuymenu;
	}
}
