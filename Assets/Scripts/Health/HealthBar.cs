using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    PlayerManager player;
    private GameObject[] heartContainers;
    private Image[] hearts;
    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        player = PlayerManager.instance;
        heartContainers = new GameObject[PlayerManager.instance.maxHealth];
        hearts = new Image[PlayerManager.instance.maxHealth];
        player.onHealthChangedCallback += UpdateHealthBar;
        InstantianteHeartContainers();
        UpdateHealthBar();
    }

    private void Update()
    {
        
    }

    private void SetHeartContainer()
    {
        for(int i = 0; i < heartContainers.Length; i++)
        {
            if (i < player.maxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }    
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.CurrentHealth)
            {
                hearts[i].fillAmount = 1;
            }
            else
            {
                hearts[i].fillAmount = 0;
            }
        }
    }

    private void InstantianteHeartContainers()
    {
        for (int i = 0; i < player.maxHealth; i++)
        {
            GameObject tmp = Instantiate(heartContainerPrefab);
            tmp.transform.SetParent(heartsParent, false);
            heartContainers[i] = tmp;
            hearts[i] = tmp.transform.Find("Heart").GetComponent<Image>();
        }
    }

     public void UpdateHealthBar()
    {
        SetHeartContainer();
        SetFilledHearts();
    }
}