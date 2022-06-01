using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Pac Man
/// </summary>
public class PacManScript : MonoBehaviour
{
    GameObject Container; // holds spawned PacFoods
    [SerializeField] private PacFoodDropper dropper; // The Dropper Script
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 0.3f; // Movement Speed

    private int _maxFood; // max Number of Food
    private int _counter = 0; // how many has been eaten
    private GameObject _next; // next Food
    private bool started = false; // bag collected?
    private float startUpTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Assign references and start animation
        Container = dropper.Container;
        _maxFood = dropper.dropCounter;
        animator.enabled = true;
    }

    private void FixedUpdate()
    {
        // if bag has been collected -> Wait 1.5 seconds and rotate towards first food
        if (!started)
        {
            GetNextFood();
            transform.rotation = Quaternion.Lerp(transform.rotation, getRotation(), Time.deltaTime);

            startUpTimer += Time.deltaTime;

            if (startUpTimer > 1.5f) started = true;
        }
        else if (_next && started) // if there is a next food
        {
            transform.rotation = getRotation();

            transform.position += (transform.right) * speed; // move towards it
        }
        else
        {
            GetNextFood(); // find next food
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // eat food
        if (collision.tag.Equals("PacFood")) Destroy(collision.gameObject);

    }

    /// <summary>
    /// Determins the rotation to the next Food
    /// </summary>
    /// <returns>Returns Rotation as Quaternion</returns>
    private Quaternion getRotation()
    {
        Vector3 vectorToTarget = _next.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion newQuaternion = Quaternion.AngleAxis(angle, Vector3.forward);

        if (newQuaternion.eulerAngles.z > 90 && newQuaternion.eulerAngles.z < 280)
        {
            newQuaternion.eulerAngles = new Vector3(0, 180, 180 - newQuaternion.eulerAngles.z);
        }

        return newQuaternion;
    }

    /// <summary>
    /// Assign next Food Pearl to field _next
    /// </summary>
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
            Die(); // if no pearl left -> die
        }
    }

    /// <summary>
    /// Stops animation and kill the script
    /// </summary>
    private void Die()
    {
        animator.enabled = false;
        this.enabled = false;
    }
}