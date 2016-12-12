using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource audio_source;
    public AudioClip au_spirit_pickup;
    //public AudioClip au_enemy_death;


    // Use this for initialization
    void Awake () {

        audio_source = gameObject.AddComponent<AudioSource>();
        audio_source.clip = au_spirit_pickup;
                
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown("q")) { audio_source.clip = au_spirit_pickup; }
        //if (Input.GetKeyDown("w")) { audio_source.clip = au_enemy_death; }
    }

    public void Spirit_Audio()
    {
        //enemy_death.clip = clip1;
        audio_source.clip = au_spirit_pickup;
        GetComponent<AudioSource>().Play();
       
    }

    /*
    public void Enemy_Death()
    {
        audio_source.clip = au_enemy_death;
        GetComponent<AudioSource>().Play();
    }
    */

}
