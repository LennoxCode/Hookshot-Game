using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float canonForce;
    [SerializeField] private float maxDistance;
    [SerializeField] private float coolDown;
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ballPrefab;

    private TurretState state = TurretState.Charging;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        RotateCanonTo(player.transform.position);

        switch (state)
        {
            case TurretState.Charging:
                timer += Time.deltaTime;

                if(timer >= coolDown)
                {
                    timer = 0f;
                    state = TurretState.Charged;
                }
                break;
            case TurretState.Charged:
                if (TryShoot()) state = TurretState.Charging;
                break;
        }
    }

    private bool TryShoot()
    {
        Vector2 diffrenceVector = (player.transform.position - barrel.transform.position);
        if (diffrenceVector.magnitude > maxDistance) return false;

        GameObject ball = Instantiate(ballPrefab, barrel.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().AddForce(diffrenceVector.normalized * canonForce);

        return true;
    }

    private void RotateCanonTo(Vector3 target)
    {
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion newQuaternion = Quaternion.AngleAxis(angle, Vector3.forward);

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
