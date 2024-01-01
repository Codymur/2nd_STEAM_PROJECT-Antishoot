using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBreath : MonoBehaviour
{
    public AnimationCurve VerticalCurve;
    public AnimationCurve HorizontalCurve;



    public float Amplitude, TimeElapsed, Frequency, RotationalAmlitude, HorizontalAmplitude, MotionSpeed, PhaseShift, Multiplier;

    public PlayerHealth PlayerHealthScript;

    public GameObject RotationPoint;

    private void Start()
    {
        PlayerHealthScript = GameObject.FindGameObjectWithTag("MainPlayerTag").GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        if (PlayerHealthScript.HealthBar.fillAmount > 0f)
        {      
            Breath();
        }

        if (PlayerHealthScript.HealthBar.fillAmount <= 0f)
        {
            RotationPoint.SetActive(false);
        }
    }

    void Breath()
    {
        TimeElapsed += Time.deltaTime;
        Vector3 pos = new Vector3(HorizontalCurve.Evaluate(TimeElapsed) * HorizontalAmplitude * MotionSpeed, VerticalCurve.Evaluate(TimeElapsed * Frequency) * Amplitude, transform.localPosition.z);
        transform.localPosition = pos;

        Quaternion rotation = Quaternion.Euler(VerticalCurve.Evaluate(TimeElapsed * Frequency + PhaseShift) * RotationalAmlitude, 0, 0);
        transform.localRotation = rotation;



    }
}
