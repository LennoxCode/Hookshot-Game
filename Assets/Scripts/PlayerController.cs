
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
   
    public PlayerState currentState;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform raycastStart;
    [SerializeField] private SpringJoint2D _joint2D;
    
  
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        RopeAnimationController.hookArrived += ChangeStateToHooked;
        GrapplingController.unhooked += Unhook;
    }

    void Start()
    {
        currentState = PlayerState.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 2;
        layerMask = ~layerMask;
        if (rb.velocity.y < 0 && (currentState == PlayerState.Jumping || currentState == PlayerState.Neutral)) currentState = PlayerState.Falling;
        bool hitGround = Physics2D.Raycast(raycastStart.position, Vector2.down, 0.1f, layerMask);
        if(hitGround && currentState == PlayerState.Falling) currentState = PlayerState.Neutral;
        //if(currentState == PlayerState.Jumping)
        if (Input.GetKeyDown(KeyCode.Space) && currentState == PlayerState.Neutral)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            currentState = PlayerState.Jumping;
        }

        if (Input.GetKey(KeyCode.Mouse0) && currentState == PlayerState.Hooked)
        {
            _joint2D.distance = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _joint2D.distance = (_joint2D.connectedAnchor - (Vector2) transform.position).magnitude;
        }
        if (Input.GetKey(KeyCode.A )&& currentState != PlayerState.Hooked) rb.AddForce(Vector2.left * movementSpeed);
        if (Input.GetKey(KeyCode.D) &&currentState != PlayerState.Hooked) rb.AddForce(Vector2.right * movementSpeed);
        
    }

    private void ChangeStateToHooked()
    {
        currentState = PlayerState.Hooked;
        rb.mass = 0.3f;
    }

    private void Unhook()
    {
        rb.mass = 1;
        if (rb.velocity.magnitude == 0)
        {
            currentState = PlayerState.Neutral;    
        }else if (rb.velocity.y > 0)
        {
            currentState = PlayerState.Jumping;
        }else currentState = PlayerState.Falling;
    }
    public enum PlayerState
    {
        Jumping,
        Falling,
        Neutral,
        Hooked
    }
    
}
