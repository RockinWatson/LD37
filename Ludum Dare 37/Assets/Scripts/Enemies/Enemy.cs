using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int _health;
    public int _speed;
    public int _damage;

    public Enemy(int health, int speed, int damage)
    {
        _health = health;
        _speed = speed;
        _damage = damage;
    }
}