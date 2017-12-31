using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBright : MonoBehaviour {
    private SpriteRenderer spr;
    public bool flash;

    public float alphaOffset;
    private float maxAlpha;
    public float alphaChangeTime = 2.0f;
    public float targetAlpha;
    public float smoothAlpha = 0.1f;

    Coroutine moreAlphaCoroutine;
    Coroutine lessAlphaCoroutine;
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
        maxAlpha = alphaOffset * (float)combo;
        alphaChangeTime = 2.0f - 0.2f * combo;

        if(combo == 0)
        {
            StartCoroutine(ZeroAlpha());
        }
        else
        {
            if (lessAlphaCoroutine != null)
            {
                StopCoroutine(lessAlphaCoroutine);
            }
            if (moreAlphaCoroutine != null)
            {
                StopCoroutine(moreAlphaCoroutine);
            }

            if (flash)
            {
                smoothAlpha = 1.0f;
                StartCoroutine(Flash());
            }
            else
            {
                StartCoroutine(StartChange());
            }
        }
    }

	IEnumerator StartChange()
    {
        float time = Random.Range(0.0f, alphaChangeTime);

        yield return new WaitForSeconds(time);

        moreAlphaCoroutine = StartCoroutine(MoreAlpha());

        yield return null;
    }

    //Add smoothly alpha to reach maxAlpha, and then call LessAlpha
    IEnumerator MoreAlpha()
    {
        float cooldown = 0.0f;

        while (cooldown < alphaChangeTime)
        {
            cooldown += Time.deltaTime;
            float percent = cooldown / alphaChangeTime;
            targetAlpha = maxAlpha * percent;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(Random.Range(alphaChangeTime / 4.0f, alphaChangeTime / 2.0f));

        lessAlphaCoroutine = StartCoroutine(LessAlpha());

        yield return null;
    }

    //Remove smoothly alpha to reach 0, and then call MoreAlpha
    IEnumerator LessAlpha()
    {
        float cooldown = alphaChangeTime;
        float percent;

        while (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            percent = cooldown / alphaChangeTime;
            targetAlpha = maxAlpha * percent;
            //spr.color = spriteColor;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(Random.Range(alphaChangeTime / 4.0f, alphaChangeTime / 2.0f));
        
        moreAlphaCoroutine = StartCoroutine(MoreAlpha());

        yield return null;
    }

    //Smoothly set alpha to zero
    public IEnumerator ZeroAlpha()
    {
        Color spriteColor = spr.color;
        float cooldown = 0.5f;
        float percent;
        smoothAlpha = 0.1f;

        while (cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            percent = cooldown / alphaChangeTime;
            targetAlpha = maxAlpha * percent;

            yield return null;
        }

        yield return null;
    }

    //Make a little flash using alpha
    IEnumerator Flash()
    {
        float cooldown = 0.0f;
        float addAlpha = maxAlpha;
        float flashTime = 0.1f;
        float percent;
        float targetAlphaOffset;
         
        while (cooldown < 0.1f)
        {
            cooldown += Time.deltaTime;
            percent = cooldown / flashTime;
            targetAlphaOffset = addAlpha * percent;
            targetAlpha += targetAlphaOffset;
            yield return null;
        }

        while(cooldown >= 0.0f)
        {
            percent = cooldown / flashTime;
            targetAlphaOffset = addAlpha * percent;
            targetAlpha -= targetAlphaOffset;
            cooldown -= Time.deltaTime;

            yield return null;
        }

        smoothAlpha = maxAlpha;
        StartCoroutine(StartChange());

        yield return null;
    }
}
