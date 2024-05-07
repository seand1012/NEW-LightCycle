using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDeath1 : MonoBehaviour
{
    public GameObject Player;
    public GameObject AI;
    public Camera PlayerCamera;
    // Update is called once per frame
    void Update()
    {
        if (AI == null)
        {
            PlayerCamera.enabled = false; ;
        }
    }
}