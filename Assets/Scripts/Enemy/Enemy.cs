using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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
    protected Animator animator;
    
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = PlayerManager.instance;
    }
    protected virtual void Update()
    {
        if (GameManager.instance.gameIsPaused) return;
        animator.SetBool("Hurt",isRecoiling);
        if (health <= 0)
        {
            animator.SetTrigger("Die");
        }
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
        animator.SetTrigger("Attack");
        player.TakeDamage(damage);
        player.pState.isRecoilingX = true;
        player.pState.isRecoilingY = true;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !player.pState.isInvincible && PlayerManager.instance.pState.isAlive)
        {
            Attack();
            player.StopTimeOnHit(0, 5, 0.5f);
        }
    }

    protected virtual void Flip()
    {
        if (PlayerManager.instance.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    protected void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }
}
