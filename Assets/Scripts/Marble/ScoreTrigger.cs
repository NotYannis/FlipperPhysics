using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour {
    ScoreUI scoreUI;
    ObjectPooled scoreParticles;

	// Use this for initialization
	void Start () {
        scoreUI = GameObject.Find("ScoreUI").GetComponent<ScoreUI>();
        scoreParticles = GameObject.Find("ScoreParticlesPool").GetComponent<ObjectPooled>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scoreUI.AddScore(1);

        Vector2 force = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        GameObject scoreParticleInstance = scoreParticles.GetPooledObject();
        scoreParticleInstance.transform.position = collision.gameObject.transform.position;

        scoreParticleInstance.GetComponent<ScoreParticleScript>().PlayParticles(force);

        collision.gameObject.SetActive(false);
    }
}
