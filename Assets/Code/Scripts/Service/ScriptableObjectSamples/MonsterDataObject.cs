using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "MonsterData", menuName = "Data/Monster", order = 1)]
	public class MonsterDataObject : GenericDataObject<MonsterData>
	{
		public string MonsterName;
	}
}