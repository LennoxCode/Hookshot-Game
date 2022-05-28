using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;


    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(transform.position.x+0.01f, transform.position.y, transform.position.z);
    }
}
