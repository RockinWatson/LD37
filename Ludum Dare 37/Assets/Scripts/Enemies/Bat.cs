using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {
  
    private static int _health = 5;
    private static float _speed = 5;
    private static int _damage = 5;

    private bool canMove = true;

    private void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.name == "EnemyStopper")
        {
            transform.Translate(new Vector3(0, 0, 0));
            canMove = false;
            Debug.Log("Hitten Dat Stopper");
        }
    }
}
