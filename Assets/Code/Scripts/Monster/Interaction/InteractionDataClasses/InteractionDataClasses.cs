using System.Collections.Generic;
using TowerDefence.Unity.Bullet;
using TowerDefence.Unity.Effect.Monster;

namespace TowerDefence.Unity.Monster
{
	public class MonsterHitDamageData
	{
		public BaseBulletController Bullet;
		public List<MonsterEffect> OnHitInflictableEffects = new List<MonsterEffect>();
		public List<MonsterEffect> OnDamageInflictableEffects = new List<MonsterEffect>();
		public int DamageAmount;

		public MonsterHitDamageData()
		{

		}

		public MonsterHitDamageData(MonsterHitDamageData hitDamageData)
		{
			Bullet = hitDamageData.Bullet;
			DamageAmount = hitDamageData.DamageAmount;
			OnHitInflictableEffects = new List<MonsterEffect>(hitDamageData.OnHitInflictableEffects);
			OnDamageInflictableEffects = new List<MonsterEffect>(hitDamageData.OnDamageInflictableEffects);
		}
	}
}
