using UnityEngine;
using TowerDefence.Core.DataStructure;

namespace TowerDefence.Unity.Bullet
{
	public class GenericInitableBulletController<T> : BaseBulletController where T : BaseBulletData
	{
		public virtual void Init(Vector3 emitterPos, T data)
		{
			base.Init(data.Damage, data.LifeTime, emitterPos);
		}
	}
}
