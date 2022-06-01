using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// chooses random sprite from array on start and sets it to sprite of this game object
/// </summary>
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
