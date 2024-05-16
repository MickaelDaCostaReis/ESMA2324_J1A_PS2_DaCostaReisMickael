using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpack : MonoBehaviour
{
    [SerializeField] private float healing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            collision.GetComponent<Health>().TakeHealthpack(healing);
            gameObject.SetActive(false);
        }
    }
}
