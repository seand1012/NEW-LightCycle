using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.Networking.Transport;
using Unity.XR.CoreUtils;
using Unity.Netcode.Transports.UTP;

public class MultiplayerHandler : NetworkBehaviour
{
    [SerializeField] private Button heroesButton;
    [SerializeField] private Button villainsButton;
    [SerializeField] private Camera stationaryCamera;
    [SerializeField] private Transform xrRigCameraTransform;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private NetworkObject heroPrefab;
    [SerializeField] private NetworkObject villainPrefab;

    private bool hasChosenTeam = false;

    void Start()
    {
        //networkManager.transform = NetworkManager.Singleton.gameObject.AddComponent<UNETTransport>();
        heroesButton.onClick.AddListener(() => ChooseTeam("Heroes"));
        villainsButton.onClick.AddListener(() => ChooseTeam("Villains"));
    }

    public void ChooseTeam(string team)
    {
        if (!hasChosenTeam)
        {
            // Set the chosen team
            PlayerPrefs.SetString("ChosenTeam", team);

            if (!networkManager.IsServer)
            {
                // Start the host
                Debug.Log("Starting host");
                networkManager.StartHost();
            }
            else
            {
                // Start the client
                Debug.Log("Starting client");
                networkManager.StartClient();
            }

            heroesButton.gameObject.SetActive(false);
            villainsButton.gameObject.SetActive(false);

            // Disable the stationary camera
            stationaryCamera.gameObject.SetActive(false);

            // Enable the XR rig camera
            
            xrRigCameraTransform.gameObject.SetActive(true);
            //xrRigCameraTransform.transform.localRotation = Quaternion.Euler(100f, 100f, 0f);
            xrRigCameraTransform.rotation = Quaternion.Euler(-90f,90f,90f);


            // Spawn the player
            SpawnPlayer();

            hasChosenTeam = true;
        }
        else
        {
            Debug.LogWarning("A player has already chosen a team.");
        }
    }

    void SpawnPlayer()
    {
        if (PlayerPrefs.HasKey("ChosenTeam"))
        {
            string chosenTeam = PlayerPrefs.GetString("ChosenTeam");
            Vector3 startingPosition;
            Quaternion startingRotation;
            NetworkObject player;
            Debug.Log("Chosen team: " + chosenTeam);
            if (chosenTeam == "Heroes") 
            {
                startingPosition = new Vector3(75f, 1f, 0f);
                startingRotation = Quaternion.Euler(-90f, 230f, -140f);
                // Instantiate the player prefab
                player = Instantiate(heroPrefab, startingPosition, startingRotation);
            }
            else
            {
                startingPosition = new Vector3(-75f, 1f, 0f);
                startingRotation = Quaternion.Euler(-90f, 0f, -90f);
                player = Instantiate(villainPrefab, startingPosition, startingRotation);
            }
            

            // Teleport the player to the spawn position
            // Assuming your player prefab has a teleport script or component to handle this
            // Example:
            player.transform.position = startingPosition;
            player.transform.rotation = startingRotation;
            Debug.Log("Player position: " + player.transform.position);

            // Switch the camera to the XR rig camera
            // Assuming your XR rig has a camera attached to it and you want to switch to that camera
        }
        else
        {
            Debug.LogError("Chosen team not set for the client.");
        }
    }
}
