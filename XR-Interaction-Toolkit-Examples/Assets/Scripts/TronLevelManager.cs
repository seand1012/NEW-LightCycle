using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;

public class TronLevelManager : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Array of player prefabs
    public Transform[] spawnPoints; // Array of spawn point
    public Vector3 spawnPosition; // Variable to hold the spawn position


    void Start()
    {
        // You can initialize any required variables or setup here
        spawnPosition = new Vector3(20f, 20f, 20f);
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        GUILayout.Button("Host");
        GUILayout.Button("Client");
        GUILayout.Button("Server");
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            CheckStartButtons();
        }
        else
        {
            StatusLabels();
        }
        GUILayout.EndArea();
    }

    void CheckStartButtons()
    {
        if (Event.current.type == EventType.Repaint || Event.current.type == EventType.Layout)
            return;

        

        if (Input.GetMouseButtonUp(0)) // Check for left mouse button release
        {
            Vector2 mousePosition = Event.current.mousePosition;

            // Check if mouse position is within the button area
            if (mousePosition.x >= 10 && mousePosition.x <= 310 && mousePosition.y >= 10 && mousePosition.y <= 310)
            {
                // Determine which button was clicked based on mouse position
                if (mousePosition.y >= 10 && mousePosition.y <= 110)
                {
                    // Host button clicked
                    NetworkManager.Singleton.StartHost();
                    SpawnObject(Resources.Load<GameObject>("Prefabs/tron_bike"));

                }
                else if (mousePosition.y >= 120 && mousePosition.y <= 220)
                {
                    // Client button clicked
                    NetworkManager.Singleton.StartClient();
                    SpawnObject(Resources.Load<GameObject>("Prefabs/tron_bike"));
                }
                else if (mousePosition.y >= 230 && mousePosition.y <= 330)
                {
                    // Server button clicked

                    NetworkManager.Singleton.StartServer();
                    SpawnObject(Resources.Load<GameObject>("Prefabs/tron_bike"));
                }
            }
        }
    }

    void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    // Function to spawn an object at a specified position
    public void SpawnObject(GameObject objectToSpawn)
    {
        Debug.Log("Spawn Position: " + spawnPosition);
        Debug.Log(objectToSpawn.ToString());
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
