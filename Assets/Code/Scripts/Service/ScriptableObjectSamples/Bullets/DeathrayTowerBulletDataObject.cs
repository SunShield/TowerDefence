using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet/Deathray", order = 1)]
	public class DeathrayTowerBulletDataObject : GenericDataObject<DeathrayTowerBulletData>
	{
	}
}
