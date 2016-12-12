using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflake : MonoBehaviour {

    [SerializeField]
    const float _speed = 1.0f;

    [SerializeField]
    const float _spinSpeed = 50.0f;
	
	// Update is called once per frame
	void Update ()
    {
        MoveAndSpin();
	}

    private void MoveAndSpin()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * Time.deltaTime * _spinSpeed);
    }

    private void OnBecameInvisible()
    {
        GameBoard.Destroy(this.gameObject);
    }
}
