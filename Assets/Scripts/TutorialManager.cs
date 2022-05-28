using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] instructions;
    private int instructionsIndex;
    float waitTime = 5.0f;
    private float timeStamp = Mathf.Infinity;

    
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < instructions.Length; i++)
        {
            if (i == instructionsIndex)
            {
                instructions[instructionsIndex].SetActive(true);
                //Debug.Log(instructionsIndex);
            }
            else
            {
                instructions[instructionsIndex].SetActive(false);
            }
        }

        if (instructionsIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            { 
                instructionsIndex++;
                instructionsIndex++;
                Debug.Log("MouseKlick");
        }
            else if (instructionsIndex == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("MouseHold");
                    timeStamp = Time.time + waitTime;
                    if (Time.time >= timeStamp)
                    {
                        Debug.Log("Mouse TIME");
                        instructionsIndex++;
                    }
                }
            }
            else if (instructionsIndex == 2)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Debug.Log("Mouse Right");
                    instructionsIndex++;
                }
            }
            else if (instructionsIndex == 3)
            {
                if (Input.GetKeyDown(KeyCode.A & KeyCode.B))
                {
                    instructionsIndex++;
                }
            }
        }
    }
}
