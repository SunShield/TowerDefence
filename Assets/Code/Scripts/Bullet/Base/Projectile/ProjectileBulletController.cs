using TowerDefence.Core.DataStructure;
using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	public class ProjectileBulletController<T> : GenericInitableBulletController<T> where T: ProjectileBulletData
	{
		[SerializeField] private CircleCollider2D _collider;
		[SerializeField] private BulletMover Mover;
		private float _radius;

		public override void Init(Vector3 emitterPos, T data)
		{
			Mover.Speed = data.BulletSpeed;
			_radius = data.Radius;
			SetRadius();
			base.Init(emitterPos, data);
		}

		public override void Proceed()
		{
			Mover.Move();
		}

		public override void SetTarget(BulletTargetData targetData)
		{
			base.SetTarget(targetData);
			Mover.SetTarget(targetData);
		}

		protected void SetRadius()
		{
			_collider.radius = _radius / 200;
		}

		protected override void DoOnEnemyHit(Collider2D other)
		{
			Mover.IsActive = false;
			base.DoOnEnemyHit(other);
		}
	}
}
