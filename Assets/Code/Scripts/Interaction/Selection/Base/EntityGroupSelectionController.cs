using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerDefence.Unity.Interaction.Selection
{
	public interface IEntityGroupSelectionController
	{
		void Init();
		void Unselect();
		event Action<IEntityGroupSelectionController> OnSelected;
	}

	public class EntityGroupEntityGroupSelectionController : SerializedMonoBehaviour, IEntityGroupSelectionController
	{
		[SerializeField] private IBasicSelector[] _items;
		protected IBasicSelector PreviousSelectedItem;

		public void Init()
		{
			SubscribeForOnSelectedEvents();
		}

		public void Unselect()
		{
			PreviousSelectedItem.ChangeSelectionState(false);
			PreviousSelectedItem = null;
		}

		private void SubscribeForOnSelectedEvents()
		{
			if (_items == null) return;

			foreach (var item in _items)
			{
				item.OnSelected += OnSelectionEvent;
			}
		}

		private void OnSelectionEvent(IBasicSelector item)
		{
			ChangeSelection(item);
			DoOnSelectionReceivedActions();
			InvokeOnSelectedEvent();
		}

		protected virtual void DoOnSelectionReceivedActions()
		{

		}

		private void ChangeSelection(IBasicSelector item)
		{
			if (PreviousSelectedItem == item)
			{
				PreviousSelectedItem = null;
				item.ChangeSelectionState(false);
			}
			else
			{
				PreviousSelectedItem?.ChangeSelectionState(false);
				PreviousSelectedItem = item;
				item.ChangeSelectionState(true);
			}
		}

		private void InvokeOnSelectedEvent()
		{
			OnSelected?.Invoke(this);
		}

		public event Action<IEntityGroupSelectionController> OnSelected;
	}
}
