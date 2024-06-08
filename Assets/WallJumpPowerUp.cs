using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpPowerUp : MonoBehaviour
{
    [SerializeField] GameObject canvasUI;
    [SerializeField] CircleCollider2D circleCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StartCoroutine(ShowUI());
        }
    }

    IEnumerator ShowUI()
    {
        circleCollider.enabled = false;
        canvasUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        PlayerManager.instance.wallJumpingPowerUp = true;
        canvasUI.SetActive(false);
        Destroy(gameObject);
    }
}
