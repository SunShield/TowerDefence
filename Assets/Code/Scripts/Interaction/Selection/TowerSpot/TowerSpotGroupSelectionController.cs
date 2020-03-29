using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.TowerSpot;

namespace TowerDefence.Unity.Interaction.Selection
{
	public class TowerSpotGroupSelectionController : EntityGroupEntityGroupSelectionController
	{
		protected override void DoOnSelectionReceivedActions()
		{
			GlobalSelectionEvents.Instance.InvokeOnTowerSpotSelectionChanged(((BasicSelector<TowerSpotController>)PreviousSelectedItem)?.SelectionDataItem);
		}
	}
}
