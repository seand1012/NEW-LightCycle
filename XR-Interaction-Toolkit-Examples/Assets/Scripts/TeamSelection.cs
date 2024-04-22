using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSelection : MonoBehaviour
{
    public void ChooseHeroes()
    {
        StartLightCycleBattle("Heroes");
    }

    public void ChooseVillains()
    {
        StartLightCycleBattle("Villains");
    }

    private void StartLightCycleBattle(string team)
    {
        SceneManager.LoadScene("TronLevel", LoadSceneMode.Single);
        // Pass chosen team to LightCycleBattle scene using PlayerPrefs or other methods
        PlayerPrefs.SetString("ChosenTeam", team);
    }

    
}
