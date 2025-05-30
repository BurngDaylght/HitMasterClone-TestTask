using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public void SetRunning(bool isRunning)
    {
        _animator.SetBool("IsRunning", isRunning);
    }
}
