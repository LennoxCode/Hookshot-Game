using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // serialized values
    [SerializeField] private float canonForce;
    [SerializeField] private float maxDistance;
    [SerializeField] private float coolDown;

    // references
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ballPrefab;

    // global variables
    private TurretState state = TurretState.Charging;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        RotateCanonTo(player.transform.position);

        switch (state)
        {
            case TurretState.Charging:

                // count up timer and then set turret to charged
                timer += Time.deltaTime;
                if(timer >= coolDown)
                {
                    timer = 0f;
                    state = TurretState.Charged;
                }
                break;
            case TurretState.Charged:
                // try to shoot, set state back to charging if successful
                if (TryShoot()) state = TurretState.Charging;
                break;
        }
    }

    /// <summary>
    /// calculates distance to player and shoots if they are close enough
    /// </summary>
    /// <returns>boolean: if the turret has shot</returns>
    private bool TryShoot()
    {
        // calculate distance
        Vector2 diffrenceVector = (player.transform.position - barrel.transform.position);
        if (diffrenceVector.magnitude > maxDistance) return false;

        // instantiate canonball and shoot
        GameObject ball = Instantiate(ballPrefab, barrel.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(diffrenceVector.normalized * canonForce);

        return true;
    }

    /// <summary>
    /// rotates the turret towards a given target point
    /// </summary>
    /// <param name="target">Target Position</param>
    private void RotateCanonTo(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion newQuaternion = Quaternion.AngleAxis(angle, Vector3.forward);

        // flip sprite if it would be upside down, adjust angles
        if (newQuaternion.eulerAngles.z > 90 && newQuaternion.eulerAngles.z < 280)
        {
            newQuaternion.eulerAngles = new Vector3(0, 180, 180 - newQuaternion.eulerAngles.z);
        }

        transform.rotation = newQuaternion;
    }
}

public enum TurretState
{
    Charging,
    Charged
}
