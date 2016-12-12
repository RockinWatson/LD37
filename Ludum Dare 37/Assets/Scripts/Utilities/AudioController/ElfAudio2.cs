using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfAudio2 : MonoBehaviour {

    public AudioSource audio_source;
    public AudioClip au_placement1;
    public AudioClip au_placement2;

    int audio_choice;

    // Use this for initialization
    void Awake()
    {

        audio_source = gameObject.AddComponent<AudioSource>();
        audio_source.clip = au_placement1;

    }

    public void Creation_Audio()
    {
        audio_choice = Random.Range(1, 50);


        if (audio_choice < 25)
        {

            audio_source.clip = au_placement1;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            audio_source.clip = au_placement2;
            GetComponent<AudioSource>().Play();
        }

    }
}
