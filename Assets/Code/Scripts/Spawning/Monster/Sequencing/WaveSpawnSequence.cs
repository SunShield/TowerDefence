using TowerDefence.Core.DataStructure;

namespace TowerDefence.Unity.Spawning.Monster
{
	public enum WaveSpawnSequenceProceedResult
	{
		NothingHappened = 0,
		WaveFinished = 1
	}

	public class WaveSpawnSequence
	{
		private readonly WaveData _data;

		private int _currentGroupNumber = -1;
		private int _currentMonsterNumber = -1;
		private float _timer = 0f;
		private WaveMonsterGroupData CurrentGroup => _data.GroupsData[_currentGroupNumber];
		private string CurrentMonsterName => CurrentGroup.MonsterType;
		private int CurrentMonsterSpawnPoint => CurrentGroup.SpawnPointIndex;
		private int CurrentMonsterPath => CurrentGroup.MonsterPathIndex;
		private bool OnFirstGroup => _currentGroupNumber == -1;
		private bool HasGroupsPending => _currentGroupNumber < _data.MaxGroupNumber;
		private bool PreviousGroupSpawned => _timer > CurrentGroup.GroupTime;
		private bool NeedToSpawnNextMonster => _currentMonsterNumber + 1 < CurrentGroup.MonsterAmount &&
											   _timer / CurrentGroup.MonsterSpawnDelay >= (_currentMonsterNumber + 1);

		public WaveSpawnSequence(WaveData data)
		{
			_data = data;
		}

		public WaveSpawnSequenceProceedResult Proceed(float time)
		{
			AdvanceTimer(time);

			if (OnFirstGroup ||
			    PreviousGroupSpawned)
			{
				ResetTimers();
				if (HasGroupsPending)
					SpawnNextGroup();
				else
					return WaveSpawnSequenceProceedResult.WaveFinished;
			}
			
			if (NeedToSpawnNextMonster)
			{
				SpawnNextMonster();
			}

			return WaveSpawnSequenceProceedResult.NothingHappened;
		}

		private void AdvanceTimer(float time)
		{
			_timer += time;
		}

		private void ResetTimers()
		{
			_timer = 0f;
		}

		private void SpawnNextGroup()
		{
			_currentGroupNumber++;
			_currentMonsterNumber = -1;
		}

		private void SpawnNextMonster()
		{
			_currentMonsterNumber++;
			InvokeNeedSpawnMonsterEvent();
		}

		private void InvokeNeedSpawnMonsterEvent()
		{
			NeedSpawnMonster?.Invoke(CurrentMonsterName, 
									 CurrentMonsterSpawnPoint, 
									 CurrentMonsterPath);
		}

		public event MonsterSpawnSequenceController.MonsterSpawnData NeedSpawnMonster;
	}
}
