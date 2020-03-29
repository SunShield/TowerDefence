using UnityEngine;

namespace TowerDefence.Unity.UI
{
	public class BasicHighlighter : MonoBehaviour
	{
		[SerializeField] private GameObject HighlightedGraphics;

		public void ChangeHighlightState(bool newState)
		{
			HighlightedGraphics.SetActive(newState);
		}
	}
}
