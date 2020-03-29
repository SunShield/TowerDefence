using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	public class BeamBulletController<T> : InstantBulletController<T> where T: InstantBulletData
	{
		public override void OnAppear()
		{
			SetBeamScale();
			SetBeamRotation();
			DealDamage();

			base.OnAppear();
		}

		protected virtual void SetBeamScale()
		{
			transform.position = EmitterPosition + (TargetData.TransformData.position - EmitterPosition) / 2;
			transform.localScale = new Vector3(CalculateScale(), 1f, 1f);
		}

		protected virtual void SetBeamRotation()
		{
			Vector3 directionVector = (EmitterPosition - TargetData.TransformData.position).normalized;
			float rotation = -Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotation + 90f);
		}

		protected float CalculateScale()
		{
			float scale = (EmitterPosition - TargetData.TransformData.position).magnitude * 100f / Renderer.sprite.rect.height;
			return scale;
		}

		protected virtual void DealDamage()
		{
			MonsterController controller = TargetData.TransformData.GetComponent<MonsterController>();
			MonsterHitDamageData data = GetHitDamageData();
			controller.TryReceiveHit(data);
		}

		// beams don't disappear on contact with an enemy
		public override void HandleCollision(Collider2D other)
		{
			if (CheckIfEnemy(other.gameObject))
			{
				DoOnEnemyHit(other);
			}
		}
	}
}
