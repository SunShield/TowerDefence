using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	public class HomingBulletMover : BulletMover
	{
		protected Vector3 DirectionVector => (TargetData.TransformData.position - transform.position).normalized;
		private bool HasTarget => TargetData.TransformData != null;

		public override void Move()
		{
			if(IsActive && HasTarget)
				transform.position += DirectionVector * Speed * Time.deltaTime;
		}
	}
}
