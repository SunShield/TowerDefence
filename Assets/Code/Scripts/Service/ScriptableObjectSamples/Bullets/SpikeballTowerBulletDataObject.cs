using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet/Spikeball", order = 1)]
	public class SpikeballTowerBulletDataObject : GenericDataObject<SpikeballTowerBulletData>
	{
	}
}
