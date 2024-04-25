using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class TronLevelManager : NetworkManager
{
    public GameObject[] playerPrefabs; // Array of player prefabs
    public Transform[] heroSpawnPoints;
    public Transform[] villainSpawnPoints;
    public Vector3 spawnPosition;

    /*public override void OnStartServer()
    {
        base.OnStartServer();

        // Check if this is the host
        if (NetworkManager.Singleton.IsHost)
        {
            // Determine spawn points based on chosen team
            string chosenTeam = PlayerPrefs.GetString("ChosenTeam");
            Transform[] spawnPoints = chosenTeam == "Heroes" ? heroSpawnPoints : villainSpawnPoints;

            // Assign spawn points to the NetworkManager
            networkConfig.NetworkPrefabs.Add(playerPrefabs[0]); // Villain prefab
            networkConfig.NetworkPrefabs.Add(playerPrefabs[1]); // Hero prefab
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                NetworkSpawnManager.Spawn(spawnPoints[i].position, Quaternion.identity);
            }
        }
    }*/

    
    void Start()
    {
        // You can initialize any required variables or setup here
        spawnPosition = new Vector3(20f, 20f, 20f);
    }

    /*public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        playerPrefabs[0] = Resources.Load<GameObject>("Assets/Prefabs/TronVillain.prefab");
        playerPrefabs[1] = Resources.Load<GameObject>("Assets/Prefabs/TronHero.prefab");
    }*/
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
                    //SpawnObject(Resources.Load<GameObject>("Prefabs/tron_bike"));
                    NetworkManager.Singleton.StartHost();
                    //NetworkManager.

                }
                else if (mousePosition.y >= 120 && mousePosition.y <= 220)
                {
                    // Client button clicked
                    NetworkManager.Singleton.StartClient();
                    //SpawnObject(Resources.Load<GameObject>("Prefabs/tron_bike"));
                    
                }
                else if (mousePosition.y >= 230 && mousePosition.y <= 330)
                {
                    // Server button clicked

                    NetworkManager.Singleton.StartServer();
                    //SpawnObject(Resources.Load<GameObject>("Prefabs/tron_bike"));
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
