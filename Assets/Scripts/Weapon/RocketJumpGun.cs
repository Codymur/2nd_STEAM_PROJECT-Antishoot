using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketJumpGun : MonoBehaviour
{
    //RocketJump
    public Rigidbody RocketJumpBullet;
    public float RocketJumpBulletSpeed = 10f;


    public void RocketJumpExplode()
    {
        Rigidbody bulletClone = (Rigidbody)Instantiate(RocketJumpBullet, transform.position, transform.rotation);
        bulletClone.velocity = transform.forward * RocketJumpBulletSpeed;
    }
}
