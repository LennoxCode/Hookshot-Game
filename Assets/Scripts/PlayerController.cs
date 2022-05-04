
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private PlayerState currentState;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (rb.velocity.y < 0 && currentState == PlayerState.Jumping) currentState = PlayerState.Falling;
        bool hitGround = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, layerMask);
        if(hitGround && currentState == PlayerState.Falling) currentState = PlayerState.Neutral;
        //if(currentState == PlayerState.Jumping)
        if (Input.GetKeyDown(KeyCode.Space) && currentState != PlayerState.Jumping)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            currentState = PlayerState.Jumping;
        }

        if (Input.GetKey(KeyCode.A)) rb.AddForce(Vector2.left * movementSpeed);
        if (Input.GetKey(KeyCode.D)) rb.AddForce(Vector2.right * movementSpeed);
        
    }
    public enum PlayerState
    {
        Jumping,
        Falling,
        Neutral
    }
    
}
