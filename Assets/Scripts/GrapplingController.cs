using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingController : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private Camera viewPort;

    [SerializeField] private Transform hookPivot;
    [SerializeField] private SpringJoint2D _joint2D;
    [SerializeField] private Transform gunNuzzle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = viewPort.ScreenToWorldPoint(Input.mousePosition);
        RotateHookShot(mousePos);
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) ShootHook();
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
                _joint2D.connectedAnchor = _hit.point;
            }
        }
    }
}
