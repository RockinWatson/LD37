using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour {

    [SerializeField]
    float _speed = 3.0f;

    [SerializeField]
    float _spinSpeed = 100.0f;
	
	// Update is called once per frame
	void Update ()
    {
        MoveAndSpin();
	}

    private void MoveAndSpin()
    {
        Vector3 angle = new Vector3(5,4);
        transform.Translate(angle * _speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * Time.deltaTime * _spinSpeed);
    }

    private void OnBecameInvisible()
    {
        GameBoard.Destroy(this.gameObject);
    }
}
