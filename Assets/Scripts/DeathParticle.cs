using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        CheckPointManager.playerDeath += Play;
    }

    private void Play()
    {
        ps.Play();
    }
}
