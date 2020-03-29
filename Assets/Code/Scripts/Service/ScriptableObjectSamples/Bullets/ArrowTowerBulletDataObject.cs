using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet/Arrow", order = 1)]
	public class ArrowTowerBulletDataObject : GenericDataObject<ArrowTowerBulletData>
	{
	}
}
