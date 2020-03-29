using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Service.ScriptableObjectSpawners;
using TowerDefence.Unity.Storaging.Data;

namespace TowerDefence.Unity.Storaging
{
	public class GeneralDataStorage
	{
		private static GeneralDataStorage _instance;
		private GeneralDataStorage() { }
		public static GeneralDataStorage Instance => _instance ?? (_instance = new GeneralDataStorage());

		private readonly MonsterNamedDataStorage _monsterNamedDataStorage = new MonsterNamedDataStorage();
		private readonly TowerNamedDataStorage _towerNamedDataStorage = new TowerNamedDataStorage();
		public readonly BulletDataStorage BulletsDataStorage = new BulletDataStorage();
		public LevelData LevelData { get; private set; }
		public PlayerData PlayerData { get; private set; }

		public void Init(LevelDataObject level, PlayerDataObject player, MonsterDataObject[] monsters, TowerDataObject[] towers, BulletsData data)
		{
			LevelData = level.Data;
			PlayerData = player.Data;
			_monsterNamedDataStorage.FillFromDataObjects(monsters);
			_towerNamedDataStorage.FillFromDataObjects(towers);
			BulletsDataStorage.Init(data);
		}

		public MonsterData GetMonsterData(string monsterName)
		{
			return _monsterNamedDataStorage[monsterName];
		}

		public TowerData GetTowerData(string towerName)
		{
			return _towerNamedDataStorage[towerName];
		}
	}
}
