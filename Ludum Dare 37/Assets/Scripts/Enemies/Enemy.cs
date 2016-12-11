using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private float _speed = 0.5f;
    [SerializeField]
    private int _damage = 5;

    private bool canMove = true;

    private void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.name == "EnemyStopper")
        {
            transform.Translate(new Vector3(0, 0, 0));
            canMove = false;
            Debug.Log("Hitten Dat Stopper");
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}