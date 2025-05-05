using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaHandler : MonoBehaviour
{
    public Slider playerSlider;
    public CanvasGroup CanvasGroup;
    public float fadeDuration = 0.5f;
    float m_Timer;
    bool isDesligada;

    private void Start()
    {
        isDesligada = false;
    }

    void Update()
    {
        if (playerSlider.value == playerSlider.maxValue && isDesligada == false)
        {
            Desligar();
        }
        else if (playerSlider.value != playerSlider.maxValue && isDesligada)
        {
            Ligar();
        }
    }

    void Desligar()
    {
        Debug.Log("Desigar");
        m_Timer += Time.deltaTime;
        CanvasGroup.alpha = Mathf.Clamp01(1f - (m_Timer / fadeDuration));

        isDesligada = true;
    }

    void Ligar()
    {
        Debug.Log("Ligar");
        m_Timer += Time.deltaTime;
        CanvasGroup.alpha = m_Timer / fadeDuration;

        isDesligada = false;
    }
}
