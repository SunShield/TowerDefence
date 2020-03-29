using TowerDefence.Unity.Storaging.Graphics;
using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.Player;
using TowerDefence.Unity.Storaging;
using TowerDefence.Unity.Tower;
using TowerDefence.Unity.TowerSpot;
using UnityEngine;

namespace TowerDefence.Unity.Spawning.Tower
{
	public class TowerSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject TowerPrefab;
		[SerializeField] private Transform TowerOrigin;

		private int _currentTowerId = 0;

		public TowerController SpawnTower(TowerSpotController spot, string towerName)
		{
			GameObject go = CreateTowerGameObject(towerName, spot.transform.position);
			TowerController controller = ConfigureTowerController(towerName, go);
			SetTowerId(controller);
			spot.SetTower(controller);
			return controller;
		}

		private GameObject CreateTowerGameObject(string towerName, Vector3 coords)
		{
			GameObject towerGo = Instantiate(TowerPrefab, coords, Quaternion.identity);
			towerGo.name = towerName;
			towerGo.transform.parent = TowerOrigin;
			return towerGo;
		}

		private TowerController ConfigureTowerController(string towerName, GameObject go)
		{
			TowerController controller = go.GetComponent<TowerController>();
			TowerData data = GetTowerData(towerName);
			controller.Init(data);
			ConfigureTowerGraphics(towerName, controller);
			return controller;
		}

		private TowerData GetTowerData(string towerName)
		{
			return GeneralDataStorage.Instance.GetTowerData(towerName);
		}

		private void ConfigureTowerGraphics(string towerName, TowerController controller)
		{
			controller.SetSprite(GeneralGraphicsStorage.Instance.GetTowerSprite(towerName));
		}

		private void SetTowerId(TowerController controller)
		{
			controller.EntityId = _currentTowerId;
			_currentTowerId++;
		}
	}
}
