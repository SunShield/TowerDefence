using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Bullet;
using UnityEngine;

namespace TowerDefence.Unity.Spawning
{
	public class BulletSpawner<T, K> : MonoBehaviour where T : BaseBulletData where K: GenericInitableBulletController<T>
	{
		[SerializeField] protected GameObject BulletPrefab;
		protected T DataStored;

		public virtual void Init(T data)
		{
			// This approach is kinda only generic approach I've found so far do give some generalization to bullet spawning process.
			// Don't really like it but can't find anything better huh.
			DataStored = data;
			K controller = BulletPrefab.GetComponent<K>();
			
		}

		public K SpawnBullet(Vector3 position, BulletTargetData targetData)
		{
			GameObject go = Instantiate(BulletPrefab, position, Quaternion.identity);
			K controller = go.GetComponent<K>();
			controller.SetTarget(targetData);
			controller.Init(position, DataStored);
			return controller;
		}
	}
}
