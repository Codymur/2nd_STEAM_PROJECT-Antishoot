using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public Image HealthBar;
    public float health;
    public float actualHealth;

    public float speed;


    public TiemrCountUp TimerCountUpScript;



    public GameObject GameOverPanel;
    public PauseMenu PauseMenuScript;

    public GameObject CanvasGameOverImage;

    public CameraController CameraControllerScript;

    public UziController uziControllerScript;
    public MinigunController MinigunControlerScript;
    public WeaponController WeaponControllerScript;
    public ShotgunController ShotgunControllerScript;


    [SerializeField] Transform _playerRig;
    [SerializeField] float _slideYScale;

    public GameObject Hurting;




    private void Start()
    {
        Hurting.SetActive(false);
        HealthBar.fillAmount = 1f;
        health = HealthBar.fillAmount;
        GameOverPanel.SetActive(false);
        CanvasGameOverImage.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HealthBar.fillAmount -= 0.35f;
            StartCoroutine(HurtingCourtion());
        }
    }

    private void Update()
    {
        if (HealthBar.fillAmount <= 0f)
        {
            uziControllerScript.CanShoot = false;
            MinigunControlerScript.CanShoot = false;
            ShotgunControllerScript.CanShoot = false;
            WeaponControllerScript.CanShoot = false;

            StartCoroutine(GameOverAnimation());
            
            PauseMenuScript.GameUI.SetActive(false);
            //TimerCountUpScript.enabled = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    IEnumerator GameOverAnimation()
    {
        _playerRig.localScale = new Vector3(_playerRig.localScale.x, _slideYScale, _playerRig.localScale.z);
        CameraControllerScript.DoTilt(-90f);
        CanvasGameOverImage.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        GameOverPanel.SetActive(true);
    }

    IEnumerator HurtingCourtion()
    {
        Hurting.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        Hurting.SetActive(false);
    }

}
