using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveInsta : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] float speed;
    private Vector2 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;   
    }

    void FixedUpdate()
    {
        Vector2 v = startingPosition;
        v.y += distance * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
