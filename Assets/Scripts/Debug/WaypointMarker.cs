using UnityEngine;
using UnityEditor;

public class WaypointMarker : MonoBehaviour
{
    [SerializeField] private Color _color = Color.red;
    [SerializeField] private float _sphereRadius = 0.5f;
    [SerializeField] private float _maxRayDistance = 100f;

    private void OnDrawGizmos()
    {
        Handles.color = _color;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _maxRayDistance))
        {
            Handles.DrawLine(transform.position, hit.point);
            Handles.DrawWireDisc(hit.point, hit.normal, _sphereRadius);
        }
    }
}
