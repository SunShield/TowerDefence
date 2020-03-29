using TMPro;
using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Storaging;
using UnityEngine;

namespace TowerDefence.Unity.UI
{
	public class MainDataUIController : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI LivesText;
		[SerializeField] private TextMeshProUGUI GoldText;
		[SerializeField] private TextMeshProUGUI WaveText;

		private int _wavesTotalStored;

		public void Init()
		{
			SetStartingTexts();
			GlobalLevelEvents.Instance.OnPlayerLifeChange += UpdateLivesText;
			GlobalLevelEvents.Instance.OnPlayerGoldChange += UpdateGoldText;
			GlobalLevelEvents.Instance.OnNewWave += UpdateWaveText;
		}

		private void SetStartingTexts()
		{
			LivesText.text = (GeneralDataStorage.Instance.LevelData.CastleLife +
			                  GeneralDataStorage.Instance.PlayerData.LivesBonus).ToString();
			GoldText.text = (GeneralDataStorage.Instance.LevelData.StartingCoins +
			                 GeneralDataStorage.Instance.PlayerData.CoinBonus).ToString();
			WaveText.text = $"0/{GeneralDataStorage.Instance.LevelData.Waves.Length}";
		}

		private void UpdateLivesText(int difference, int newLivesAmount)
		{
			LivesText.text = newLivesAmount.ToString();
		}

		private void UpdateGoldText(int difference, int newGoldAmount)
		{
			GoldText.text = newGoldAmount.ToString();
		}

		private void UpdateWaveText(int currentWave)
		{
			int wavesTotal = GeneralDataStorage.Instance.LevelData.Waves.Length;
			WaveText.text = $"{currentWave}/{wavesTotal}";
		}
	}
}
