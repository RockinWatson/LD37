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

        GameBoard.Get().SetInputAlreadyHandled();
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
