using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritLarge : MonoBehaviour {

    [SerializeField]
    const float _speed = 0.75f;

    [SerializeField]
    private int _score = 15;

    private void Update()
    {
        MoveDown();

        //@TODO: Figure out when to delete the Spirit (off-screen, etc.)
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    public void Activate()
    {
        //Play Pickup Audio
        var spirit_audio = GameObject.Find("AudioController");
        spirit_audio.GetComponent<AudioController>().Spirit_Audio();

        GameBoard.Get().AddScore(_score);
        GameObject.Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        Activate();
    }

    private void OnBecameInvisible()
    {
        GameBoard.Destroy(this.gameObject);
    }
}
