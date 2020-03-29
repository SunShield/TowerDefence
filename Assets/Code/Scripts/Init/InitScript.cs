using TowerDefence.Unity.Level;
using TowerDefence.Unity.Service.ScriptableObjectSpawners;
using TowerDefence.Unity.Storaging.Data;
using TowerDefence.Unity.UI;
using UnityEngine;

namespace TowerDefence
{
	public class InitScript : MonoBehaviour
	{
		[SerializeField] private PlayerDataObject PlayerData;
		[SerializeField] private LevelDataObject LevelDataObject;
		// todo: move on the topper level of game states (possibly in the base scene which will be loaded into each battle)
		[SerializeField] private MonsterDataObject[] MonsterData; 
		[SerializeField] private TowerDataObject[] TowerData;

		[SerializeField] private ArrowTowerBulletDataObject ArrowBulletData;
		[SerializeField] private DeathrayTowerBulletDataObject DeathrayBulletData;
		[SerializeField] private FreezeTowerBulletDataObject FreezeBulletData;
		[SerializeField] private SpikeballTowerBulletDataObject SpikeballBulletData;

		void Start()
		{
			InitLevel();
			InitUi();
		}

		private void InitLevel()
		{
			BulletsData bulletsData = new BulletsData()
			{
				ArrowTowerBulletData = ArrowBulletData.Data,
				DeathrayTowerBulletData = DeathrayBulletData.Data,
				FreezeTowerBulletData = FreezeBulletData.Data,
				SpikeballTowerBulletData = SpikeballBulletData.Data
			};
			LevelController.Instance.Init(PlayerData, LevelDataObject, MonsterData, TowerData, bulletsData);
		}

		private void InitUi()
		{
			LevelUiController.Instance.Init();
		}
	}
}