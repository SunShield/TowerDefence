using TowerDefence.Unity.Service;
using UnityEngine;

namespace TowerDefence.Unity.Interaction.Selection
{
	public class SelectionRelayController : MonoSingleton<SelectionRelayController>
	{
		private IEntityGroupSelectionController[] _entityGroupSelectionControllers;
		private IEntityGroupSelectionController _prevController;

		public void Init(IEntityGroupSelectionController[] entityGroupSelectionControllers)
		{
			_entityGroupSelectionControllers = entityGroupSelectionControllers;
			SubscribeForSelectionEvents();
		}

		public void UnselectAll() // todo: implement right mouse click anywhere calling this event
		{
		}

		private void SubscribeForSelectionEvents()
		{
			foreach (var controller in _entityGroupSelectionControllers)
			{
				controller.OnSelected += OnSelectionEvent;
			}
		}

		private void OnSelectionEvent(IEntityGroupSelectionController controller)
		{
			if (_prevController == controller) return;

			_prevController?.Unselect();
			_prevController = controller;
		}
	}
}
