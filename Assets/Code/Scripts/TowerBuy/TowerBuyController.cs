using System;
using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Interaction.Selection;
using TowerDefence.Unity.Service;
using TowerDefence.Unity.Spawning.Tower;
using TowerDefence.Unity.Tower;
using TowerDefence.Unity.TowerSpot;
using UnityEngine;

namespace TowerDefence.Unity.TowerBuy
{
	public class TowerBuyController : MonoSingleton<TowerBuyController>
	{
		[SerializeField] private TowerBuyMenuController MenuController;
		private TowerSpotController _currentSpot;

		public void Init(TowerSpotGroupSelectionController spotSelectionController)
		{
			SubscribeOnGlobalEvents();
			SubscribeSelectorOnTowerSpawnEvent(spotSelectionController);
		}

		private void SubscribeOnGlobalEvents()
		{
			GlobalSelectionEvents.Instance.OnTowerSpotSelectionChanged += OnEmptyTowerSpotSelected;
			GlobalLevelUiEvents.Instance.OnTowerVariantChosenFromBuymenu += SummonPaidTower;
		}

		private void SubscribeSelectorOnTowerSpawnEvent(TowerSpotGroupSelectionController spotSelectionController)
		{
			OnTowerWillSpawn += spotSelectionController.Unselect;
		}

		private void OnEmptyTowerSpotSelected(TowerSpotController towerSpot)
		{
			_currentSpot = towerSpot;
			if (_currentSpot != null)
				ShowBuyMenu();
			else
				HideBuyMenu();
		}

		private void ShowBuyMenu()
		{
			MenuController.MoveTo(_currentSpot.transform.position * 100f);
			MenuController.gameObject.SetActive(true);
		}

		private void HideBuyMenu()
		{
			MenuController.gameObject.SetActive(false);
		}

		private void SummonPaidTower(string towerName)
		{
			OnTowerWillSpawn?.Invoke();
			OnTowerNeedsSpawn?.Invoke(_currentSpot, towerName);
			HideBuyMenu();
		}

		public delegate void TowerSpawnData(TowerSpotController spot, string towerName);
		public event TowerSpawnData OnTowerNeedsSpawn;
		public event Action OnTowerWillSpawn;
	}
}
