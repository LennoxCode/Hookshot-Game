using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePosition.z = 0;

        transform.up = mousePosition;
            
    }
}
