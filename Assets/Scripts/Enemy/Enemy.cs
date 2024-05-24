using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float recoilLength;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling =false;

    [SerializeField] protected PlayerManager player;
    [SerializeField] protected float speed;

    protected float recoilTimer;
    protected Rigidbody2D rb;
    protected virtual void Awake()
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
    }

    protected virtual void EnemyHit(float _damadeDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damadeDone;
        if (!isRecoiling)
        {
            rb.AddForce(-_hitForce * recoilFactor * _hitDirection);
        }
    }
}
