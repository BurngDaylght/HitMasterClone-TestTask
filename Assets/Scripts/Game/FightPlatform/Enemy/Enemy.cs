using UnityEngine;
using System;
using Zenject;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health = 3;
    [SerializeField] private float _speedRotation = 3f;
    [SerializeField] private Animator _animator;

    public event Action<Enemy> OnEnemyDied;
    private bool _isDie;
    
    private EnemyUI _enemyUI;
    private PlayerMovement _playerMovement;
    
    [Inject]
    private void Construct(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    private void Awake()
    {
        _enemyUI = GetComponent<EnemyUI>();
    }

    private void Start()
    {
        SetRigidbodiesState(true);
        SetCollidersState(false);
    }

    private void Update()
    {
        RotateEnemy();
    }
    
    private void RotateEnemy()
	{
        if (_isDie) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(_playerMovement.gameObject.transform.position - transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speedRotation * Time.deltaTime);
	}

    public void TakeDamage(int damage)
    {
        _health -= damage;
        CheckHealth();
    }
    
    private void CheckHealth()
    {
        _enemyUI.DeleteHeartImage();
    
        if (_health <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        _isDie = true;
    
        OnEnemyDied?.Invoke(this);

        _animator.enabled = false;
        SetRigidbodiesState(false);
        SetCollidersState(true);
    }
    
    private void SetRigidbodiesState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }
    
    private void SetCollidersState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        
        foreach (var collider in colliders)
        {
            collider.enabled = state;
        }
        
        GetComponent<Collider>().enabled = !state;
    }
}
