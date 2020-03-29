using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Service;
using UnityEngine;

namespace TowerDefence.Unity.Interaction.Selection
{
	public class SelectionController : MonoSingleton<SelectionController>
	{
		[field: SerializeField] public TowerSpotGroupSelectionController TowerSpotsController { get; private set; }
		[SerializeField] private TowerGroupSelectionController TowersController;
		[SerializeField] private MonsterGroupSelectionController MonstersController;
		[SerializeField] private SelectionRelayController RelayController;

		public void Init()
		{
			InitEntityGroupSelectionControllers();
			InitRelaySelectionController();
		}

		private void InitEntityGroupSelectionControllers()
		{
			TowerSpotsController.Init();
			TowersController.Init();
			MonstersController.Init();
		}

		private void InitRelaySelectionController()
		{
			IEntityGroupSelectionController[] controllers = { TowerSpotsController, TowersController, MonstersController };
			RelayController.Init(controllers);
		}
	}
}
