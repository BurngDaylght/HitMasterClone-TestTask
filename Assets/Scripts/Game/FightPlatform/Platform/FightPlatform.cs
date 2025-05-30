using System;
using UnityEngine;

public class FightPlatform : BasePlatform
{
    [SerializeField] private EnemyGroup _enemyGroup;
    public EnemyGroup EnemyGroup => _enemyGroup;

    public override event Action OnPlatformCleared;

    private bool _isCleared = false;
    public override bool IsCleared => _isCleared;

    private void OnEnable()
    {
        _enemyGroup.OnAllEnemiesDead += PlatformClear;
    }

    private void OnDisable()
    {
        _enemyGroup.OnAllEnemiesDead -= PlatformClear;
    }

    private void PlatformClear()
    {
        _isCleared = true;
        OnPlatformCleared?.Invoke();
    }
}
