using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    int score = 0;
    Text scoreText;

    Vector3 baseScale;
    Coroutine scaleCoroutine;

	// Use this for initialization
	void Start () {
        scoreText = GetComponentInChildren<Text>();
        baseScale = scoreText.transform.localScale;
	}

    public void AddScore(int scoreNum)
    {
        score += scoreNum;
        scoreText.text = score.ToString();

        scoreText.transform.localScale = baseScale;
        StopCoroutine(ScoreScale());
        StartCoroutine(ScoreScale());
    }

    IEnumerator ScoreScale()
    {
        float scaleTime = 0.1f;
        float currentTime = 0.0f;
        float scaleOffset = 0.7f;

        Vector2 textScale = scoreText.transform.localScale;

        while(currentTime < scaleTime)
        {
            currentTime += Time.deltaTime;
            textScale.x = 1 + (scaleOffset * (currentTime / scaleTime));
            textScale.y = 1 + (scaleOffset * (currentTime / scaleTime));
            scoreText.transform.localScale = textScale;

            yield return null;
        }

        while (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;

            textScale.x = 1 + (scaleOffset * (currentTime / scaleTime));
            textScale.y = 1 + (scaleOffset * (currentTime / scaleTime));
            scoreText.transform.localScale = textScale;

            yield return null;
        }

        yield return null;
    }
}
