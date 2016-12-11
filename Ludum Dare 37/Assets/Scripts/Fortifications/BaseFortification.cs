using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFortification : MonoBehaviour {

    [SerializeField]
    private int _health = 10;

    public bool TakeDamage(int damage)
    {
        _health -= damage;
        return (_health <= 0);
    }
}
