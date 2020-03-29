namespace TowerDefence.Unity.UI
{
	public class GlobalLevelUiEventsRelay
	{
		private static GlobalLevelUiEventsRelay _instance;
		private GlobalLevelUiEventsRelay() { }
		public static GlobalLevelUiEventsRelay Instance => _instance ?? (_instance = new GlobalLevelUiEventsRelay());


	}
}
