using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraController : MonoBehaviour
{
    [Header("Mouse Sensivity")]
    public static float _sensX = 100f;
    public static float _sensY = 100f;
    [SerializeField] float _multiplier;

    float _mouseX;
    float _mouseY;

    public float _rotationX;
    float _rotationY;

    [SerializeField] Transform _camera;
    [SerializeField] Transform _orientation;

    public float CameraFieldOfView = 70f;
    public static float CameraFieldOfViewOption;

    public PlayerHealth PlayerHealthScript;

    private void Start()
    {
        PlayerHealthScript = GameObject.FindGameObjectWithTag("MainPlayerTag").GetComponent<PlayerHealth>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    private void Update()
    {
        if (PlayerHealthScript.HealthBar.fillAmount > 0f)
        {
            CameraFieldOfView = CameraFieldOfViewOption;
            ControlCameraInput();

            _camera.transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
            _orientation.rotation = Quaternion.Euler(0f, _rotationY, 0f);
        }
    }
    void ControlCameraInput()
    {
        _mouseX = Input.GetAxisRaw("Mouse X");
        _mouseY = Input.GetAxisRaw("Mouse Y");

        _rotationY += _mouseX * _sensX * _multiplier;
        _rotationX -= _mouseY * _sensY * _multiplier;

        _rotationX = Mathf.Clamp(_rotationX, -90, 90);
    }

    public void CameraSensitivityX(float  SensX)
    {
        _sensX = SensX;
    }
    public void CameraSensitivityY(float SensY)
    {
        _sensY = SensY;
    }

    public void CameraFieldOfViewMethod(float CameraFieldOfViewSetting)
    {
        
        CameraFieldOfViewOption = CameraFieldOfViewSetting;
    }


    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
    public void DoTiltX(float xTilt)
    {
        transform.DOLocalRotate(new Vector3(xTilt, 0, 0), 0.25f);
    }
}