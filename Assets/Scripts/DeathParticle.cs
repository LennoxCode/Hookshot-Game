using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// sits on game object with particle system and plays on player death action
/// </summary>
public class DeathParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    void Start()
    {
        CheckPointManager.playerDeath += Play;
    }

    private void Play()
    {
        ps.Play();
    }
}
