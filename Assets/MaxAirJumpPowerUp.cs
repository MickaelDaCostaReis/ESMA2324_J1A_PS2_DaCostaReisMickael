using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAirJumpPowerUp : MonoBehaviour
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
        PlayerManager.instance.maxAirJumps = Mathf.Clamp(++PlayerManager.instance.maxAirJumps,0,3);
        canvasUI.SetActive(false);
        Destroy(gameObject);
    }
}
