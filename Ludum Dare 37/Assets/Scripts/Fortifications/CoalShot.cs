using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalShot : MonoBehaviour {

    [SerializeField]
    const float _speed = 1.0f;

    [SerializeField]
    const int _damage = 2;

    [SerializeField]
    const float _spinSpeed = 100.0f;

    public void Initialize(Vector3 origin)
    {
        transform.position = origin;
    }

	void Update ()
    {
        UpdateMove();
        UpdateSpin();
	}

    private void UpdateMove()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);
    }

    private void UpdateSpin()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _spinSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            GameBoard.Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        GameBoard.Destroy(this.gameObject);
    }
}
