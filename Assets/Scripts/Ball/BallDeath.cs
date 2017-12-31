using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeath : MonoBehaviour {
    EndMenu endMenu;
    GameObject movingPlatform;
    GameObject marbleTrigger;
    GameObject scoreTrigger;


    // Use this for initialization
    void Start () {
        endMenu = GameObject.Find("EndUI").GetComponent<EndMenu>();
        endMenu.gameObject.SetActive(false);

        movingPlatform = GameObject.Find("MovingPlatform");
        marbleTrigger = GameObject.Find("MarbleTriggers/MarbleTrigger");
        scoreTrigger = GameObject.Find("MarbleTriggers/ScoreTrigger");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        endMenu.gameObject.SetActive(true);
        movingPlatform.GetComponent<PlatformController>().enabled = false;
        marbleTrigger.GetComponent<BoxCollider2D>().enabled = false;
        scoreTrigger.GetComponent<BoxCollider2D>().enabled = false;
    }
}
