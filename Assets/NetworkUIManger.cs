using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class NetworkUIManger : MonoBehaviour
{
    public GameObject ClientUi;
    void Start()
    {
        if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
        {
            ClientUi.SetActive(false);
        }
        else if (NetworkManager.Singleton.IsHost)
        {
            ClientUi.SetActive(false);
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            ClientUi.SetActive(true);
        }
    }

}
