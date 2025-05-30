using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Projectile Pool Settings")]
    [SerializeField] private Transform _poolParent;
    [SerializeField] private int _prewarmObjectsCount;
    private CustomPool<Projectile> _projectilePool;
    
    [Header("Shooting Settings")]
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private float _timer = 0f;
    private bool _isActive = false;
    
    private Camera _mainCamera;

    private void Start()
    {
        _projectilePool = new CustomPool<Projectile>(_projectilePrefab, _prewarmObjectsCount, _poolParent);
        
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_timer <= 0 && _isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RotatePlayer(GetPointFromTap());
                SpawnProjectile(GetPointFromTap());

                _timer = _fireRate;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    private Vector3 GetPointFromTap()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        float rayDistance = 50f;

        return ray.origin + ray.direction * rayDistance;
    }

    private void RotatePlayer(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }

    private void SpawnProjectile(Vector3 targetPoint)
    {
        Vector3 spawnPosition = _mainCamera.transform.position + _mainCamera.transform.forward * 0.5f;

        Vector3 direction = (targetPoint - spawnPosition).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        Projectile projectile = _projectilePool.Get();
        projectile.transform.position = spawnPosition;
        projectile.transform.rotation = rotation;
        projectile.SetPool(_projectilePool);
    }

    public void EnableShooting()
    {
        _isActive = true;
    }

    public void DisableShooting()
    {
        _isActive = false;
    }
}
