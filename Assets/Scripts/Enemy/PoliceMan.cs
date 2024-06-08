using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : Enemy
{
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 2f;
    }
    

    protected override void Update()
    {
        base.Update();
        Flip();
        // suivre le joueur :
        if (!isRecoiling)
        {
            animator.SetBool("Walk",true);
            transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(PlayerManager.instance.transform.position.x, transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    public override void EnemyHit(float _damadeDone, Vector2 _hitDirection, float _hitForce)
    {
        base.EnemyHit(_damadeDone, _hitDirection, _hitForce);
    }
}
