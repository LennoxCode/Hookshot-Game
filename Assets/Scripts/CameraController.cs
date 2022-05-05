using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField]private Transform playerPos;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
        
    }
}
