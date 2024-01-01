using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotgunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce;

    public float FireRate;

    public Camera FpsCamera;
    public Transform cam;

    public Animator GunAnim;

    private float NextTimeToFire;

    public Image CrossHair;
    

    public LayerMask CanHit;

    public GameObject ImpactEffect;
    public GameObject MuzzleEffect;
    public GameObject BloodSplatter;

    public GameObject MuzzleTip;

    [Header("Shotgun")]
    public int BulletsPerShot;
    public float InAccuracyDistance;
    public ShotgunRecoil ShotgunRecoilScript;


    public int ammo;
    public TMP_Text ammoText;

    public CameraRecoil CameraRecoilScript;

    public RocketJumpGun RocketJumpingScript;

    public Rigidbody Rb;
    public float BackForce;
    public GameObject Player;

    public PlayerController PlayerControllerScript;
    public CameraController CameraControllerScript;

    public bool CanShoot = true;


    public bool NowShooting = false;

    private void Start()
    {
        PlayerControllerScript = GameObject.FindGameObjectWithTag("MainPlayerTag").GetComponent<PlayerController>();

        ammo = 10;


        CrossHair.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        ShotgunRecoilScript = GetComponent<ShotgunRecoil>();
        GunAnim = GetComponent<Animator>();
        GunAnim.Play("Idle", -1, 0f);
        CameraRecoilScript = GameObject.FindGameObjectWithTag("CamRecoil").GetComponent<CameraRecoil>();
    }

    private void Update()
    {
        ammoText.text = ammo.ToString();
        if ((Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.JoystickButton5)) && Time.time >= NextTimeToFire && ammo > 0 && CanShoot)
        {
            NowShooting = true;
            NextTimeToFire = Time.time + 1f / FireRate;
            ShotgunRecoilScript.Fire();
            CameraRecoilScript.FirePowerful();
            Shoot();
        }
        else
        {
            NowShooting = false;
        }
    }

    public void Shoot()
    {
        
        ammo--;
        GameObject MuzzleOB = Instantiate(MuzzleEffect, MuzzleTip.transform.position, Quaternion.identity);
        //RocketJumpingScript.RocketJumpExplode();
        Destroy(MuzzleOB, 2f);
        StartCoroutine(CrossHairScale());
        GunAnim.Play("Shoot", -1, 0f);

        for (int i = 0; i < BulletsPerShot; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(FpsCamera.transform.position, GetShootingDirection(), out hit, range, CanHit))
            {
                float distance = hit.distance;
                Target target = hit.transform.GetComponent<Target>();
                Bomb bombScript = hit.transform.GetComponent<Bomb>();
                Destroyer organs = hit.transform.GetComponent<Destroyer>();
                TrashHealth TrashTarget = hit.transform.GetComponent<TrashHealth>();
                HelicopterTarget HeliTarget = hit.transform.GetComponent<HelicopterTarget>();

                ////Rocket Jump
                //if (PlayerControllerScript._isGround && CameraControllerScript._rotationX > 70f)
                //{
                //    BackForce = 1200f;
                //    Rb.AddExplosionForce(BackForce, Player.transform.position, 0.05f);
                //}
                if (distance <= 10 && !PlayerControllerScript._isGround)
                {
                    BackForce = 250f;
                    Rb.AddExplosionForce(BackForce, Player.transform.position, 0.05f);
                }
                else if (distance >= 10 && distance <= 15 && !PlayerControllerScript._isGround)
                {
                    BackForce = 150f;
                    Rb.AddExplosionForce(BackForce, Player.transform.position, 0.05f);
                }


                if (HeliTarget != null)
                {
                    GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                    HeliTarget.Explode(damage);
                }

                if (TrashTarget != null)
                {
                    GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                    TrashTarget.Explode();
                }

                if (organs != null)
                {
                    GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                }

                if (target != null)
                {
                    GameObject EnemyBloodParticle = Instantiate(BloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                if (bombScript != null)
                {
                    bombScript.Explode();
                }

                GameObject impactOB = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactOB, 2f);
            }
        }




    }

    IEnumerator CrossHairScale()
    {
        CrossHair.rectTransform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        yield return new WaitForSeconds(0.05f);
        CrossHair.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    Vector3 GetShootingDirection()
    {
        Vector3 targetPos = cam.position + cam.forward * range;
        targetPos = new Vector3(targetPos.x + Random.Range(-InAccuracyDistance, InAccuracyDistance), targetPos.y + Random.Range(-InAccuracyDistance, InAccuracyDistance),targetPos.z + Random.Range(-InAccuracyDistance, InAccuracyDistance));

        Vector3 direction = targetPos - cam.position;
        return direction.normalized;
    }
}
