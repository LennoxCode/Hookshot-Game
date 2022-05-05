using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingController : MonoBehaviour
{
    public static Action hookHit;
    public static Action unhooked;
    public Vector2 targetDirection {private set; get;}
    public Vector2 grappleOrigin {private set; get;}
    public Vector2 grapplePoint {private set; get;}
    [Header("Settings:")]
    [SerializeField] [Range(5, 100)]private float maxDistance;

    [SerializeField][Range(1, 6)]private float grappleSpeed;
    [Header("Refernces:")]
    [SerializeField] private Camera viewPort;
    [SerializeField] private Transform hookPivot;
    [SerializeField] private SpringJoint2D _joint2D;
    [SerializeField] private Transform gunNuzzle;
    [SerializeField] private RopeAnimationController _rac;

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
        grappleOrigin = gunNuzzle.position;
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !hooked) ShootHook();
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _joint2D.enabled = false;
            _rac.enabled = false;
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
                grapplePoint = _hit.point;
                grappleOrigin = gunNuzzle.position;
                _joint2D.enabled = true;
                _rac.enabled = true;
                targetDirection = direction;
            }
        }
    }
}
