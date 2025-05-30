using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Range(0f, 10f)] 
    [SerializeField] private float _speed = 2f;

    [SerializeField] private List<BasePlatform> _platforms;

    [SerializeField] private int _currentPlatformIndex = 0;
    [SerializeField] private bool _canMove;

    private NavMeshAgent _navMeshAgent;

    private GameState _gameState;
    private CameraFollow _cameraFollow;
    private Shooting _shooting;
    private PlayerAnimations _playerAnimations;
    private SceneLoader _sceneLoader;

    [Inject]
    private void Construct(GameState gameState, CameraFollow cameraFollow, Shooting shooting, PlayerAnimations playerAnimations, SceneLoader sceneLoader)
    {
        _gameState = gameState;
        _cameraFollow = cameraFollow;
        _shooting = shooting;
        _playerAnimations = playerAnimations;
        _sceneLoader = sceneLoader;
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

        if (_currentPlatformIndex >= 0 && _currentPlatformIndex < _platforms.Count)
            UnsubscribeFromPlatformEvent(_currentPlatformIndex);
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _speed;
    }

    private void Start()
    {
        if (_platforms.Count > 0)
        {
            transform.position = _platforms[0].WayPoint.position;
        }
        else
        {
            Debug.LogWarning("The list of platforms is empty!");
        }
    }

    private void EnableMovement()
    {
        if (_platforms.Count == 0)
        {
            Debug.LogWarning("There are no platforms for movement!");
            return;
        }

        _canMove = true;
        MoveToCurrentPlatform();
    }

    private void DisableMovement()
    {
        _canMove = false;
        _navMeshAgent.ResetPath();
        UnsubscribeFromPlatformEvent(_currentPlatformIndex);
    }

    private void Update()
    {
        if (!_canMove) return;
        
        if (_navMeshAgent.velocity.sqrMagnitude > 0.1f)
            _playerAnimations.SetRunning(true);
        else
            _playerAnimations.SetRunning(false);

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                if (_platforms[_currentPlatformIndex].IsCleared)
                    MoveToNextPlatform();
                else
                {
                    _canMove = false;
                    _cameraFollow.SetShootOffset();
                    
                    if (_currentPlatformIndex < _platforms.Count)
                        _shooting.EnableShooting();
                        
                    SubscribeToPlatformEvent(_currentPlatformIndex);
                }
    }

    private void MoveToCurrentPlatform()
    {
        if (_currentPlatformIndex >= _platforms.Count)
        {
            _canMove = false;
            Debug.Log("Level Completed!");
            _sceneLoader.RestartSceneWithDelay(1f);
            return;
        }

        var currentPlatform = _platforms[_currentPlatformIndex];
        if (currentPlatform == null || currentPlatform.WayPoint == null)
        {
            _currentPlatformIndex++;
            MoveToCurrentPlatform();
            return;
        }

        _navMeshAgent.SetDestination(currentPlatform.WayPoint.position);
    }

    private void MoveToNextPlatform()
    {
        UnsubscribeFromPlatformEvent(_currentPlatformIndex);

        _cameraFollow.SetWalkOffset();
        _shooting.DisableShooting();

        _currentPlatformIndex++;
        _canMove = true;
        MoveToCurrentPlatform();
    }

    private void SubscribeToPlatformEvent(int index)
    {
        if (index >= 0 && index < _platforms.Count)
            _platforms[index].OnPlatformCleared += PlatformCleared;
    }

    private void UnsubscribeFromPlatformEvent(int index)
    {
        if (index >= 0 && index < _platforms.Count)
            _platforms[index].OnPlatformCleared -= PlatformCleared;
    }

    private void PlatformCleared()
    {
        _canMove = true;
        MoveToNextPlatform();
    }
}
