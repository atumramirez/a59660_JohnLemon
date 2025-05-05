using UnityEngine;
using Unity.Netcode;

public class CameraActivator : MonoBehaviour
{
    public Camera serverCamera;
    public Camera clientCamera;

    void Start()
    {
        if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
        {
            serverCamera.enabled = true;
            clientCamera.enabled = false;
        }
        else if (NetworkManager.Singleton.IsHost)
        {
            serverCamera.enabled = true;
            clientCamera.enabled = false; 
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            serverCamera.enabled = false;
            clientCamera.enabled = true;
        }
    }
}