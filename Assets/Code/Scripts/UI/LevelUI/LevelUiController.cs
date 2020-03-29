using TowerDefence.Unity.Service;
using UnityEngine;

namespace TowerDefence.Unity.UI
{
	public class LevelUiController : MonoSingleton<LevelUiController>
	{
		[SerializeField] private MainDataUIController MainDataUiController;
		[SerializeField] private TowerBuyMenuController TowerBuyMenuController;

		public void Init()
		{
			InitTowerBuyUiManager();
			InitMainLevelUiController();
		}

		private void InitTowerBuyUiManager()
		{
			TowerBuyMenuController.Init();
		}

		private void InitMainLevelUiController()
		{
			MainDataUiController.Init();
		}
	}
}
