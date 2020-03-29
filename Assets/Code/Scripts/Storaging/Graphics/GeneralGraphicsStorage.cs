using UnityEngine;

namespace TowerDefence.Unity.Storaging.Graphics
{
	public class GeneralGraphicsStorage
	{
		private static GeneralGraphicsStorage _instance;
		public static GeneralGraphicsStorage Instance => _instance ?? (_instance = new GeneralGraphicsStorage());

		private readonly SpriteStorage _monsterSpriteStorage = new SpriteStorage(GraphicPathConstants.MonsterGraphicsPath);
		private readonly SpriteStorage _towerSpriteStorage = new SpriteStorage(GraphicPathConstants.TowerGraphicsPath);
		
		private GeneralGraphicsStorage()
		{
			// todo: make spriteStorages not to fill by themselves; it should be done by another class
			_monsterSpriteStorage.Fill();
			_towerSpriteStorage.Fill();
		}

		public Sprite GetMonsterSprite(string monsterName) => _monsterSpriteStorage[monsterName];
		public Sprite GetTowerSprite(string towerName) => _towerSpriteStorage[towerName];
	}
}
