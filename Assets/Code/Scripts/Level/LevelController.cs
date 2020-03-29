using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Interaction.Selection;
using TowerDefence.Unity.Spawning.Monster;
using TowerDefence.Unity.Player;
using TowerDefence.Unity.Service;
using TowerDefence.Unity.Service.ScriptableObjectSpawners;
using TowerDefence.Unity.Spawning;
using TowerDefence.Unity.Spawning.Tower;
using TowerDefence.Unity.Storaging;
using TowerDefence.Unity.Storaging.Data;
using TowerDefence.Unity.TowerBuy;
using UnityEngine;

namespace TowerDefence.Unity.Level
{
	public class LevelController : MonoSingleton<LevelController>
	{
		[SerializeField] private PlayerController PlayerController;
		[SerializeField] private MonsterSpawnSequenceController MonsterSpawnSequenceController;
		[SerializeField] private MonsterSpawner MonsterSpawner;
		[SerializeField] private TowerSpawner TowerSpawner;
		[SerializeField] private GeneralBulletSpawner GeneralBulletSpawner;
		[SerializeField] private SelectionController SelectionController;
		[SerializeField] private TowerBuyController TowerBuyController;

		private bool _isInitialized = false;

		public PlayerController GetPlayer()
		{
			return PlayerController;
		}

		public void Init(PlayerDataObject playerData, 
						 LevelDataObject levelData, 
						 MonsterDataObject[] monsterData, 
						 TowerDataObject[] towerData,
						 BulletsData bulletsData) // todo: organize in structure
		{
			GeneralDataStorage.Instance.Init(levelData, playerData, monsterData, towerData, bulletsData);

			InitMonsterSpawnSequenceController();
			InitMonsterSpawner();
			InitPlayerController();
			InitSelectionController();
			InitTowerBuyController();
			InitGeneralBulletSpawner();
			_isInitialized = true;
		}

		private void InitMonsterSpawnSequenceController()
		{
			MonsterSpawnSequenceController.Init();
		}

		private void InitMonsterSpawner()
		{
			MonsterSpawnSequenceController.NeedSpawnMonster += MonsterSpawner.SpawnMonster;
		}

		private void InitPlayerController()
		{
			PlayerController.Init(TowerSpawner);
		}

		private void InitSelectionController()
		{
			SelectionController.Init();
		}

		private void InitTowerBuyController()
		{
			TowerBuyController.Init(SelectionController.TowerSpotsController);
			TowerBuyController.OnTowerNeedsSpawn += PlayerController.SpawnTower;
		}

		private void InitGeneralBulletSpawner()
		{
			GeneralBulletSpawner.Init();
		}

		private void Update()
		{
			if (_isInitialized)
			{
				MonsterSpawnSequenceController.Proceed(Time.deltaTime);
			}
		}
	}
}
