using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSelection : MonoBehaviour
{
    int counter = 0;
    public void ChooseHeroes()
    {
        counter++;
        StartLightCycleBattle("Heroes");
    }

    public void ChooseVillains()
    {
        counter++;
        StartLightCycleBattle("Villains");
        
    }

    private void StartLightCycleBattle(string team)
    {
        
        if(counter >= 2)
        {
            SceneManager.LoadScene("TronLevel", LoadSceneMode.Single);
            // Pass chosen team to LightCycleBattle scene using PlayerPrefs or other methods
            PlayerPrefs.SetString("ChosenTeam", team);
        }
        else
        {
            SceneManager.LoadScene("Station_01_Setup_Isolated", LoadSceneMode.Single);
        }
        
    }

    
}
