using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMarble : MonoBehaviour {
    Vector3 target;
    Rigidbody2D rig;
    public TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        target = GameObject.Find("ScoreTrigger").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        MoveToTarget();
	}

    void MoveToTarget()
    {
        Vector2 force = target - transform.position;
        float distance = force.magnitude;
        force.Normalize();
        float strength = 10000 / (distance * distance);
        strength = Mathf.Clamp(strength, 0.5f, 2.0f);

        force *= strength;

        rig.AddForce(force);
    }

    private void OnDisable()
    {
        enabled = false;
        trail.enabled = false;
    }
}
