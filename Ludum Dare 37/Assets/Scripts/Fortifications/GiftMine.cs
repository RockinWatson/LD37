using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GiftMine : BaseFortification {

    [SerializeField]
    private int _damage = 10;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (CanUpdate())
        {
            AttemptCollideWithEnemy(col);
        }
    }

    private void AttemptCollideWithEnemy(Collision2D col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            //Audio Effect
            var death_audio = GameObject.Find("MineExplosion");
            death_audio.GetComponent<MineExplosion>().Mine_Explosion();

            //@TODO: Transition to explody animation. - Implement different states.
            enemy.TakeDamage(_damage);
            GameCell cell = GameBoard.Get().GetGameCellOnWorldPos(transform.position);
            cell.RemoveFortification();
        }
    }

    public override bool IsAttackable()
    {
        return false;
    }
}
