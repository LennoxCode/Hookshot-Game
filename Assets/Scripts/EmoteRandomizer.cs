using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteRandomizer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] emotes;

    void Start()
    {
        spriteRenderer.sprite = emotes[Random.Range(0, emotes.Length)];
    }
}
