using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShot : MonoBehaviour
{
    [SerializeField]
    const float _speed = 2.0f;

    [SerializeField]
    const int _damage = 6;

    public void Initialize(Vector3 origin)
    {
        transform.position = origin;
    }

    void Update()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);
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
