using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;

public class PlayerSpawner : NetworkBehaviour
{
    public GameObject[] playerPrefabs; // Array of player prefabs
    [SerializeField]
    private GameObject playerPrefab;

    void Start()
    {
        //DontDestroyOnLoad(this, gameObject);
    }
    public override void OnNetworkSpawn()
    {
        //NetworkManager.Singleton.SceneManager.OnLoadComplete += SceneLoaded;
        if (IsServer)
        {
            
            // Only the server should spawn player objects
            SpawnPlayer();
        }
    }

    private void SceneLoaded(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if(IsHost && sceneName == "TronLevel")
        {
            foreach(ulong id in clientsCompleted)
            {
                GameObject player = Instantiate(playerPrefab);
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(id, true);
            }
        }
    }

    private void SpawnPlayer()
    {
        int selectedPlayerIndex = GetSelectedPlayerIndex();

        if (selectedPlayerIndex >= 0 && selectedPlayerIndex < playerPrefabs.Length)
        {
            GameObject selectedPlayerPrefab = playerPrefabs[selectedPlayerIndex];

            // Spawn the selected player object for this client
            //NetworkManager.Singleton.SpawnManager.Instantiate(selectedPlayerPrefab, null, true);
        }
        else
        {
            Debug.LogError("Invalid player selection index.");
        }
    }

    private int GetSelectedPlayerIndex()
    {
        // Example method to get the selected player index
        // Replace this with your own logic to determine the selected player index
        // This could be based on UI selection, player input, etc.
        return 0; // Default to the first player prefab for demonstration
    }
}
