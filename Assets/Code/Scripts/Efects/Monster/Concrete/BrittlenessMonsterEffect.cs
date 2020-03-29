using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Effect.Monster
{
	public class BrittlenessMonsterEffect : MonsterEffect
	{
		public override string Name { get; protected set; } = "Brittleness";
		public override int Priority { get; protected set; } = 1;
		public override bool IsStackable { get; protected set; } = false;
		public override EffectTags[] EffectTagses { get; protected set; } = { EffectTags.OnTryHit, EffectTags.Durational };

		private int DamageIncreasement => (int)Values[0];
		private float Duration => Values[1];
		private float _timer;

		public BrittlenessMonsterEffect(float[] values) : base(values) { } // we are able to add values amount checks here

		public override MonsterHitDamageData OnHitConfirmed(MonsterHitDamageData hitDamageData, MonsterController controller)
		{
			hitDamageData.DamageAmount *= (1 + DamageIncreasement);
			return base.OnHitConfirmed(hitDamageData, controller);
		}

		// todo: create DurationalMonsterEffect subclass
		public override void OnProceed(MonsterController controller)
		{
			_timer += Time.deltaTime;
			if (_timer >= Duration)
			{
				controller.RemoveEffect(this); // kak tebe takoe, ILON MASK?!
			}
			base.OnProceed(controller);
		}
	}
}

