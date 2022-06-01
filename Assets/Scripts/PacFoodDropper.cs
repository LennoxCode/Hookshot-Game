using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drops Food for Pac Man
/// </summary>
public class PacFoodDropper : MonoBehaviour
{
    public GameObject Container; // holds spawned PacFoods

    [SerializeField] public int dropCounter = 20; // how many shall be spawned?
    [SerializeField] private GameObject PacFood; // what shall be spawned?
    [SerializeField] private Collider2D thisCollider;
    [SerializeField] private float timeBetweenDrops = 0.5f;
    [SerializeField] private PacManScript PacMan; // Pac Man reference
    [SerializeField] private Sprite[] sprites; // for Animation 

    private SpriteRenderer spriteRenderer;

    private bool _collected = false;
    private Transform _playerTransform;
    private int originalDrops; // how many drops initally?

    private void Awake()
    {
        // Assign references
        originalDrops = dropCounter;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If bag has been touched by player -> Move bag alongside player and awake the PacMan
        if (collision.gameObject.tag.Equals("Player") && !_collected)
        {
            _playerTransform = collision.gameObject.transform;
            _collected = true;
            thisCollider.enabled = false;
            transform.SetParent(_playerTransform);

            PacMan.enabled = true;

            StartCoroutine(DropFood());
        }
    }

    /// <summary>
    /// Drops one Food Prefab in Container
    /// </summary>
    private void DropOne()
    {
        GameObject food = Instantiate(PacFood, _playerTransform);
        food.transform.SetParent(Container.transform);
        food.tag = "PacFood";

        food.transform.localScale = new Vector3(5, 5, 1); // Scale was off
    }

    /// <summary>
    /// Drops Food until counter reached 0
    /// </summary>
    /// <returns>?</returns>
    IEnumerator DropFood()
    {
        Container = Instantiate(new GameObject());

        while (dropCounter > 0)
        {
            spriteRenderer.sprite = ChangeSprite();
            DropOne();
            dropCounter--;
            if (dropCounter > 0) yield return new WaitForSeconds(timeBetweenDrops);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Animation for Bag.
    /// </summary>
    /// <returns>Returns Sprite depending on how many food drops are left</returns>
    private Sprite ChangeSprite()
    {
        float relativeAmount = (float)dropCounter / (float)originalDrops;

        if (relativeAmount < 0.2) return sprites[4];
        if (relativeAmount < 0.4) return sprites[3];
        if (relativeAmount < 0.6) return sprites[2];
        if (relativeAmount < 0.8) return sprites[1];
        return sprites[0];
    }
}