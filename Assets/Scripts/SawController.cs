using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    [SerializeField] private float timeBetweenPoints;
    [SerializeField] private float idleTime;

    [SerializeField] private GameObject saw;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    private float timer = 0f;
    private bool sawedUp;
    private SawState state = SawState.Idle;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SawState.Idle:
                timer += Time.deltaTime;

                if(timer >= idleTime)
                {
                    timer = 0f;
                    state = sawedUp ? SawState.SawingDown : SawState.SawingUp;
                }
                break;

            case SawState.SawingUp:
                timer += Time.deltaTime;

                if(timer < timeBetweenPoints)
                {
                    SetSawToPercent(timer / timeBetweenPoints);
                }
                else
                {
                    timer = 0f;
                    SetSawToPercent(1f);
                    sawedUp = true;
                    state = SawState.Idle;
                }
                break;

            case SawState.SawingDown:
                timer += Time.deltaTime;

                if (timer < timeBetweenPoints)
                {
                    SetSawToPercent(1f - timer / timeBetweenPoints);
                }
                else
                {
                    timer = 0f;
                    SetSawToPercent(0f);
                    sawedUp = false;
                    state = SawState.Idle;
                }
                break;
        }
    }

    // percent 0f to 1f
    private void SetSawToPercent(float percent)
    {
        Vector3 diffrence = pointB.transform.position - pointA.transform.position;
        saw.transform.position = pointA.transform.position + diffrence * percent;
    }
}


public enum SawState
{
    Idle,
    SawingUp,
    SawingDown
}
