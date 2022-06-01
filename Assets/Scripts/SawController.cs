using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    // serialized values
    [SerializeField] private float timeBetweenPoints;
    [SerializeField] private float idleTime;

    // references
    [SerializeField] private GameObject saw;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    // global variables
    private float timer = 0f;
    private bool sawedUp = false;
    private SawState state = SawState.Idle;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SawState.Idle:

                // count up timer then set state to sawing up or sawing down respectively
                timer += Time.deltaTime;
                if(timer >= idleTime)
                {
                    timer = 0f;
                    state = sawedUp ? SawState.SawingDown : SawState.SawingUp;
                }
                break;

            case SawState.SawingUp:

                // count up timer
                timer += Time.deltaTime;
                if(timer < timeBetweenPoints)
                {
                    // percentage calculated by time divided by the total distance between the two points
                    SetSawToPercent(timer / timeBetweenPoints);
                }
                else
                {
                    // when timer set state back to idle
                    timer = 0f;
                    SetSawToPercent(1f);
                    sawedUp = true;
                    state = SawState.Idle;
                }
                break;

            case SawState.SawingDown:

                // count up timer
                timer += Time.deltaTime;
                if (timer < timeBetweenPoints)
                {
                    // percentage calculated by inverse time divided by the total distance between the two points
                    SetSawToPercent(1f - timer / timeBetweenPoints);
                }
                else
                {
                    // when timer set state back to idle
                    timer = 0f;
                    SetSawToPercent(0f);
                    sawedUp = false;
                    state = SawState.Idle;
                }
                break;
        }
    }

    /// <summary>
    /// sets saw position to given percent between points
    /// </summary>
    /// <param name="percent">Percentage between 0f and 1f</param>
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
