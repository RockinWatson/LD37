using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfAudio2 : MonoBehaviour {

    public AudioSource audio_source;
    public AudioClip au_placement1;
    public AudioClip au_placement2;

    int audio_choice;

    public void Creation_Audio()
    {
        audio_source = gameObject.GetComponent<AudioSource>();
        audio_source.clip = au_placement1;

        if (Random.value < 0.5f)
        {
            audio_source.clip = au_placement1;
        }
        else
        {
            audio_source.clip = au_placement2;
        }

        audio_source.Play();
    }
}
