using System;
using Sirenix.OdinInspector;
using TowerDefence.Unity.UI;
using UnityEngine;

namespace TowerDefence.Unity.Interaction.Selection
{
	public interface IBasicSelector
	{
		void ChangeSelectionState(bool newState);
		event Action<IBasicSelector> OnSelected;
	}

	public class BasicSelector<T> : SerializedMonoBehaviour, IBasicSelector where T: MonoBehaviour
	{
		[SerializeField] private BasicHighlighter Highlighter;
		[field: SerializeField] public T SelectionDataItem { get; private set; } // this is passed to selection event to be handled on upper levels.
		
		private void OnMouseDown()
		{
			InvokeOnSelectedEvent();
		}

		protected virtual void InvokeOnSelectedEvent()
		{
			OnSelected?.Invoke(this);
		}

		public void ChangeSelectionState(bool newState)
		{
			Highlighter.ChangeHighlightState(newState);
		}

		public event Action<IBasicSelector> OnSelected;
	}
}
