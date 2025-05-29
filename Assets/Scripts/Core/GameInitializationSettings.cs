using UnityEngine;

public class GameInitializationSettings : MonoBehaviour
{
    [Range(-1, 240)]
    [SerializeField] private int _targetFrameRate = 144;
    [SerializeField] private bool _vSync;
    [SerializeField] private bool cursorLocked;

    private void Start()
    {
    	Application.targetFrameRate = _targetFrameRate; 
        QualitySettings.vSyncCount = _vSync ? 1 : 0;
        
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}