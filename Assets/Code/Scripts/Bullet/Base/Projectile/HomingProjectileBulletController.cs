using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	public class HomingProjectileBulletController<T> : ProjectileBulletController<T> where T: ProjectileBulletData
	{

		public override void Proceed()
		{
			if(TargetData.TransformData != null)
				base.Proceed();
			else
				DestroySelf(); // if target is lost, bullet just disappears
		}
	}
}
