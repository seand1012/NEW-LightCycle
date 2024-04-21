using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    // Function to handle Light Cycle Battle button click
    public void StartLightCycleBattle()
    {
        SceneManager.LoadScene("LightCycleSelectorScene");
    }

    // Function to handle Disk Battle button click
    public void StartDiskBattle()
    {
        SceneManager.LoadScene("DiskBattleScene");
    }
}