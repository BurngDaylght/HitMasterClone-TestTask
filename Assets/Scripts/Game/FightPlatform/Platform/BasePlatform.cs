using System;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour
{
    [SerializeField] protected Transform _wayPoint;
    public Transform WayPoint => _wayPoint;

    public abstract bool IsCleared { get; }
    public abstract event Action OnPlatformCleared;
}

