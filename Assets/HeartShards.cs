using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartShards : MonoBehaviour
{
    public Image fill;
    public float targetFillAmount;
    public float lerpDuration= 1.5f;
    public float initialFillAmount;

    public IEnumerator LerpFill()
    {
        float elapsedTime = 0f;
        while (elapsedTime<lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/lerpDuration);

            float lerpedFillAmount=Mathf.Lerp(initialFillAmount,targetFillAmount,t);
            fill.fillAmount = lerpedFillAmount;
            yield return null;
        }

        fill.fillAmount=targetFillAmount;
        Debug.Log(fill.fillAmount);
        if(fill.fillAmount == 1)
        {
            PlayerManager.instance.maxHealth++;
            PlayerManager.instance.onHealthChangedCallback();
            PlayerManager.instance.heartShards = 0;
        }
    }
}
