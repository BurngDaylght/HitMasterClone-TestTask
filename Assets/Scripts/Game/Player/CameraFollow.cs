using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    [Range(0, 15)] [SerializeField] private float _speedMovement = 5f;
    [Range(0, 15)] [SerializeField] private float _speedRotation = 5f;
    [Range(0, 15)] [SerializeField] private float _speedOfChangeOffset = 5f;

    [SerializeField] private Vector3 _walkOffset = new Vector3(0, 1.5f, -3.5f);
    [SerializeField] private Vector3 _shootOffset = new Vector3(0, 2f, -2.5f);

    private Vector3 _currentOffset;
    private Vector3 _targetOffset;

    private void Start()
    {
        _currentOffset = _walkOffset;
        _targetOffset = _walkOffset;
    }

    private void Update()
    {
        _currentOffset = Vector3.Lerp(_currentOffset, _targetOffset, _speedOfChangeOffset * Time.deltaTime);
    }

    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
    }

    private void MoveCamera()
    {
        Vector3 targetCameraPosition = _target.position + _target.rotation * _currentOffset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, _speedMovement * Time.deltaTime);
    }

    private void RotateCamera()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speedRotation * Time.deltaTime);
    }

    public void SetWalkOffset()
    {
        _targetOffset = _walkOffset;
    }

    public void SetShootOffset()
    {
        _targetOffset = _shootOffset;
    }
}
