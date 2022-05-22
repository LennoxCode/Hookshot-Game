using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] instructions;
    private int instructionsIndex;


    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i< instructions.Length; i++)
        {
            if(i == instructionsIndex)
            {
                instructions[instructionsIndex].SetActive(true);
            }
            else 
            {
                instructions[instructionsIndex].SetActive(false);
                }
            }
        if(instructionsIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                instructionsIndex++;// MouseKlick
        }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                instructionsIndex++;// MouseKlick
            }
            else if (instructionsIndex == 1)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1)) {
                    instructionsIndex++;
                }
            }

        }
        
    }
}
