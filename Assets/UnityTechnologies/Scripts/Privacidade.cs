using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Privacidade : MonoBehaviour
{
    public GameObject player;
    bool m_IsPlayerAtExit;

    public GameObject fantasma;
    public GameObject efeito;
    public GameObject efeito2;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            Desvanecer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    void Desvanecer()
    {
        fantasma.SetActive(false);
        efeito.SetActive(false);
        efeito2.SetActive(false);
    }

}
