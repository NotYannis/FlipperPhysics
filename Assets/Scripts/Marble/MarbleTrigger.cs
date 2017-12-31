using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MarbleTrigger : MonoBehaviour {
    SpriteRenderer triggerFeedback;

    // Use this for initialization
    void Start()
    {
        triggerFeedback = GameObject.Find("MarbleTrigger/TriggerFeedback").GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        coll.GetComponent<MoveMarble>().enabled = true;
        coll.GetComponent<MoveMarble>().trail.enabled = true;
        coll.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        coll.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15);

        StartCoroutine(FadeFeedback());
    }

    IEnumerator FadeFeedback()
    {
        float fadeTime = 0.1f;
        float cooldown = fadeTime;
        float alphaAmount = 0.5f;
        Color color = triggerFeedback.color;

        while (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            color.a = alphaAmount * (cooldown / fadeTime);
            triggerFeedback.color = color;

            yield return null;
        }

        yield return null;
    }
}
