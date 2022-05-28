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
}
