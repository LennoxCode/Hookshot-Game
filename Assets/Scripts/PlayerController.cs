
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public PlayerState currentState;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform raycastStart;
    [SerializeField] private SpringJoint2D _joint2D;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GrapplingController.hookHit += ChangeStateToHooked;
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
            Vector2 direction = (_joint2D.connectedAnchor - (Vector2)transform.position).normalized;
            rb.AddForce(direction * 3);
        }
        if (Input.GetKey(KeyCode.A)) rb.AddForce(Vector2.left * movementSpeed);
        if (Input.GetKey(KeyCode.D)) rb.AddForce(Vector2.right * movementSpeed);
        
    }

    private void ChangeStateToHooked()
    {
        currentState = PlayerState.Hooked;
    }

    private void Unhook()
    {
        currentState = PlayerState.Jumping;
    }
    public enum PlayerState
    {
        Jumping,
        Falling,
        Neutral,
        Hooked
    }
    
}
