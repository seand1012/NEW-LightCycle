using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSelection : MonoBehaviour
{
    public void ChooseTeam(string team)
    {
        // Check if this is the first player to choose a team
        if (!PlayerPrefs.HasKey("ChosenTeam"))
        {
            // Set the chosen team
            PlayerPrefs.SetString("ChosenTeam", team);

            // Start the multiplayer scene
            SceneManager.LoadScene("TronLevel", LoadSceneMode.Single);
        }
        else
        {
            // Start the scene for subsequent players (not host)
            SceneManager.LoadScene("TronLevel", LoadSceneMode.Single);
        }


    }
}
