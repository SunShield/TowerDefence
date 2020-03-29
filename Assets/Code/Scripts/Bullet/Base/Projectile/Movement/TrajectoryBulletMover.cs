using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	public class TrajectoryBulletMover : BulletMover
	{
		private const float DistanceToleration = 0.03f;
		
		protected Vector3 DirectionVector => (TargetData.VectorData - transform.position).normalized;
		private bool ReachedTarget => (transform.position - TargetData.VectorData).magnitude <= DistanceToleration;
		
		public override void Move()
		{
			if (!IsActive) return;

			DoMove();
			if (ReachedTarget)
				IsActive = false;
		}

		private void DoMove()
		{
			transform.position += DirectionVector * Speed * Time.deltaTime;
		}
	}
}
