using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBright : MonoBehaviour {
    private SpriteRenderer spr;

    public float maxAlpha;
    public float alphaChangeTime = 2.0f;
    public float targetAlpha;
    public float smoothAlpha = 0.1f;

    Coroutine moreAlpha;
    Coroutine lessAlpha;
	// Use this for initialization
	void Awake () {
        spr = GetComponent<SpriteRenderer>();
        targetAlpha = spr.color.a;
	}

    private void Update()
    {
        spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, Mathf.Lerp(spr.color.a, targetAlpha, smoothAlpha));
    }

    public void UpdateAlpha(int combo, int maxCombo)
    {
        maxAlpha = (float)combo / (float)maxCombo;
        smoothAlpha = (float)combo / (float)maxCombo * 2.0f + 0.1f;
        alphaChangeTime = 2.0f - 0.2f * combo;

        if(combo == 0)
        {
            if(lessAlpha != null)
            {
                StopCoroutine(lessAlpha);
            }
            if(moreAlpha != null)
            {
                StopCoroutine(moreAlpha);
            }
            StartCoroutine(ZeroAlpha());
        }
        else if(combo == 1)
        {
            StartCoroutine(StartChange());
        }
    }



	IEnumerator StartChange()
    {
        float time = Random.Range(0.0f, 2.0f);

        yield return new WaitForSeconds(time);

        moreAlpha = StartCoroutine(MoreAlpha());

        yield return null;
    }

    IEnumerator MoreAlpha()
    {
        Color spriteColor = spr.color;
        float cooldown = 0.0f;

        while (cooldown < alphaChangeTime)
        {
            cooldown += Time.deltaTime;
            float percent = cooldown / alphaChangeTime;
            targetAlpha = maxAlpha * percent;
            //spr.color = spriteColor;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(Random.Range(alphaChangeTime / 4.0f, alphaChangeTime / 2.0f));

        lessAlpha = StartCoroutine(LessAlpha());

        yield return null;
    }

    IEnumerator LessAlpha()
    {
        Color spriteColor = spr.color;
        float cooldown = alphaChangeTime;

        while (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            float percent = cooldown / alphaChangeTime;
            targetAlpha = maxAlpha * percent;
            //spr.color = spriteColor;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(Random.Range(alphaChangeTime / 4.0f, alphaChangeTime / 2.0f));
        
        moreAlpha = StartCoroutine(MoreAlpha());

        yield return null;
    }

    public IEnumerator ZeroAlpha()
    {
        Color spriteColor = spr.color;
        float cooldown = 0.5f;

        while (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            float percent = cooldown / alphaChangeTime;
            targetAlpha = maxAlpha * percent;

            yield return null;
        }

        yield return null;
    }
}
