using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private float _speed = 0.5f;
    [SerializeField]
    private int _damage = 5;
    [SerializeField]
    private float _attackSpeed = 3.0f;
    private float _attackTimer = 0.0f;

    [SerializeField]
    private float _stoppingDistanceToObstacle = 1.0f;

    private bool canMove = true;

    private void Start()
    {
        _attackTimer = 0.0f;
    }

    private void Update()
    {
        Fortification fort = CheckForObstacle();
        if (fort != null)
        {
            // Try to do damage to it.
            if (_attackTimer >= _attackSpeed)
            {
                fort.TakeDamage(_damage);
                _attackSpeed = 0.0f;
            }
            _attackSpeed += Time.deltaTime;
        }
        else
        {
            _attackSpeed = 0.0f;
            if (canMove)
            {
                transform.Translate(Vector2.right * _speed * Time.deltaTime);
            }
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

            var death_audio = GameObject.Find("EnemyDeath");
            death_audio.GetComponent<EnemyDeath>().Enemy_Death();
            GameObject.Destroy(this.gameObject);

        }
    }

    private Fortification CheckForObstacle()
    {
        GameCell cell = GameBoard.Get().GetGameCellRightOfPos(transform.position);
        if (cell != null)
        {
            // Debug draw the cell we're focused on:
            //cell.Draw(true);

            Fortification fort = cell.GetFortification();
            if (fort != null && fort.IsSet() && fort.IsAttackable())
            {
                Vector3 fortPos = fort.GetPos();
                float sqrMag = (fortPos - transform.position).sqrMagnitude;
                if (sqrMag <= Mathf.Pow(_stoppingDistanceToObstacle, 2.0f))
                {
                    return fort;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}