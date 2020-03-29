using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Monster;

namespace TowerDefence.Unity.Interaction.Selection
{
	// todo: consider adding MonsterSelectors in OnMonsterSpawn event and deleting them on OnMonsterDeath event
	public class MonsterGroupSelectionController : EntityGroupEntityGroupSelectionController
	{
		protected override void DoOnSelectionReceivedActions()
		{
			GlobalSelectionEvents.Instance.InvokeOnMonsterSelectionChanged(((BasicSelector<MonsterController>)PreviousSelectedItem).SelectionDataItem);
		}
	}
}
