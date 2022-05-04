using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingController : MonoBehaviour
{
    public static Action hookHit;
    public static Action unhooked;
    [SerializeField] private float maxDistance;
    [SerializeField] private Camera viewPort;

    [SerializeField] private Transform hookPivot;
    [SerializeField] private SpringJoint2D _joint2D;
    [SerializeField] private Transform gunNuzzle;

    [SerializeField] private LineRenderer _lineRenderer;

    private bool hooked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = viewPort.ScreenToWorldPoint(Input.mousePosition);
        RotateHookShot(mousePos);
        _lineRenderer.SetPosition(0, transform.position);
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !hooked) ShootHook();
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _joint2D.enabled = false;
            _lineRenderer.enabled = false;
            unhooked?.Invoke();
            hooked = false;
        }
    }

    private void RotateHookShot(Vector3 to)
    {
        Vector2 distanceVector = to - hookPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        hookPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ShootHook()
    {
        Vector2 direction = (gunNuzzle.position - transform.position).normalized;
        if (Physics2D.Raycast(gunNuzzle.position, direction))
        {
            RaycastHit2D _hit = Physics2D.Raycast(gunNuzzle.position, direction);
            if (Vector2.Distance(_hit.point, gunNuzzle.position) <= maxDistance)
            {
                hooked = true;
                hookHit?.Invoke();
                _joint2D.connectedAnchor = _hit.point;
                _joint2D.enabled = true;
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _hit.point);
            }
        }
    }
}
