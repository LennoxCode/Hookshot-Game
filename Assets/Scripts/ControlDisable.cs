using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDisable : MonoBehaviour
{
    [SerializeField] private GrapplingController grapController;

    // Start is called before the first frame update
    void Start()
    {
        CheckPointManager.playerDeath += DisableControls;
        CheckPointManager.playerRespawn += EnableControls;
        GameStateMachine.instance.gamePaused += DisableControls;
        GameStateMachine.instance.gameResumed += EnableControls;
    }

    public void DisableControls()
    {
        grapController.Unhook();
        grapController.enabled = false;
    }

    public void EnableControls()
    {
        grapController.enabled = true;
    }

    private void OnDestroy()
    {
        CheckPointManager.playerDeath -= DisableControls;
        CheckPointManager.playerRespawn -= EnableControls;
        GameStateMachine.instance.gamePaused -= DisableControls;
        GameStateMachine.instance.gameResumed -= EnableControls;
    }
}
