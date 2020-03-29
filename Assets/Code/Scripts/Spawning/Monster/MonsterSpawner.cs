using TowerDefence.Unity.Storaging.Graphics;
using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Level.Pathing.MonsterPath;
using TowerDefence.Unity.Monster;
using TowerDefence.Unity.Storaging;
using UnityEngine;

namespace TowerDefence.Unity.Spawning.Monster
{
	public class MonsterSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject MonsterPrefab;
		[SerializeField] private Transform[] MonsterSpawnPoints;
		[SerializeField] private MonsterPathes Pathes;
		[SerializeField] private Transform MonsterOrigin;

		private int _currentMonsterId = 0;

		public void SpawnMonster(string monsterName, int spawnPointIndex, int pathIndex)
		{
			SpawnMonsterInternal(monsterName, spawnPointIndex, pathIndex);
			// OnMonsterSpawned event here?
		}

		private void SpawnMonsterInternal(string monsterName, int spawnPointIndex, int pathIndex)
		{
			GameObject go = CreateMonsterGo(monsterName, spawnPointIndex);
			ConfigureMonsterController(go, monsterName, pathIndex);
		}

		private GameObject CreateMonsterGo(string monsterName, int spawnPointIndex)
		{
			GameObject monsterGo = Instantiate(MonsterPrefab, GetMonsterCoords(spawnPointIndex), Quaternion.identity);
			monsterGo.name = monsterName;
			monsterGo.transform.parent = MonsterOrigin;
			return monsterGo;
		}

		private Vector3 GetMonsterCoords(int spawnPointIndex)
		{
			return MonsterSpawnPoints[spawnPointIndex].position;
		}

		private void ConfigureMonsterController(GameObject go, string monsterName, int pathIndex)
		{
			MonsterController controller = go.GetComponent<MonsterController>();
			MonsterData data = GetMonsterData(monsterName);
			controller.Init(data);
			SetMonsterId(controller);
			ConfigureMonsterGraphics(controller, monsterName);
			ConfigureMover(controller, pathIndex);
		}

		private MonsterData GetMonsterData(string monsterName)
		{
			return GeneralDataStorage.Instance.GetMonsterData(monsterName);
		}

		private void SetMonsterId(MonsterController controller)
		{
			controller.EntityId = _currentMonsterId;
			_currentMonsterId++;
		}

		private void ConfigureMonsterGraphics(MonsterController controller, string monsterName)
		{
			controller.SetSprite(GeneralGraphicsStorage.Instance.GetMonsterSprite(monsterName));
		}

		private void ConfigureMover(MonsterController controller, int pathIndex)
		{
			controller.GetMover().SetPath(Pathes[pathIndex]);
		}
	}
}
