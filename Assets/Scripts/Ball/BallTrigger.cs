using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour {
    MarbleSpawner marbleSpawner;
    SpriteRenderer triggerFeedback;

	// Use this for initialization
	void Start () {
        marbleSpawner = GameObject.Find("MarbleSpawner").GetComponent<MarbleSpawner>();
        triggerFeedback = GameObject.Find("BallTrigger/TriggerFeedback").GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y >= 0.0f)
        {
            marbleSpawner.SpawnMarbles();
            StartCoroutine(FadeFeedback());
        }
    }

    IEnumerator FadeFeedback()
    {
        float fadeTime = 0.3f;
        float cooldown = fadeTime;
        float alphaAmount = 0.5f;
        Color color = triggerFeedback.color;

        while(cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            color.a = alphaAmount * (cooldown / fadeTime);
            triggerFeedback.color = color;

            yield return null;
        }

        yield return null;
    }
}
