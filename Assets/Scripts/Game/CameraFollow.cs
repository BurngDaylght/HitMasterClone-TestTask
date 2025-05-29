using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[Range(0, 15)] [SerializeField] private float _speedMovement = 5f;
	[Range(0, 15)] [SerializeField] private float _speedRotation = 5f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 1.5f, -3.5f);

	[SerializeField] private Transform _target;

	private void LateUpdate()
	{
        MoveCamera();
        RotateCamera();
	}
	
	private void MoveCamera()
	{
	    Vector3 targetCameraPosition = _target.position + _target.rotation * _offset;
		transform.position = Vector3.Lerp(transform.position, targetCameraPosition, _speedMovement * Time.deltaTime);
	}
	
	private void RotateCamera()
	{
        Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speedRotation * Time.deltaTime);
	}
}
