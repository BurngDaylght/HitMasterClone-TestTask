using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 10f)] [SerializeField] private float _speed = 2f;
    [SerializeField] private Transform[] _wayPoints;
    
    private int _currentPointIndex = 0;
    private bool _canMove;

    private NavMeshAgent _navMeshAgent;

    private GameState _gameState;

    [Inject]
    private void Construct(GameState gameState)
    {
        _gameState = gameState;
    }

    private void OnEnable()
    {
        _gameState.OnGameStarted += EnableMovement;
        _gameState.OnGameFinished += DisableMovement;
    }

    private void OnDisable()
    {
        _gameState.OnGameStarted -= EnableMovement;
        _gameState.OnGameFinished -= DisableMovement;
    }
    
    private void EnableMovement()
    {
        _canMove = true;
        if (_wayPoints.Length > 0)
        {
            _navMeshAgent.SetDestination(_wayPoints[_currentPointIndex].position);
        }
    }

    private void DisableMovement()
    {
        _canMove = false;
        _navMeshAgent.ResetPath();
    }
        
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        if (!_canMove) return;

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                MoveToNextPoint();
            }
        }
    }
    
    private void MoveToNextPoint()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _wayPoints.Length)
        {
            _canMove = false;
            return;
        }

        _navMeshAgent.SetDestination(_wayPoints[_currentPointIndex].position);
    }
}
