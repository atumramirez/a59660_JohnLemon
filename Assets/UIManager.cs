using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MenuPrincipal;
    public GameObject MenuHost;
    void Start()
    {
        MenuPrincipal.SetActive(true);
        MenuHost.SetActive(false);
    }

    public void AbrirHost()
    {
        MenuPrincipal.SetActive(false);
        MenuHost.SetActive(true);
    }

    public void Voltar()
    {
        MenuPrincipal.SetActive(true);
        MenuHost.SetActive(false);
    }
}
