using System.Collections.Generic;
using TowerDefence.Core.DataStructure;
using TowerDefence.Unity.GlobalEvents;
using TowerDefence.Unity.Level.Pathing.MonsterPath;
using TowerDefence.Unity.Service;
using TowerDefence.Unity.Storaging;
using UnityEngine;

namespace TowerDefence.Unity.Spawning.Monster
{
	public class MonsterSpawnSequenceController : MonoBehaviour
	{
		[SerializeField] private Transform MonsterSpawnArea;
		[SerializeField] private MonsterPathes Pathes;
		private readonly List<WaveData> _waves = new List<WaveData>();
		
		private int _currentWaveNumber = -1;
		private float _timer = 0f;
		private readonly List<WaveSpawnSequence> _wavesCurrentlySpawning = new List<WaveSpawnSequence>();
		private int WavesAmount => _waves.Count;
		private bool OnFirstWave => _currentWaveNumber == -1;
		private bool OnLastWave => _currentWaveNumber >= WavesAmount - 1;
		private WaveData CurrentWave => _waves[_currentWaveNumber];
		private bool NoWavesSpawning => _wavesCurrentlySpawning.Count == 0;

		public void Init()
		{
			_waves.AddRange(GeneralDataStorage.Instance.LevelData.Waves);
		}

		public void ForceNextWave() // Made for "Force Wave" TD mechanics.
		{
			if (OnLastWave)
				return;

			SpawnNextWave();
		}

		public void Proceed(float time)
		{
			if(NoWavesSpawning)
			{
				// Onwaitingfornextwave event?..
				AdvanceTimer(time);

				if (OnFirstWave || 
				    _timer > CurrentWave.NextWaveTime)
				{
					if (!OnLastWave)
					{
						SpawnNextWave();
						GlobalLevelEvents.Instance.InvokeOnNewWave(_currentWaveNumber);
					}
					else
					{

					}
				}
			}

			ProceedWaves();
		}

		private void AdvanceTimer(float time)
		{
			_timer += time;
		}

		private void ResetTimer()
		{
			_timer = 0f;
		}
		
		private void SpawnNextWave()
		{
			ResetTimer();
			_currentWaveNumber++;
			WaveSpawnSequence spawnSequence = new WaveSpawnSequence(CurrentWave);
			_wavesCurrentlySpawning.Add(spawnSequence);
			spawnSequence.NeedSpawnMonster += InvokeNeedSpawnMonsterEvent;
		}

		private void ProceedWaves()
		{
			for(int i = _wavesCurrentlySpawning.Count - 1; i >= 0; i--)
			{
				WaveSpawnSequence wave = _wavesCurrentlySpawning[i];
				var proceedProceedResult = wave.Proceed(Time.deltaTime);
				if (proceedProceedResult == WaveSpawnSequenceProceedResult.WaveFinished)
				{
					_wavesCurrentlySpawning.RemoveAt(i);
					// Probaby some kind of event on wave finish?
				}
			}
		}

		private void InvokeNeedSpawnMonsterEvent(string monsterName, int spawnPointIndex, int pathIndex)
		{
			NeedSpawnMonster?.Invoke(monsterName, spawnPointIndex, pathIndex);
		}

		public delegate void MonsterSpawnData(string monsterName, int spawnPointIndex, int pathIndex);
		public event MonsterSpawnData NeedSpawnMonster;
	}
}
