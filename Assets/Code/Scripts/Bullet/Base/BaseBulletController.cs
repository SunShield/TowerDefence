using TowerDefence.Unity.Monster;
using UnityEngine;

namespace TowerDefence.Unity.Bullet
{
	public struct BulletTargetData
	{
		public Transform TransformData;
		public Vector3 VectorData;
	}

	public class BaseBulletController : MonoBehaviour
	{
		[SerializeField] protected SpriteRenderer Renderer;

		private bool _isInited;
		protected float LifeTimeTimer;
		protected Vector3 EmitterPosition;
		protected BulletTargetData TargetData;

		public bool IsActive { get; set; }
		public int Damage { get; private set; }
		public float LifeTime { get; private set; }

		public void Init(int damage, float lifeTime, Vector3 emitterPos)
		{
			Damage = damage;
			LifeTime = lifeTime;
			EmitterPosition = emitterPos;

			LifeTimeTimer = LifeTime;
			OnAppear();
			_isInited = true;
		}

		public void SetSprite(Sprite sprite)
		{
			Renderer.sprite = sprite;
		}

		public virtual void SetTarget(BulletTargetData targetData)
		{
			TargetData = targetData;
		}

		private void Update()
		{
			if (!_isInited)
				return;

			Proceed();
			CheckLifeTime(Time.deltaTime);
		}

		protected virtual void OnTriggerEnter2D(Collider2D other)
		{
			HandleCollision(other);
		}

		public virtual void OnAppear()
		{

		}

		public virtual void Proceed()
		{

		}

		private void CheckLifeTime(float time)
		{
			LifeTimeTimer -= time;
			if (LifeTimeTimer <= 0f)
				DestroySelf();
		}

		public virtual void HandleCollision(Collider2D other)
		{
			if (CheckIfEnemy(other.gameObject))
			{
				DoOnEnemyHit(other);
				DestroySelf();
			}
		}

		protected virtual void DoOnEnemyHit(Collider2D other)
		{
			MonsterController controller = other.GetComponent<MonsterController>();
			MonsterHitDamageData data = GetHitDamageData();
			controller.TryReceiveHit(data);
		}

		protected bool CheckIfEnemy(GameObject go)
		{
			return go.layer == LayerMask.NameToLayer("Enemies");
		}

		protected virtual void DestroySelf()
		{
			Destroy(gameObject);
		}

		protected virtual MonsterHitDamageData GetHitDamageData()
		{
			MonsterHitDamageData data = new MonsterHitDamageData()
			{
				Bullet = this,
				DamageAmount = Damage
			};
			return data;
		}
	}
}
