using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StrengthPowerUp : MonoBehaviour
{
    [SerializeField] GameObject canvasUI;
    [SerializeField] TMP_Text UITextMeshPro;
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
        PlayerManager.instance.damage = Mathf.Clamp(PlayerManager.instance.damage + 1, 1, 4);
        string _newDamage = PlayerManager.instance.damage.ToString();
        UITextMeshPro.SetText(_newDamage);
        canvasUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        canvasUI.SetActive(false);
        Destroy(gameObject);
    }
}
