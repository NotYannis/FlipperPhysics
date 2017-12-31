using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GemCombo : MonoBehaviour {
    private GameObject marbleSpawner;
    private GameObject backgroundLayer;
    private Text comboTextFeedback;

    public int marblesPerCombo = 2;
    public float comboCooldown;
    public int currentCombo = 0;
    public int comboMax = 5;

    Coroutine comboCooldownCoroutine;

    // Use this for initialization
    void Start () {
        marbleSpawner = GameObject.Find("MarbleSpawner");
        marbleSpawner.transform.localScale = new Vector3(0.0f, marbleSpawner.transform.localScale.y, 1.0f);

        comboTextFeedback = GameObject.Find("ComboUI").GetComponentInChildren<Text>();
    }

    IEnumerator ComboCooldown()
    {
        float currentComboCooldown = comboCooldown;
        Vector3 marbleSpawnerScale = marbleSpawner.transform.localScale;

        float percent;

        while (currentComboCooldown > 0.0f)
        {
            currentComboCooldown -= Time.deltaTime;
            percent = currentComboCooldown / comboCooldown;

            marbleSpawnerScale.x = Mathf.Lerp(marbleSpawner.transform.localScale.x, currentCombo * percent, 0.1f);

            marbleSpawner.transform.localScale = marbleSpawnerScale;

            yield return new WaitForEndOfFrame();
        }
        marbleSpawnerScale.x = 0.0f;
        marbleSpawner.transform.localScale = marbleSpawnerScale;
        currentCombo = 0;
    }

    IEnumerator ComboTextScale()
    {
        Vector2 currentScale = comboTextFeedback.transform.localScale;
        comboTextFeedback.text = "Gems  x " + currentCombo * marblesPerCombo;
        comboTextFeedback.fontSize = 22 + currentCombo * 8;

        while (currentScale != Vector2.one)
        {
            currentScale = Vector2.MoveTowards(currentScale, Vector2.one, 0.1f);
            comboTextFeedback.transform.localScale = currentScale;

            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        while (currentScale != Vector2.zero)
        {
            currentScale = Vector2.MoveTowards(currentScale, Vector2.zero, 0.1f);
            comboTextFeedback.transform.localScale = currentScale;

            yield return null;
        }

        yield return null;
    }



    public int AddCombo()
    {
        currentCombo++;
        currentCombo = Mathf.Clamp(currentCombo, 0, comboMax);

        if (comboCooldownCoroutine != null)
        {
            StopCoroutine(comboCooldownCoroutine);
        }

        int marblesToSpawnCount = currentCombo * marblesPerCombo;
        marblesToSpawnCount = Mathf.Clamp(marblesToSpawnCount, 0, 10);

        StartCoroutine(ComboTextScale());
        comboCooldownCoroutine = StartCoroutine(ComboCooldown());

        return marblesToSpawnCount;
    }
}
