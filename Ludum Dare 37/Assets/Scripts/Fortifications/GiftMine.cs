using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftMine : BaseFortification {

    [SerializeField]
    private int _damage = 10;

    private void OnCollisionEnter2D(Collision2D col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            //@TODO: Transition to explody animation. - Implement different states.

            enemy.TakeDamage(_damage);
            GameBoard.Destroy(this.gameObject);
        }
    }
}
