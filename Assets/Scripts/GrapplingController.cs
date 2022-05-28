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
    [SerializeField] [Range(0.5f, 20)]private float maxDistance;

    [Header("Refernces:")]
    [SerializeField] private Camera viewPort;
    [SerializeField] private SpringJoint2D _joint2D;
    [SerializeField] private Transform gunNuzzle;
    [SerializeField] private RopeAnimationController _rac;
    private bool hooked = false;

    // Update is called once per frame
    private void Update()
    {
        grappleOrigin = gunNuzzle.position;
        Vector3 mousePos = viewPort.ScreenToWorldPoint(Input.mousePosition);
        
        if(Input.GetKeyDown(KeyCode.Mouse0) && !hooked) ShootHook();
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Unhook();
        }
    }

    private void LateUpdate()
    {
        if(_joint2D.connectedBody) grapplePoint = _joint2D.connectedBody.position;
    }

    public void Unhook()
    {
            _joint2D.enabled = false;
            _rac.enabled = false;
            unhooked?.Invoke();
            _joint2D.connectedBody = null;
            hooked = false;
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
                if (_hit.rigidbody)
                {
                    _joint2D.connectedBody = _hit.rigidbody;
                    grapplePoint = _hit.rigidbody.gameObject.transform.position;
                }
                else
                {
                    _joint2D.connectedAnchor = _hit.point;
                    grapplePoint = _hit.point;
                }
                
                grappleOrigin = gunNuzzle.position;
                _joint2D.enabled = true;
                _rac.enabled = true;
                targetDirection = direction;
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gunNuzzle.position, maxDistance);
    }
}
