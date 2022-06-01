using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the Grapple Gun and all corresponding controls
/// </summary>
public class GrapplingController : MonoBehaviour
{
    public static Action hookHit;
    public static Action unhooked;

    public Vector2 targetDirection {private set; get;} // holds direction information for the grapple shot animation
    public Vector2 grappleOrigin {private set; get;} // where grapple comes from (primarily for rope animation)
    public Vector2 grapplePoint {private set; get; } // where grapple hooks (primarily for rope animation)

    [Header("Settings:")]
    [SerializeField] [Range(0.5f, 20)]private float maxDistance; // hook shot max distance

    [Header("Refernces:")]
    [SerializeField] private Camera viewPort;
    [SerializeField] private SpringJoint2D _joint2D;
    [SerializeField] private Transform gunNuzzle;
    [SerializeField] private GameObject hookSprite;
    [SerializeField] private RopeAnimationController _rac;

    private GameObject _hookedSprite; // holds duplicate of hook gameobject/sprite

    private bool hooked = false; // hooked?

    // Update is called once per frame
    private void Update()
    {
        grappleOrigin = gunNuzzle.position;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !hooked) ShootHook(); // if clicked and not hooked yet -> shoot hook

        if (Input.GetKeyDown(KeyCode.Mouse1)) Unhook(); // if right-clicked -> unhook
    }

    private void LateUpdate()
    {
        if(_joint2D.connectedBody) grapplePoint = _joint2D.connectedBody.position; // if connected to moving object -> refresh grapple point for correct rope display
    }

    /// <summary>
    /// Unhooks the player from hooked object and destroys the hook duplicate at grapplePoint
    /// </summary>
    public void Unhook()
    {
        hooked = false;

        // cut spring joint
        _joint2D.enabled = false; 
        _joint2D.connectedBody = null;

        _rac.enabled = false; // deactivate animation of rope
        unhooked?.Invoke();

        hookSprite.SetActive(true); // reactivate hookSprite at gun Nuzzle
        Destroy(_hookedSprite);
    }

    /// <summary>
    /// Shoots hook if hookable object is in reach
    /// </summary>
    private void ShootHook()
    {
        Vector2 direction = (gunNuzzle.position - transform.position).normalized;

        RaycastHit2D _hit = Physics2D.Raycast(gunNuzzle.position, direction, maxDistance);
        AudioManager.instance.Play("HookShoot");


        if (_hit) // if object in reach has been hit
        {
            hooked = true;
            AudioManager.instance.Play("HookAnchor");

            hookHit?.Invoke();

            if (_hit.rigidbody) // if hit object has rigidbody (only moving Obstacles)
            {
                _joint2D.connectedBody = _hit.rigidbody;
                grapplePoint = _hit.rigidbody.gameObject.transform.position;
            }
            else // static obstacle
            {
                _joint2D.connectedAnchor = _hit.point;
                grapplePoint = _hit.point;
            }
                
            _joint2D.enabled = true;

            // activate rope animation
            grappleOrigin = gunNuzzle.position;
            _rac.enabled = true; 
            targetDirection = direction;
            // Instantiate and transform hookSprite duplicate (part of animation)
            _hookedSprite = Instantiate(hookSprite, grapplePoint, transform.rotation);
            _hookedSprite.transform.localScale = hookSprite.transform.lossyScale;
            hookSprite.SetActive(false);
        }
        
    }
    
    /// <summary>
    /// Just for development
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gunNuzzle.position, maxDistance);
    }
}
