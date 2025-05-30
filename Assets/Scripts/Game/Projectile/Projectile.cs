using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 1;
    
    private CustomPool<Projectile> _pool;
    
    public void SetPool(CustomPool<Projectile> pool)
    {
        _pool = pool;
        Invoke(nameof(ReturnToPool), 3f);
    }
    
    private void ReturnToPool()
    {
        _pool.Release(this);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }

        ReturnToPool();
    }
}
