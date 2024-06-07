using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Enemy
{
    private bool isDashing;
    private bool canDash =true;
    private bool isDashHandler;
    private bool canUpdatePlayerPos = true;
    [SerializeField] private Vector2 jumpPos;
    [SerializeField] private float dashRange;
    private Vector3 playerPos;
    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 2f;
    }

    protected override void Update()
    {

        base.Update();
        if (!PlayerManager.instance.pState.isAlive)
        {
            //ChangeState(EnemyStates.Ninja_Idle);
        }
        //permet d'éviter un changement de trajectoire/ de rendre l'attaque plus prévisible ; se déclenche sur 0.5 seconde au tout début de la coroutine DashHandler
        if (canUpdatePlayerPos)
        {
            playerPos = PlayerManager.instance.transform.position;
        }
        float _distance = Vector3.Distance(playerPos, transform.position);
        int _direction = ((playerPos.x - transform.position.x) < 0) ? -1 : 1;
       
        if (_distance <= dashRange && canDash && PlayerManager.instance.pState.isAlive)
        {
            if (!isDashHandler)
            {
                StartCoroutine(DashHandler());
            }
            if (isDashing)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(_direction * transform.position.x+_distance, transform.position.y), Time.deltaTime * speed * 2);
            }
        }
    }
    public override void EnemyHit(float _damadeDone, Vector2 _hitDirection, float _hitForce)
    {
        base.EnemyHit(_damadeDone, _hitDirection, _hitForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !player.pState.isInvincible)
        {
            Attack();
            if (PlayerManager.instance.pState.isAlive)
            {
                player.StopTimeOnHit(0, 5, 0.5f);
            }
        }
    }
    IEnumerator DashHandler()
    {
        isDashHandler = true;
        yield return new WaitForSeconds(0.5f);  //prépare le dash
        canUpdatePlayerPos = false;
        isDashing = true;
        yield return new WaitForSeconds(0.2f);  //dash
        canDash = false;
        isDashing = false;
        yield return new WaitForSeconds(2f);    //cooldown  
        canDash = true;
        isDashHandler = false;
        canUpdatePlayerPos = true;
    }
}
