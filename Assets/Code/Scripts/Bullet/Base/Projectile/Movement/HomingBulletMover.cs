using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	public class HomingBulletMover : BulletMover
	{
		protected Vector3 DirectionVector => (TargetData.TransformData.position - transform.position).normalized;
		
		public override void Move()
		{
			if(IsActive)
				transform.position += DirectionVector * Speed * Time.deltaTime;
		}
	}
}
