using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggoController : MonoBehaviour
{
    // serialized values
    [SerializeField] private float idleTime;
    [SerializeField] private float chargeTime;
    [SerializeField] private float speed;

    // references
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] sprites;

    // global variables
    private FrogState state = FrogState.Charging;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case FrogState.Idle:

                // count up timer and set state to charging, set sprite to charging
                timer += Time.deltaTime;
                if (timer >= idleTime)
                {
                    timer = 0f;
                    state = FrogState.Charging;
                    RefreshSprite();
                }
                break;

            case FrogState.Charging:

                // count up timer and set state to jumping, set sprite to jumping
                timer += Time.deltaTime;
                if (timer >= chargeTime)
                {
                    timer = 0f;
                    state = FrogState.Jumping;
                    RefreshSprite();
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        // move frog upwards if state is jumping
        if(state == FrogState.Jumping)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // return if frog isn't jumping or colliding object is the player
        if (state != FrogState.Jumping) return;
        if (collision.gameObject.CompareTag("Player")) return;

        // turn upside down, set state to idle, set sprite to idle
        transform.up = -transform.TransformDirection(Vector3.up);
        state = FrogState.Idle;
        RefreshSprite();
    }

    /// <summary>
    /// sets sprite according to current state
    /// </summary>
    private void RefreshSprite()
    {
        sr.sprite = sprites[(int) state];
    }
}

enum FrogState
{
    Idle,
    Charging,
    Jumping
}
