using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    private AudioSource spirit_source;

    //public AudioClip spirit_clip = (AudioClip)Resources.Load("Audio/SFX/enemy_death");
    

    // Use this for initialization
    void Awake () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spirit_Audio()
    {
        //enemy_death.clip = clip1;
        GetComponent<AudioSource>().Play();
       
    }


}
