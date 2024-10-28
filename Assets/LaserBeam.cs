using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    #region member variables

    public float _maxDistance = 5f;

    private LineRenderer _lineRenderer;
    private List<Vector3> _points = new List<Vector3>();

    #endregion

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {
        // shoot laser beam
        _points.Clear();
        _points.Add(transform.position);

        bool foundHit = true;
        Vector3 direction = transform.up;

        while (foundHit)
        {
            // from the current point, raycast up and check for collisions with mirrors
            Vector3 currentPosition = _points[_points.Count - 1];
            if (Physics.Raycast(currentPosition, direction, out RaycastHit hit, _maxDistance))
            {
                _points.Add(hit.point);
                if (hit.collider.CompareTag("Mirror"))
                {
                    // calculate reflection direction
                    Vector3 inDirection = direction;
                    Vector3 normal = hit.normal;
                    direction = Vector3.Reflect(inDirection, normal);
                }
                else if (hit.collider.TryGetComponent<LaserReceiver>(out LaserReceiver receiver))
                {
                    receiver.Activate();
                    // stop the laser beam
                    foundHit = false;
                }
                else
                {
                    // stop the laser beam
                    foundHit = false;
                }
            }
            else
            {
                _points.Add(currentPosition + direction * _maxDistance);
                // no hit
                foundHit = false;
            }
        }

        // apply points to line renderer
        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPositions(_points.ToArray());
    }
}
