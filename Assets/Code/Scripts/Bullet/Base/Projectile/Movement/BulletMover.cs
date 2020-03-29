using UnityEngine;

namespace TowerDefence.Unity.Bullet
{

	public abstract class BulletMover : MonoBehaviour
	{
		public float Speed { get; set; }
		public bool IsActive { get; set; } = true;
		public BulletTargetData TargetData { get; set; }
		public abstract void Move();

		public void SetTarget(BulletTargetData data)
		{
			TargetData = data;
		}
	}
}
