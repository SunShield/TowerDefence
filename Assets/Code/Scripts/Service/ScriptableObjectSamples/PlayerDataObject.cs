using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player", order = 1)]
	public class PlayerDataObject : GenericDataObject<PlayerData>
	{
	}
}
