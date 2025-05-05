using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject ghostPrefab;

    void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            // Spawn a player prefab on the server
            GameObject player = Instantiate(playerPrefab, new Vector3(-10, 0, 0), Quaternion.identity);
            NetworkObject networkObject = player.GetComponent<NetworkObject>();

            GameObject ghostPlayer = Instantiate(ghostPrefab, new Vector3(-10, 0.1f, -3), Quaternion.identity);
            NetworkObject ghostNetworkObject = ghostPlayer.GetComponent<NetworkObject>();

            // Spawn the NetworkObject
            networkObject.Spawn();
            ghostNetworkObject.Spawn();
        }
    }
}