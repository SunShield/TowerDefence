using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "TowerData", menuName = "Data/Tower", order = 1)]
	public class TowerDataObject : GenericDataObject<TowerData>
	{
		public string Name;
	}
}
