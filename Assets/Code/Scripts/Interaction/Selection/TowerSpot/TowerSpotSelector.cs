using TowerDefence.Unity.TowerSpot;

namespace TowerDefence.Unity.Interaction.Selection
{
	public class TowerSpotSelector : BasicSelector<TowerSpotController>
	{
		protected override void InvokeOnSelectedEvent()
		{
			if(SelectionDataItem.IsEmpty)
				base.InvokeOnSelectedEvent();
		}
	}
}
