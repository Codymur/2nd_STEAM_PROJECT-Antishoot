using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziAmmo : MonoBehaviour
{
    public GameObject ShotgunGameOB;
    public UziController UZIScript;

    private void Start()
    {
        ShotgunGameOB = GameObject.Find("Micro Uzi");
        UZIScript = ShotgunGameOB.GetComponent<UziController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UziAmmo")
        {
            UZIScript.ammo += 30;
            Destroy(other.gameObject);
        }
    }
}
