using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManScript : MonoBehaviour
{
    GameObject Container;
    [SerializeField] private PacFoodDropper dropper;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 0.3f;

    private int _maxFood;
    private int _counter = 0;
    private GameObject _next;
    private bool started = false;
    private float startUpTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Container = dropper.Container;
        _maxFood = dropper.dropCounter;
        animator.enabled = true;
    }

    private void FixedUpdate()
    {
        if(!started) {
            GetNextFood();
            transform.rotation = Quaternion.Lerp(transform.rotation, getRotation(), Time.deltaTime);

            startUpTimer += Time.deltaTime;

            if (startUpTimer > 1.5f) started = true;
        } else if(_next && started)
        {
            transform.rotation = getRotation();
            
            transform.position += (transform.right) * speed;
        } else
        {
            GetNextFood();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag.Equals("PacFood")) Destroy(collision.gameObject);

    }

    private Quaternion getRotation()
    {
        Vector3 vectorToTarget = _next.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion newQuaternion = Quaternion.AngleAxis(angle, Vector3.forward);

        if (newQuaternion.eulerAngles.z > 90 && newQuaternion.eulerAngles.z < 280)
        {
            newQuaternion.eulerAngles = new Vector3(0, 180, 180 - newQuaternion.eulerAngles.z);
        }

        return  newQuaternion;
    }

    private void GetNextFood()
    {
        if (_counter < _maxFood)
        {
            try
            {
                _next = Container.transform.GetChild(0).gameObject;
            }
            catch (UnityException)
            {
                Debug.Log("Waiting for food");
            }
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        animator.enabled = false;
        this.enabled = false;
    }
}
