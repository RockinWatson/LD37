using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    public AudioSource audio_source;
    public AudioClip au_enemy_death;


    // Use this for initialization
    void Awake()
    {

        audio_source = gameObject.AddComponent<AudioSource>();
        audio_source.clip = au_enemy_death;

    }

    // Update is called once per frame
    void Update()
    {
    }



    public void Enemy_Death()
    {
        audio_source.clip = au_enemy_death;
        GetComponent<AudioSource>().Play();
    }
    
}
