using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacFoodDropper : MonoBehaviour
{
    public GameObject Container;

    [SerializeField] public int dropCounter = 20;
    [SerializeField] private GameObject PacFood;
    [SerializeField] private Collider2D thisCollider;
    [SerializeField] private float timeBetweenDrops = 0.5f;
    [SerializeField] private PacManScript PacMan;
    [SerializeField] private Sprite[] sprites;
       
    private SpriteRenderer spriteRenderer;

    private bool _collected = false;
    private Transform _playerTransform;
    private int originalDrops;

    private void Awake()
    {
        originalDrops = dropCounter;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") && !_collected)
        {
            _playerTransform = collision.gameObject.transform;
            _collected = true;
            thisCollider.enabled = false;
            transform.SetParent(_playerTransform);

            PacMan.enabled = true;

            StartCoroutine(DropFood());
        }
    }

    private void DropOne()
    {
        GameObject food = Instantiate(PacFood, _playerTransform);
        food.transform.SetParent(Container.transform);
        food.tag = "PacFood";

        food.transform.localScale = new Vector3(5, 5, 1);
    }

    IEnumerator DropFood()
    {
        Container = Instantiate(new GameObject());
        
        while(dropCounter > 0)
        {
            spriteRenderer.sprite = ChangeSprite();
            DropOne();
            dropCounter--;
            if(dropCounter>0) yield return new WaitForSeconds(timeBetweenDrops);
        }
        Destroy(gameObject);
    }

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
