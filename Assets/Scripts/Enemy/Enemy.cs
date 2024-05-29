using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float recoilLength;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling =false;

    protected PlayerManager player;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;

    protected float recoilTimer;
    protected Rigidbody2D rb;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.instance;
    }
    protected virtual void Update()
    {
        if(health<=0)
            Destroy(gameObject);
        if (isRecoiling)
        {
            if (recoilTimer < recoilLength)
                recoilTimer += Time.deltaTime;
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
        if (!isRecoiling)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(PlayerManager.instance.transform.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }

    public virtual void EnemyHit(float _damadeDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damadeDone;
        if (!isRecoiling)
        {
            rb.AddForce(-_hitForce * recoilFactor * _hitDirection);
        }
    }

    protected virtual void Attack()
    {
        player.TakeDamage(damage);
        player.pState.isRecoilingX = true;
        player.pState.isRecoilingY = true;
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !player.pState.isInvincible)
        {
            Attack();
            player.StopTimeOnHit(0,5,0.5f);
        }
    }
}
