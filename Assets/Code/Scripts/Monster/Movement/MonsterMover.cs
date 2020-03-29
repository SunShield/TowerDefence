using TowerDefence.Unity.Level.Pathing.MonsterPath;
using UnityEngine;

namespace TowerDefence.Unity.Monster
{
	public class MonsterMover : MonoBehaviour
	{
		private const float SpeedMultiplier = 0.1f;
		private const float NodeDistanceTolerance = 0.05f;

		private Transform _objectToMove;
		private MonsterPath _path;
		private bool _isMoving = true;

		private int _currentPathNode = -1;
		public float Speed { get; set; }
		private int PathLength => _path.Size;
		private Vector3 NextCoords => _path[_currentPathNode].position;
		private bool OnFirstNode => _currentPathNode == -1;
		private bool OnLastNode => _currentPathNode == PathLength - 1;
		private float DistanceToNextNode => (NextCoords - _objectToMove.position).magnitude;
		private Vector3 DirectionVector => (NextCoords - _objectToMove.position).normalized;

		public void Init(Transform objectToMove, float speed)
		{
			_objectToMove = objectToMove;
			Speed = speed;
		}

		public void StartMoving()
		{
			_isMoving = true;
		}

		public void StopMoving()
		{
			_isMoving = false;
		}

		public void SetPath(MonsterPath path)
		{
			_path = path;
		}

		private void Update()
		{
			if (_isMoving)
			{
				if (OnFirstNode ||
				    DistanceToNextNode <= NodeDistanceTolerance)
				{
					if(!OnLastNode)
						GoToNextNode();
					else
						StopMoving();
				}

				Move();
			}
		}

		private void Move()
		{
			float fullDistance = (DirectionVector * Speed * SpeedMultiplier * Time.deltaTime).magnitude;
			if (fullDistance > DistanceToNextNode)
			{

				DoMove(NextCoords - _objectToMove.position);
			}
			else
				DoMove();
		}

		private void DoMove()
		{
			_objectToMove.position += DirectionVector * Speed * SpeedMultiplier * Time.deltaTime;
		}

		private void DoMove(Vector3 moveVector)
		{
			_objectToMove.position += moveVector;
		}

		private void GoToNextNode()
		{
			_currentPathNode++;
			SetLookAtNextNode();
		}

		private void SetLookAtNextNode()
		{
			float rotation = -Mathf.Atan2(DirectionVector.x, DirectionVector.y) * Mathf.Rad2Deg;
			_objectToMove.rotation = Quaternion.Euler(0f, 0f, rotation);
		}

		public float GetDistanceToCastle()
		{
			float distanceBetweenNodesRemaining = 0f;
			for (int i = _currentPathNode; i < PathLength - 1; i++)
			{
				distanceBetweenNodesRemaining += (_path[i].position - _path[i + 1].position).magnitude;
			}

			return DistanceToNextNode + distanceBetweenNodesRemaining;
		}
	}
}
