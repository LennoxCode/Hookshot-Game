using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggoController : MonoBehaviour
{
    [SerializeField] private float idleTime;
    [SerializeField] private float chargeTime;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] sprites;

    private FrogState state = FrogState.Charging;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case FrogState.Idle:
                timer += Time.deltaTime;
                if (timer >= idleTime)
                {
                    timer = 0f;
                    state = FrogState.Charging;
                    RefreshSprite();
                }
                break;

            case FrogState.Charging:
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
        if(state == FrogState.Jumping)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state != FrogState.Jumping) return;
        if (collision.gameObject.CompareTag("Player")) return;

        transform.up = -transform.TransformDirection(Vector3.up);
        state = FrogState.Idle;
        RefreshSprite();
    }

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
