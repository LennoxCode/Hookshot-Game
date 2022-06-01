using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// this Script controls the movement of the Camera by chasing the player and is inspired by this tutorial:
/// https://www.youtube.com/watch?v=GTxiCzvYNOc
/// this works by setting the offset vector in the editor which represents a Rectangle.
/// If the player leaves this rectangle the camera follows the player with the speed set in the editor
/// or if the velocity on the rigidbody is greater follows matches this speed this camera design
/// is commonly found in old platformers because it leads to smoother camera transitions, and
/// the camera does not clip outside of the level as easily as with a simple follow 
/// </summary>
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
        CheckPointManager.playerRespawn += MoveCameraToPlayer;
    }

    //TODO: reset the camera to the player pos upon restarting or resetting to a checkpoint
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

    private void OnDestroy()
    {
        CheckPointManager.playerRespawn -= MoveCameraToPlayer;
    }

    private void MoveCameraToPlayer()
    {  
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);        
    }
}
