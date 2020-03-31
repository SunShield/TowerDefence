using System.Collections.Generic;
using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	/// <summary>
	/// This tower does huge area damage with it's beam. Then beam lingers, dealing half of damage to anybody touching it.
	/// </summary>
	public class DeathrayTowerBulletController : BeamBulletController<DeathrayTowerBulletData>
	{
		protected override void DealDamage()
		{
			Vector2 lineToTarget = (TargetData.VectorData - EmitterPosition);
			Vector2 startingPoint = (Vector2)EmitterPosition + Vector2.Perpendicular(lineToTarget).normalized * Renderer.sprite.rect.height / 200;
			Vector2 endingPoint = (Vector2)TargetData.VectorData - Vector2.Perpendicular(lineToTarget).normalized * Renderer.sprite.rect.height / 200;

			var enemies = Physics2D.OverlapAreaAll(startingPoint, endingPoint, 1 << LayerMask.NameToLayer("Enemies"));
			var controllers = new List<MonsterController>();
			foreach (var enemy in enemies)
			{
				controllers.Add(enemy.GetComponent<MonsterController>());
			}

			foreach (var controller in controllers)
			{
				MonsterHitDamageData data = GetHitDamageData();
				controller.TryReceiveHit(data);
			}
		}

		protected override void DoOnEnemyHit(Collider2D other)
		{
			MonsterController controller = other.GetComponent<MonsterController>();
			MonsterHitDamageData data = GetHitDamageData();
			controller.TryReceiveHit(data);
		}
	}
}
