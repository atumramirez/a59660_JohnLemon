using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class SceneSetup : MonoBehaviour
{
    [Header("Prefabs")]
    public NetworkObject clientObjectPrefab; // Drag the client-controlled object prefab here
    public NetworkObject serverObjectPrefab; // Drag the server-controlled object prefab here

    void Start()
    {
        // Only the server should handle spawning
        if (NetworkManager.Singleton.IsServer)
        {
            // Wait for scene load to complete before spawning anything
            NetworkManager.Singleton.SceneManager.OnLoadComplete += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        // Ensure this only runs ONCE on the server
        if (!NetworkManager.Singleton.IsServer) return;

        Debug.Log($"Scene loaded for client {clientId}");

        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            // This is the server/host itself
            var serverObj = Instantiate(serverObjectPrefab);
            serverObj.Spawn(); // Owned by the server
        }
        else
        {
            // This is a remote client
            var clientObj = Instantiate(clientObjectPrefab);
            clientObj.SpawnAsPlayerObject(clientId); // Assign ownership to that client
        }
    }
}