using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmo : MonoBehaviour
{
    public GameObject ShotgunGameOB;
    public ShotgunController ShotGunScript;

    private void Start()
    {
        ShotgunGameOB = GameObject.Find("Remington 870");
        ShotGunScript = ShotgunGameOB.GetComponent<ShotgunController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShotgunAmmo")
        {
            ShotGunScript.ammo += 5;
            Destroy(other.gameObject);
        }
    }

}
