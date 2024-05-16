using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image currentHealth;
    [SerializeField] private Image maxHealth;

    private void Start()
    {
        maxHealth.fillAmount = playerHealth.currenthealth / 20;
    }
    private void Update()
    {
        currentHealth.fillAmount = playerHealth.currenthealth / 20;


    }
}