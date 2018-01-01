using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour {
    ScoreUI scoreUI;
    ObjectPooled scoreParticles;
    AudioSource scoreSound;
    float pitch = 1.0f;
    public float maxPitch = 3.0f;
    public float pitchOffset = 0.05f;
    public float pitchResetCooldown = 0.2f;

    Coroutine changePitchCoroutine;

	// Use this for initialization
	void Start () {
        scoreUI = GameObject.Find("ScoreUI").GetComponent<ScoreUI>();
        scoreParticles = GameObject.Find("ScoreParticlesPool").GetComponent<ObjectPooled>();
        scoreSound = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scoreUI.AddScore(1);

        Vector2 force = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        GameObject scoreParticleInstance = scoreParticles.GetPooledObject();
        scoreParticleInstance.transform.position = collision.gameObject.transform.position;

        scoreParticleInstance.GetComponent<ScoreParticleScript>().PlayParticles(force);

        PlayScoreSound();

        collision.gameObject.SetActive(false);
    }

    void PlayScoreSound()
    {
        scoreSound.Play();
        pitch *= 1.05946f;
        scoreSound.pitch = pitch;
        if(changePitchCoroutine != null)
        {
            StopCoroutine(changePitchCoroutine);
        }
        changePitchCoroutine = StartCoroutine(ChangePitch());
    }

    IEnumerator ChangePitch()
    {
        float cooldown = pitchResetCooldown;
        while(cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }
        pitch = 1.0f;

        yield return null;
    }
}
