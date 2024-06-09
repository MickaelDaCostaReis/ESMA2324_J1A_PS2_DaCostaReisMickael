using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHealth : MonoBehaviour
{
    [SerializeField] GameObject canvasUI;
    [SerializeField] HeartShards heartShards;
    [SerializeField] CircleCollider2D circleCollider;

    private void Start()
    {
        if (PlayerManager.instance.maxHealth == PlayerManager.instance.totalMaxHealth)
        {
            Destroy(gameObject);
        }
    }
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
        heartShards.initialFillAmount=PlayerManager.instance.heartShards * 0.25f;
        PlayerManager.instance.heartShards++;
        heartShards.targetFillAmount=PlayerManager.instance.heartShards * 0.25f;
        StartCoroutine(heartShards.LerpFill());
        yield return new WaitForSeconds(3.5f);
        canvasUI.SetActive(false);
        Destroy(gameObject);
    }
}
