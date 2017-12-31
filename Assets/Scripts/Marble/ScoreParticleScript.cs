using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreParticleScript : MonoBehaviour {
    ParticleSystem particles;
    ParticleSystem.VelocityOverLifetimeModule particlesVel;

    // Use this for initialization
    void Start () {
        particles = GetComponent<ParticleSystem>();
        particlesVel = particles.velocityOverLifetime;
	}
	
    public void PlayParticles(Vector2 force)
    {
        StartCoroutine(PlayParticleCoroutine(force));
    }

    IEnumerator PlayParticleCoroutine(Vector2 force)
    {
        yield return new WaitForEndOfFrame();

        float mag = force.sqrMagnitude / 1000.0f;
        force.Normalize();
        force.x += Random.Range(-0.5f, 0.5f);
        force.y += Random.Range(0.1f, 0.5f);

        particlesVel.x = -force.x * mag;
        particlesVel.y = -force.y * mag;

        particles.Play();

        yield return null;
    }
}
