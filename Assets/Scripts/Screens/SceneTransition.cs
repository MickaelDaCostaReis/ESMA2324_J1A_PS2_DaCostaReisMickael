using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string transitionTo;
    [SerializeField] private Transform startPos;
    [SerializeField] private Vector2 exitDirection;
    [SerializeField] private float exitTime;
    private void Start()
    {
        if(transitionTo == GameManager.instance.transitionFromScene)
        {
            PlayerManager.instance.transform.position = startPos.position;
            StartCoroutine(PlayerManager.instance.WalkIntoNewScene(exitDirection, exitTime));
        }
        
        StartCoroutine(UIManager.instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.transitionFromScene = SceneManager.GetActiveScene().name;
            PlayerManager.instance.pState.isInCutScene = true;
            PlayerManager.instance.pState.isInvincible = true;
            StartCoroutine(UIManager.instance.sceneFader.FadeAndLoadScene(SceneFader.FadeDirection.In, transitionTo));
            
        }
    }
}
