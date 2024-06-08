using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Image heartShards;
    [SerializeField] GameObject dash, multiJumps, wallJump;
    [SerializeField] TMP_Text strength, maxAirJumps;

    private void OnEnable()
    {
        heartShards.fillAmount = PlayerManager.instance.heartShards * 0.25f;
        strength.SetText(PlayerManager.instance.damage.ToString());
        maxAirJumps.SetText(PlayerManager.instance.maxAirJumps.ToString());

        if (PlayerManager.instance.dashPowerUp)
        {
            dash.SetActive(true);
        }
        else
        {
            dash.SetActive(false);
        }

        if (PlayerManager.instance.maxAirJumps>0)
        {
            multiJumps.SetActive(true);
        }
        else
        {
            multiJumps.SetActive(false);
        }

        if (PlayerManager.instance.wallJumpingPowerUp)
        {
            wallJump.SetActive(true);
        }
        else
        {
            wallJump.SetActive(false);
        }

        
    }
}
