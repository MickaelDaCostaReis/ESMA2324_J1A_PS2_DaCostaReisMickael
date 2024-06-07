using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string transitionFromScene;
    public Vector2 platformingRespawnPoint;
    public Vector2 playerRespawnPoint;
    [SerializeField] CheckPoint checkPoint;
    private void Awake()
    {
        if(instance!= null && instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        checkPoint = FindObjectOfType<CheckPoint>();
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawn!");
        if (checkPoint != null)
        {
            if (checkPoint.interacted)
            {
                playerRespawnPoint = checkPoint.transform.position;
            }
            else
            {
                playerRespawnPoint = platformingRespawnPoint;
            }
        }
        else
        {
            playerRespawnPoint = platformingRespawnPoint;
        }
        PlayerManager.instance.transform.position = playerRespawnPoint;
        StartCoroutine(UIManager.instance.DeactivateDeathScreen());
        PlayerManager.instance.Respawn();
    }
}
