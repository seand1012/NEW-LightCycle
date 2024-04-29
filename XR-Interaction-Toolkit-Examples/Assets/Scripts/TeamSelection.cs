using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class TeamSelection : NetworkBehaviour
{
    [SerializeField] public NetworkObject player;
    [SerializeField] public Camera startingCamera;
    public NetworkManager networkManager;
    public void ChooseTeam(string team)
    {
        // Check if this is the first player to choose a team
        if (!PlayerPrefs.HasKey("ChosenTeam"))
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


}
