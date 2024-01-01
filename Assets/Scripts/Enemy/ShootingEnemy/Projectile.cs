using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody bullet;

    private void Start()
    {
        
        Destroy(gameObject, 20f);
    }

    void Update()
    {

        


    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    Destroy(gameObject, 0.05f);
    //}
}
