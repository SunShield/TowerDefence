using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level", order = 1)]
	public class LevelDataObject : GenericDataObject<LevelData>
	{
		public string LevelName;
	}
}
