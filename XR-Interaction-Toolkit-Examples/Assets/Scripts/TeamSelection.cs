using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class TeamSelection : NetworkBehaviour
{
    [SerializeField] public NetworkObject playerPrefab;
    [SerializeField] public Camera startingCamera;
    public NetworkManager networkManager;
    public void ChooseTeam(string team)
    {
        // Check if this is the first player to choose a team
        if (networkManager.IsServer)
        {
            // Set the chosen team
            PlayerPrefs.SetString("ChosenTeam", team);
            
            networkManager.StartHost();
            // Start the multiplayer scene
            //SceneManager.LoadScene("Assets/Scenes/TronLevel.unity", LoadSceneMode.Single);
        }
        else
        {
            networkManager.StartClient();
            // Start the scene for subsequent players (not host)
            //SceneManager.LoadScene("Assets/Scenes/TronLevel.unity", LoadSceneMode.Single);
            
        }

        startingCamera.depth = -2;
    }

    public override void OnNetworkSpawn()
    {
        if(networkManager.IsServer)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        if (PlayerPrefs.HasKey("ChosenTeam"))
        {
            string chosenTeam = PlayerPrefs.GetString("ChosenTeam");
            NetworkObject player = Instantiate(playerPrefab);
            player.SpawnAsPlayerObject(networkManager.LocalClientId);
        }
        else
        {
            Debug.LogError("Chosen team not set for the client.");
        }
    }
}
