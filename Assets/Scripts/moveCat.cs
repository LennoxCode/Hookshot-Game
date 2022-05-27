using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCat : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody2D rigitbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigitbody = this.GetComponent<Rigidbody2D>();
        rigitbody.velocity = new Vector2(speed, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
