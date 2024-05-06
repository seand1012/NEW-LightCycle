using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDeath : MonoBehaviour
{
    public GameObject Player;
    public GameObject AI;
    public Camera MainCamera;
    public Camera PlayerCamera;
  
    // Update is called once per frame
    void Update()
    {
        if(AI == null) 
        {
            PlayerCamera.enabled = false;
            MainCamera.enabled = true;
            MainCamera.depth = 10;
        }
        if(Player == null)
        {
            MainCamera.enabled = true;
        }
    }
}
