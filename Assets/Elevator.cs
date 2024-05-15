using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private bool isMovingUp, hitTrigger,isHorizontal,playerPresent;
    [SerializeField] private float speed;
    [SerializeField] private Transform pointA, pointB, elevator;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isHorizontal && playerPresent)
        {
            if (isMovingUp && !hitTrigger && rb.transform.position.y < pointA.transform.position.y)
            {
                rb.velocity = Vector2.up * speed;
            }
        
            else if(!isMovingUp && !hitTrigger && rb.transform.position.y > pointB.transform.position.y)
            {
                rb.velocity = Vector2.down * speed;
            }
        }

        if (isHorizontal && playerPresent)
        {
            if (isMovingUp && !hitTrigger && rb.transform.position.x < pointA.transform.position.x)
            {
                rb.velocity = Vector2.right * speed;
            }

            else if (!isMovingUp && !hitTrigger && rb.transform.position.x > pointB.transform.position.x)
            {
                rb.velocity = Vector2.left * speed;
            }
        }      
    }

    private void ChangeDirection()
    {
        isMovingUp = !isMovingUp;
        hitTrigger = false;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPresent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPresent = false;
            hitTrigger = true;
            ChangeDirection();
        }
    }
}
