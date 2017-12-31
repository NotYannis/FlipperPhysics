using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleSpawner : MonoBehaviour {
    private ObjectPooled[] marblePools;
    private GemCombo gemCombo;

    BoxCollider2D box;

    float minX = -50.0f;
    float maxX = 50.0f;

    public int marblesToSpawnCount;
    public float forceAtSpawn;
    public bool fever;

    private Coroutine comboFeverCoroutine;
    // Use this for initialization
    void Start () {
        marblePools = GameObject.Find("Marbles").GetComponentsInChildren<ObjectPooled>();
        gemCombo = GetComponent<GemCombo>();

        box = GetComponent<BoxCollider2D>();
	}

    private void Update()
    {
        minX = box.bounds.min.x;
        maxX = box.bounds.max.x;
    }

    public void SpawnMarbles()
    {
        marblesToSpawnCount = gemCombo.AddCombo();

        if(gemCombo.currentCombo < 5)
        {
            for (int i = 0; i < marblesToSpawnCount; i++)
            {
                SpawnMarble();
            }
            fever = false;
        }
        else
        {
            if (fever == false)
            {
                fever = true;
                comboFeverCoroutine = StartCoroutine(ComboFever());
            }
        }
    }

    IEnumerator ComboFever()
    {
        while (fever)
        {
            SpawnMarble();
            yield return new WaitForSeconds(Random.Range(0.08f, 0.2f));
        }

        yield return null;
    }

    private void SpawnMarble()
    {
        Vector3 randPosition = this.transform.position;
        Vector3 randForce = Vector3.zero;
        int randomPool = 0;

        randomPool = Random.Range(0, marblePools.Length);
        randPosition.x = Random.Range(minX, maxX);

        GameObject marbleInstance = marblePools[randomPool].GetPooledObject();
        marbleInstance.transform.position = randPosition;

        randForce = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-2.0f, -1.0f), 0.0f);
        randForce *= forceAtSpawn;
        marbleInstance.GetComponent<Rigidbody2D>().AddForce(randForce, ForceMode2D.Impulse);
    }

}
