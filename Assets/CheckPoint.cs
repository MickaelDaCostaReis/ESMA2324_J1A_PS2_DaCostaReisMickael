using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Sprite checkedSprite;
    [SerializeField] Sprite unCheckedSprite;
    private SpriteRenderer sr;
    [SerializeField] GameObject key;
    public bool interacted;
    bool inRange =false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        key.SetActive(false);
    }
    private void Update()
    {
        if (inRange && PlayerManager.instance.player.GetButtonDown("Interact"))
        {
            interacted = true;
            sr.sprite = checkedSprite;
            Debug.Log("Interacted !");
        } 
    }

    void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            inRange = true;
            key.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            interacted = false;
            inRange = false;
            key.SetActive(false);
        }
    }
}
