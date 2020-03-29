using System.Collections.Generic;
using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Monster;
using TowerDefence.Unity.Spawning.Tower;
using TowerDefence.Unity.Storaging;
using TowerDefence.Unity.Tower;
using TowerDefence.Unity.TowerSpot;
using UnityEngine;

namespace TowerDefence.Unity.Player
{
	public class PlayerController : MonoBehaviour
	{
		private int _life;
		public int CastleLife
		{
			get => _life;
			private set
			{
				int oldValue = _life;
				_life = value;
				GlobalLevelEvents.Instance.InvokeOnPlayerLifeChange(_life - oldValue, _life);
			}
		}

		private int _gold;
		public int Gold
		{
			get => _gold;
			private set
			{
				int oldValue = _gold;
				_gold = value;
				GlobalLevelEvents.Instance.InvokeOnPlayerGoldChange(_gold - oldValue, _gold);
			}
		}

		private List<TowerController> _playerTowers = new List<TowerController>();

		private TowerSpawner _spawner;

		
		// generally this should go from out-battle game state, but in this task be don't have such a state.
		public string[] AvailableTowers { get; private set; }


		public void Init(TowerSpawner spawner)
		{
			_spawner = spawner;

			SetFields();
			SubscribeOnGlobalEvents();
		}

		private void SetFields()
		{
			PlayerData playerData = GeneralDataStorage.Instance.PlayerData;
			LevelData levelData = GeneralDataStorage.Instance.LevelData;
			CastleLife = playerData.LivesBonus + levelData.CastleLife;
			Gold = playerData.CoinBonus + levelData.StartingCoins;
			AvailableTowers = playerData.AvailableTowerTypes;
		}

		private void SubscribeOnGlobalEvents()
		{
			GlobalLevelEvents.Instance.OnMonsterEntersCastle += OnMonsterEntersCastle;
			GlobalLevelEvents.Instance.OnMonsterDies += OnMonsterDies;
		}

		public void AddTower(TowerController tower)
		{
			_playerTowers.Add(tower);
		}

		public void RemoveTower(TowerController tower)
		{
			_playerTowers.Remove(tower);
		}

		public void SpawnTower(TowerSpotController spot, string towerName)
		{
			LoseGoldForTowerSpawned(towerName);
			TowerController newTower = _spawner.SpawnTower(spot, towerName);
			AddTower(newTower);
		}

		private void LoseGoldForTowerSpawned(string towerName)
		{
			int towerCost = GeneralDataStorage.Instance.GetTowerData(towerName).Cost;
			Gold -= towerCost;
		}

		private void OnMonsterEntersCastle(MonsterController monster)
		{
			CastleLife -= monster.Damage;
		}

		private void OnMonsterDies(MonsterController monster)
		{
			Gold += monster.CoinValue;
		}
	}
}
