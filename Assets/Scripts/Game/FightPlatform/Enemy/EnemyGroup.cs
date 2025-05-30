using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public event Action OnAllEnemiesDead;
    private List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            Enemy enemy = child.GetComponent<Enemy>();
            if (enemy != null)
            {
                _enemies.Add(enemy);
            }
        }

        foreach (var enemy in _enemies)
        {
            enemy.OnEnemyDied += EnemyDeath;
        }
    }

    private void EnemyDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            OnAllEnemiesDead?.Invoke();
        }
    }
}
