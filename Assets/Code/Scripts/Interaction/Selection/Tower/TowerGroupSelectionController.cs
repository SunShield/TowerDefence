using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Tower;

namespace TowerDefence.Unity.Interaction.Selection
{
	public class TowerGroupSelectionController : EntityGroupEntityGroupSelectionController
	{
		protected override void DoOnSelectionReceivedActions()
		{
			GlobalSelectionEvents.Instance.InvokeOnTowerSelectionChanged(((BasicSelector<TowerController>)PreviousSelectedItem)?.SelectionDataItem);
		}
	}
}
