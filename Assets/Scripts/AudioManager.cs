using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;


	// Use this for initialization
	void Start () {
		if(Instance != null)
        {
            print("Error : An instance of AudioManager already exists !");
            return;
        }
        Instance = this;
	}
	


    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, this.transform.position);
    }
}
