using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] [Range(1, 10)] private float followSpeed;
    [SerializeField] private Vector2 followOffset;
    [Header("setup")]
    [SerializeField] private GameObject playerObject; 
    private Vector3 threshhold;
    private Camera mainCam;
    private Rigidbody2D playerRB;
    private Transform playerPos;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCam = GetComponent<Camera>();
        calculateThreshhold();
        playerPos = playerObject.transform;
        playerRB = playerObject.GetComponent<Rigidbody2D>();
    }
    
    private void LateUpdate()
    {
        Vector2 follow = playerPos.position;
        Vector3 currPos = transform.position;
        float xDifference = Vector2.Distance( Vector2.right *  currPos.x,
            Vector2.right * follow.x);
        float yDifference = Vector2.Distance( Vector2.up *  currPos.y,
            Vector2.up * follow.y);
        Vector3 targetPos = currPos;
        
        if (Mathf.Abs(xDifference) >= threshhold.x) targetPos.x = follow.x;
        if (Mathf.Abs(yDifference) >= threshhold.y) targetPos.y = follow.y;
        float playerSpeed = playerRB.velocity.magnitude;
        float adjFollowSpeed = playerSpeed > followSpeed ? playerSpeed : followSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, adjFollowSpeed * Time.deltaTime);

    }

    private void calculateThreshhold()
    {
       
        Rect aspect = mainCam.pixelRect;
        Vector2 camBounds = new Vector2(mainCam.orthographicSize * aspect.width / aspect.height, mainCam.orthographicSize);
        camBounds.x -= followOffset.x;
        camBounds.y -= followOffset.y;
        threshhold = camBounds;
    }

    private void OnDrawGizmosSelected()
    {
        mainCam = GetComponent<Camera>();
        calculateThreshhold();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(threshhold.x * 2, threshhold.y * 2));
    }
}
