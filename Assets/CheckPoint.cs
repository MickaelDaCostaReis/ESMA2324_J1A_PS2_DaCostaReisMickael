using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool interacted;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&& PlayerManager.instance.player.GetButtonDown("Interact"))
        {
            interacted = true;
        }
    }
}
