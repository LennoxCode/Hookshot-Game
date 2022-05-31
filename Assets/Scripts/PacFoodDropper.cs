using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacFoodDropper : MonoBehaviour
{
    public GameObject Container;

    [SerializeField] public int dropCounter = 5;
    [SerializeField] private GameObject PacFood;
    [SerializeField] private Collider2D thisCollider;
    [SerializeField] private float timeBetweenDrops = 3f;
    [SerializeField] private PacManScript PacMan;

    private bool _collected = false;
    private Transform _playerTransform;

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
            DropOne();
            dropCounter--;
            if(dropCounter>0) yield return new WaitForSeconds(timeBetweenDrops);
        }
        Destroy(gameObject);
    }

}
