using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Service.ScriptableObjectSpawners
{
	[CreateAssetMenu(fileName = "BulletData", menuName = "Data/Bullet/Freeze", order = 1)]
	public class FreezeTowerBulletDataObject : GenericDataObject<FreezeTowerBulletData>
	{
	}
}
