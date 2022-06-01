
using System;
using UnityEngine;

/// <summary>
/// Manages Player movement and corresponding Inputs
/// </summary>
public class PlayerController : MonoBehaviour
{
    public PlayerState currentState;

    /*[SerializeField] private float movementSpeed;*/ // No more Movement without hook
    /*[SerializeField] private float jumpHeight;*/ // No more jumps

    [SerializeField] private SpringJoint2D _joint2D;
  
    private Rigidbody2D rb; // player/gun rigidbody

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        RopeAnimationController.hookArrived += ChangeStateToHooked;
        GrapplingController.unhooked += Unhook;
    }


    // Update is called once per frame
    void Update()
    {
        /*
        int layerMask = 1 << 2;
        layerMask = ~layerMask;
        */

        // if pulling on hook -> squeeze spring joint
        if (Input.GetKey(KeyCode.Mouse0) && currentState == PlayerState.Hooked) 
        {
            _joint2D.distance = 0.5f;
        }
        // if releasing -> set spring distance to current distance to hook
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _joint2D.distance = _joint2D.connectedBody ? (_joint2D.connectedBody.position - (Vector2)transform.position).magnitude : (_joint2D.connectedAnchor - (Vector2) transform.position).magnitude;
        }

        /* No more movement without hook
        if (Input.GetKey(KeyCode.A) && currentState == PlayerState.Hooked && rb.velocity.x > -5) rb.AddForce(Vector2.left * movementSpeed * rb.mass);
        if (Input.GetKey(KeyCode.D) && currentState == PlayerState.Hooked && rb.velocity.x < 5) rb.AddForce(Vector2.right * movementSpeed * rb.mass);
        */
        
    }

    /// <summary>
    /// Player State is now hooked and for better gameplay feeling the mass gets reduced
    /// </summary>
    private void ChangeStateToHooked()
    {
        currentState = PlayerState.Hooked;
        rb.mass = 0.3f;
    }

    /// <summary>
    /// Player State is now unhooked and mass gets set back to normal
    /// </summary>
    private void Unhook()
    {
        rb.mass = 1;
        currentState = PlayerState.Neutral;    
    }

    private void OnDestroy()
    {
        RopeAnimationController.hookArrived -= ChangeStateToHooked;
        GrapplingController.unhooked -= Unhook;
    }

    public enum PlayerState
    {
        Neutral,
        Hooked
    }
    
}
