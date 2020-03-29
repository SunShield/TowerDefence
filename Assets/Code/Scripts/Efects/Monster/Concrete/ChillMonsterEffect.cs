using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Effect.Monster
{
	public class ChillMonsterEffect : MonsterEffect
	{
		public override string Name { get; protected set; } = "Chill";
		public override int Priority { get; protected set; } = 1;
		public override bool IsStackable { get; protected set; } = false;
		public override EffectTags[] EffectTagses { get; protected set; } = { EffectTags.OnApply, EffectTags.OnExpire, EffectTags.Durational };

		private float SlowAmount => (1f - Values[0] / 100f);
		private float Duration => Values[1];
		private float _timer;

		public ChillMonsterEffect(float[] values) : base(values) { }

		private float _moverSpeedStored;
		private MonsterMover _moverStored;

		public override void OnApply(MonsterController controller)
		{
			_moverStored = controller.GetMover();
			_moverSpeedStored = _moverStored.Speed;
			_moverStored.Speed *= SlowAmount;
		}

		public override void OnExpire(MonsterController controller)
		{
			_moverStored.Speed = _moverSpeedStored;
			base.OnExpire(controller);
		}

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
