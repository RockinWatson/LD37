using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour {

    public AudioSource audio_source;
    public AudioClip au_mine_explosion;


    // Use this for initialization
    void Awake()
    {

        audio_source = gameObject.AddComponent<AudioSource>();
        audio_source.clip = au_mine_explosion;
    }


    public void Mine_Explosion()
    {
        audio_source.clip = au_mine_explosion;
        GetComponent<AudioSource>().Play();
    }
}
