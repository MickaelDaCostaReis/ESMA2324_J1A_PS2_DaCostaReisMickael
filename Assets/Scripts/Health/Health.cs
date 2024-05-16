using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxhealth; 
    [SerializeField] private float iframestime;
    [SerializeField] private float blinks;
    [SerializeField] private Transform spawn;
    private SpriteRenderer blinkingSprite;
    
    public GameOver gameOver;
    int score = 0;

    private Animator animation;
    [HideInInspector] public float currenthealth;


    private void Start()
    {
        currenthealth = maxhealth;
        blinkingSprite = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - damage, 0, maxhealth);
        if (currenthealth > 0)
        {
            StartCoroutine(Invincibility());
        }
        else
        {
            animation.SetTrigger("Die");
        }
    }

    public void TakeHealthpack(float heals)
    {
        currenthealth = Mathf.Clamp(currenthealth + heals, 0, maxhealth);
    }
    
    private IEnumerator Invincibility()
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);
        for (int i = 0; i < blinks; i++)
        {
            blinkingSprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(iframestime / (blinks * 2));
            blinkingSprite.color = Color.white;
            yield return new WaitForSeconds(iframestime / (blinks * 2));
        }
        Physics2D.IgnoreLayerCollision(3, 6, false);
    }
    public void Respawn()
    {
        transform.position = spawn.position;
        TakeHealthpack(maxhealth);
        gameOver.UnDisplay();
        animation.ResetTrigger("Die");
        animation.Play("Idle");
        GetComponent<PlayerManager>().enabled = true;
    }
    public void GameOver()
    {
        gameOver.Setup(score);
        GetComponent<PlayerManager>().enabled = false;
    }
}