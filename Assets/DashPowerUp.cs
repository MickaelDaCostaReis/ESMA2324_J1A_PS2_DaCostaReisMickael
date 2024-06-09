using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerUp : MonoBehaviour
{
    [SerializeField] GameObject canvasUI;
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] GameObject shardsPrefab;
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
        if (PlayerManager.instance.dashPowerUp)
        {
            Instantiate(shardsPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            PlayerManager.instance.dashPowerUp = true;
            canvasUI.SetActive(true);
            yield return new WaitForSeconds(5f);
            canvasUI.SetActive(false);
        }
        Destroy(gameObject);
        yield return null;
    }
}
