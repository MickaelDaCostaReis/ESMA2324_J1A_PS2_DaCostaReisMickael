using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyTrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && PlayerManager.instance.pState.isAlive && !PlayerManager.instance.pState.isInvincible)
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
        yield return new WaitForSecondsRealtime(0.25f);
        PlayerManager.instance.transform.position = GameManager.instance.platformingRespawnPoint;
        StartCoroutine(UIManager.instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
        yield return new WaitForSecondsRealtime(UIManager.instance.sceneFader.fadeTime);
        PlayerManager.instance.pState.isInCutScene = false;
        PlayerManager.instance.pState.isInvincible = false;
        Time.timeScale = 1;
    }
}
