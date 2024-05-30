using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(RespawnPoint());
        }
    }

    IEnumerator RespawnPoint()
    {
        PlayerManager.instance.pState.isInCutScene = true;
        PlayerManager.instance.pState.isInvincible = true;
        PlayerManager.instance.rb.velocity = Vector2.zero;
        Time.timeScale = 0;
        StartCoroutine(UIManager.instance.sceneFader.Fade(SceneFader.FadeDirection.In));
        PlayerManager.instance.TakeDamage(1);
        yield return new WaitForSeconds(1);
        PlayerManager.instance.transform.position = GameManager.instance.platformingRespawnPoint;
        StartCoroutine(UIManager.instance.sceneFader.Fade(SceneFader.FadeDirection.In));
        yield return new WaitForSeconds(UIManager.instance.sceneFader.fadeTime);
        PlayerManager.instance.pState.isInCutScene = false;
        PlayerManager.instance.pState.isInvincible = false;
        Time.timeScale = 1;
    }
}
