using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCat : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody2D rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(speed, 0);

    }
}
