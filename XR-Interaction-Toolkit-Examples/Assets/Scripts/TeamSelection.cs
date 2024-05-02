using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using UnityEditor;

public class TeamSelection : NetworkBehaviour
{
    [SerializeField] public GameObject playerPrefab ;
    [SerializeField] public Camera startingCamera;
    public Transform[] spawnPoints;
    public NetworkManager networkManager;
    Vector3 spawnPosition = new Vector3(20f, 20f, 20f);
    Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);

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
        if (networkManager.IsServer)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        if (PlayerPrefs.HasKey("ChosenTeam"))
        {
            string chosenTeam = PlayerPrefs.GetString("ChosenTeam");
            
            //Debug.Log("Spawn Position: " + spawnPosition);
            //Debug.Log("Rotation: " + rotation.eulerAngles);
            Vector3 spawnPosition = new Vector3(20f, 20f, 20f);
            Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
            GameObject player = Instantiate(playerPrefab, spawnPosition, rotation);
            NetworkObject networkObject = player.GetComponent<NetworkObject>();
            networkObject.SpawnAsPlayerObject(networkManager.LocalClientId);
        }
        else
        {
            Debug.LogError("Chosen team not set for the client.");
        }
    }
}
