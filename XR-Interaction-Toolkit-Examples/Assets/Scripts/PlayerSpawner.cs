using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    public GameObject[] playerPrefabs; // Array of player prefabs

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // Only the server should spawn player objects
            SpawnPlayer();
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
