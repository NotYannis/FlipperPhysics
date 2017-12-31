using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour {
    Rigidbody2D rig;
    public float force;
    float xCenter;
    public bool canRotate = true;
    float yRotation = 0.0f;
    Text debug;

    // Use this for initialization
    void Start () {
        rig = GetComponent<Rigidbody2D>();
        xCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0f, 0f)).x;
        debug = GameObject.Find("Debug").GetComponent<Text>();
        StartCoroutine(ResetPosition());
	}
	
	// Update is called once per frame
	void Update () {
        RotatePlatform();
	}


    void RotatePlatform()
    {
        
        if (canRotate)
        {
#if !UNITY_EDITOR
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0){
                float xTouchPosition = Input.GetTouch(0).position.x;
                if (xTouchPosition < xCenter)
                {
                    rig.AddTorque(-force, ForceMode2D.Impulse);
                }
                else if(xTouchPosition > xCenter)
                {
                    rig.AddTorque(force, ForceMode2D.Impulse);
                }
            }
#endif
#endif

#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButton(0))
            {
                rig.AddTorque(-force, ForceMode2D.Impulse);
            }
            if (Input.GetMouseButton(1))
            {
                rig.AddTorque(force, ForceMode2D.Impulse);
            }
#endif
        }

    }

    IEnumerator ResetPosition()
    {
        Vector3 basePosition = this.transform.position;

        while (true)
        {
            transform.position = basePosition;

            yield return new WaitForSeconds(5.0f);
        }
    }
}
