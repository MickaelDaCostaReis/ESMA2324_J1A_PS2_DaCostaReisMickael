using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public SceneFader sceneFader;
    [SerializeField] GameObject deathScreen;
    public GameObject inventory;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        sceneFader = GetComponentInChildren<SceneFader>();
    }

    public IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(sceneFader.Fade(SceneFader.FadeDirection.In));
        yield return new WaitForSeconds(0.8f);
        deathScreen.SetActive(true);
    }

    public IEnumerator DeactivateDeathScreen()
    {
        yield return new WaitForSeconds(0.5f);
        deathScreen.SetActive(false);
        StartCoroutine(sceneFader.Fade(SceneFader.FadeDirection.Out));
    }
}
