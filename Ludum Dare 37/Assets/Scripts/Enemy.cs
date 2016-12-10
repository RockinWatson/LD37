using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    private static int _health;
    private static int _speed;
    private static int _damage;

    public Enemy(int health, int speed, int damage)
    {
        _health = health;
        _speed = speed;
        _damage = damage;
    }
    


}