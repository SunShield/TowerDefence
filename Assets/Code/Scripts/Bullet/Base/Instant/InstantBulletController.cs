using System.Numerics;
using TowerDefence.Core.DataStructure;

namespace TowerDefence.Unity.Bullet
{
	public class InstantBulletController<T> : GenericInitableBulletController<T> where T: InstantBulletData
	{
		public Vector3 Target { get; set; }
	}
}
