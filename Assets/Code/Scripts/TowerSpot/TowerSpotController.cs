using TowerDefence.Unity.Tower;
using UnityEngine;

namespace TowerDefence.Unity.TowerSpot
{
	public class TowerSpotController : MonoBehaviour
	{
		public TowerController Tower { get; private set; }
		public bool IsEmpty => Tower == null;

		public void SetTower(TowerController controller)
		{
			Tower = controller;
		}
	}
}
