using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUselessMag : MonoBehaviour
{
    public GameObject bullet;
    public Transform BulletOutPosition;
    
    public void InstantiatingBullet()
    {
        Quaternion rot = gameObject.GetComponentInParent<Transform>().rotation * Quaternion.AngleAxis(90, Vector3.forward);
        GameObject MethodBullet = Instantiate(bullet, BulletOutPosition.position, rot);
        MethodBullet.AddComponent<Rigidbody>();
        Rigidbody rb = (Rigidbody)MethodBullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 2, ForceMode.Impulse);
        
    }
}
