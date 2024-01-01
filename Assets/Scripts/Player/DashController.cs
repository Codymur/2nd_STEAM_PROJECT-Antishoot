using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashController : MonoBehaviour
{

    Vector3 DelayingDash;
    //dash //Settings // inputs 

    [Header("Dash Movem")]
    public float DashForce;
    public float DashUpForward;
    public float DashDuration;

    [Header("references")]
    public Transform Orientation;
    public CameraController Cam;
    private PlayerController _pmMaster;
    private Rigidbody _rb;

    public GameObject DashParticle;

    [Header("Cooldown")]
    public float DashCd;
    private float _dashTimer;

    [Header("Inputs")]
    KeyCode _dashKey = KeyCode.LeftShift;
    KeyCode _dashKeyControllerInput = KeyCode.JoystickButton2;

    public Image DashBarUI;

    public PlayerHealth PlayerHealthScript;


    public AudioSource DashSound;

    private void Start()
    {
        DashSound = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        _pmMaster = GetComponent<PlayerController>();
        DashBarUI.fillAmount = 0.9f;
        var tempColor = DashBarUI.color;
        tempColor.a = 0f;
        DashBarUI.color = tempColor;
        DashParticle.SetActive(false);
    }

    private void Update()
    {
        if (_pmMaster.Dashing == false && DashBarUI.fillAmount < 1)
        {
            DashBarCoolDown();
        }
        if ((Input.GetKeyDown(_dashKey) || Input.GetKeyDown(_dashKeyControllerInput)) && !_pmMaster.IsSlide && !_pmMaster.isWallRunning && DashBarUI.fillAmount > 0.888f) DashPlayer();
        if (_dashTimer > 0) _dashTimer -= Time.deltaTime;

        StartCoroutine(DashUIAlpha());
    }

    IEnumerator DashUIAlpha()
    {
        if (DashBarUI.fillAmount < 0.899f)
        {
            var tempColor = DashBarUI.color;
            tempColor.a = 1f;
            DashBarUI.color = tempColor;
        }
        else
        {
            var tempColor = DashBarUI.color;
            tempColor.a = 0f;
            DashBarUI.color = tempColor;
        }
        yield return null;
        
    }

    void DashBarCoolDown()
    {
        DashBarUI.fillAmount = Mathf.Lerp(DashBarUI.fillAmount, 1, 1 * Time.deltaTime);
    }

    void DashPlayer()
    {
        if (_dashTimer > 0) return;
        else _dashTimer = DashCd;

        DashSound.Play();
        DashBarUI.fillAmount -= 0.85f;
        _pmMaster.Dashing = true;
        _rb.useGravity = false;
        DashParticle.SetActive(true);


        StartCoroutine(CameraFovDashing());


        Vector3 dir = GetDashDirection(Orientation);
        Vector3 forceToApply = dir * DashForce + Orientation.up * DashUpForward;
        DelayingDash = forceToApply;
        Invoke(nameof(DashingDelay), 0.0025f);
        Invoke(nameof(ResetDash), DashDuration);
    }
    void DashingDelay()
    {
        _rb.AddForce(DelayingDash, ForceMode.Impulse);
    }
    void ResetDash()
    {
        DashParticle.SetActive(false);
        _pmMaster.Dashing = false;

        Cam.DoFov(Cam.CameraFieldOfView);
        if (PlayerHealthScript.HealthBar.fillAmount > 0f)
        {
            Cam.DoTilt(0f);
        }

        _rb.useGravity = true;
    }

    Vector3 GetDashDirection(Transform forwardT)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float veritcal = Input.GetAxisRaw("Vertical");

        Vector3 directions = forwardT.forward * veritcal + forwardT.right * horizontal;

        if (horizontal == 0 && veritcal == 0)
        {
            directions = forwardT.forward;
        }
        return directions.normalized;

    }


    IEnumerator CameraFovDashing()
    {
        Cam.DoFov(Cam.CameraFieldOfView + 35f);
        yield return new WaitForSeconds(1f);
        Cam.DoFov(Cam.CameraFieldOfView);
    }

}
