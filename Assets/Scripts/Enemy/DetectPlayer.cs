using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectPlayer : MonoBehaviour
{
    public bool playerInTheRoom = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTheRoom = true;
            Debug.Log(other.gameObject.name);
        }
    }

}
